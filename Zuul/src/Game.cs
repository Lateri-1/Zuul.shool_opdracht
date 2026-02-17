using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
class Game
{
	
	// Private fields
	// Constructor
	private Parser parser;
	private Player player;

	private Inventory inv;

	public Game ()
	{
		parser = new Parser();
		player = new Player();
		CreateRooms();
	}	
	// Initialise the Rooms (and the Items)
	private void CreateRooms()
	{
		// Create the rooms
		Room outside = new Room("outside the main entrance of the university");
		Room theatre = new Room("in a lecture theatre");
		Room pub = new Room("in the campus pub");
		Room lab = new Room("in a computing lab");
		Room office = new Room("in the computing admin office");
		Room kelder = new Room("in a dark, damp kelder");
		Room roof = new Room("on the roof of ");

		// Initialise room exits
		outside.AddExit("east", theatre);
		outside.AddExit("south", lab);
		outside.AddExit("west", pub);

		theatre.AddExit("west", outside);
		theatre.AddExit("down", kelder);


		pub.AddExit("east", outside);
		pub.AddExit("down", kelder);

		lab.AddExit("north", outside);
		lab.AddExit("up", office);


		office.AddExit("down", lab);

		kelder.AddExit("up", outside);

		roof.AddExit("down", office);

		// Create your Items here
		Item Poision = new Item (2, "Health poision.");
		Item Blade = new Item (3, "Dragon slayer.");
		Item backpack = new Item (-2, "Backpack for 2 more space in inventory.");
		Item key = new Item (1, "Strange key to unlock some door.");
		


		// ...
		// And add them to the Rooms
		lab.Items.Add("poision", Poision);
		outside.Items.Add("blade", Blade);
		pub.Items.Add("backpack", backpack);




		// ...
		

		// Start game outside
		player.CurrentRoom = outside;
		player.EndRoom = office; 
		
	}

	//  Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome();

		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.
			bool finished = false;
			if (player.IsAlive())
			{
				Command command = parser.GetCommand();
				finished = ProcessCommand(command);
			}
			else {
			Console.WriteLine("You have died!");
			Console.WriteLine("Thank you for playing.");
			Console.WriteLine("Press [Enter] to continue.");
			Console.ReadLine();
			}
	}
	
	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to Zuul!");
		Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
		Console.WriteLine("Type 'help' if you need help.");
		Console.WriteLine();
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}

	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if(command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean...");
			return wantToQuit; // false
		}

		switch (command.CommandWord)
		{
			case "help":
				PrintHelp();
				break;
			case "go":
				GoRoom(command);
				break;
			case "quit":
				wantToQuit = true;
				break;
			case "look":
				Console.WriteLine(player.CurrentRoom.GetLongDescription());
				break;
				case "status": 
				Console.WriteLine("you have " + player.GetHealth() + "/100 health left");
				break;
			case "take":

				break;
			case "drop":

			break;
			case "inv":
			
			break;
		}

		return wantToQuit;
	}

	// ######################################
	// implementations of user commands:
	// ######################################
	
	// Print out some help information.
	// Here we print the mission and a list of the command words.
	private void PrintHelp()
	{
		Console.WriteLine("You are lost. You are alone.");
		Console.WriteLine("You wander around at the university.");
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}

	// Try to go to one direction. If there is an exit, enter the new
	// room, otherwise print an error message
	private void GoRoom(Command command)
	{
		if(!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go where?");
			return;
		}

		string direction = command.SecondWord;

		// Try to go to the next room.

		Room nextRoom = player.CurrentRoom.GetExit(direction);
		if (nextRoom == player.EndRoom)
		{
			Console.WriteLine("Congratulations! You have found the admin office and won the game!");
			Console.WriteLine("Thank you for playing.");
			Console.WriteLine("Press [Enter] to continue.");
			Console.ReadLine();
		}
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to "+direction+"!");
			return;
		}
		player.Damage(5); // speler verliest  health bij elke move
		Console.WriteLine("You have taken 15 damage.");
		player.CurrentRoom = nextRoom;
        Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}
}
