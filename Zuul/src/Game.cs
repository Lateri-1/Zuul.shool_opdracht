using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
class Game
{
	
	// Private fields
	// Constructor
	private Parser parser;
	private Player player;

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
		Item key = new Item (1, "old key.");
		


		// ...
		// And add them to the Rooms
		lab.Items.Add("poision", Poision);
		outside.Items.Add("blade", Blade);
		pub.Items.Add("backpack", backpack);
		kelder.Items.Add("key", key);




		// make single door
		var Door = new Door("old door.", "old key", true);

		//locked the room
		office.Doors.Add("Strange door", Door);

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
		while (!finished & player.IsAlive())
		{
			Command command = parser.GetCommand();
			finished = ProcessCommand(command);
		}
		if (!player.IsAlive())
		{
			Console.WriteLine("You have died!");
			Console.WriteLine("Thank you for playing.");
			Console.WriteLine("Press [Enter] to continue.");
			Console.ReadLine();
		}
	}
	//METHOD FOR TAKE CUZZ IM TOO STUPID
	private void TakeItem(Command command)
	{
		// exist?
		if (!command.HasSecondWord())
		{
			Console.WriteLine("huh?"); 
			return;
		}
		// Get secondwoord
		string itemname = command.SecondWord;
		bool success = player.TakeItem(itemname);
		if (success)
		{
			Console.WriteLine($"You took {itemname}");
		}
	}
	// SAMEEEEEEE
	private void DropItem(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("huh?huh?"); 
			return;
		}
		string itemname = command.SecondWord;
		Item success = player.inventory.GetItem(itemname, player);
	}
	//METHOD USE WITH THE SAME REASON
	public void UseCommand(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Use what?");
			return;
		}
		string itemname = command.SecondWord;

		if (command.Has3woord())
		{
			string direction = command.Thirdwoord;
			UseDoorKey(itemname, direction);
		}
		else
		{
			UseItem(itemname);
		}
	}
	private void UseDoorKey(string keyname, string direction)
	{
		Room nextRoom = player.CurrentRoom.GetExit(direction);
		if (nextRoom == null)
    {
        Console.WriteLine("Why are yu trying to get in the void?");
        return;
	}
		Door targetDoor = null;
		foreach (var doorPair in player.CurrentRoom.Doors)
			{
				if (doorPair.Key.ToLower().Contains(direction.ToLower()))
				{
					targetDoor = doorPair.Value;
					break;
				}
			}
		if (targetDoor == null)
    {
        Console.WriteLine("Are you trying to open a space? There is no door");
        return;
    }
	Item key = player.inventory.Getitem(keyname.ToLower());
	if (key == null)
		{
			Console.WriteLine("You dont have a key to open the door.");
			return;
		}
		if (key.Name.ToLower() != targetDoor.RequiredKey.ToLower())
		{
			Console.WriteLine($"The {key.Name} doesn't fit this door.");
        return;
		}
		if (targetDoor.IsLocked)
		{
			targetDoor.IsLocked = false;
            targetDoor.IsOpen = true;
			player.inventory.GetItem(keyname.ToLower(), player);
			Console.WriteLine($"You used the {key.Name} to unlock the door!");
            Console.WriteLine($"The door to {direction} is now open.");
		} else
    {
        Console.WriteLine("This door is already unlocked.");
    }
	}
	// useitem method
	public void UseItem(string itemname)
	{
		itemname = itemname.ToLower();
		Item item = player.inventory.Getitem(itemname);
		if (item == null)
    {
        Console.WriteLine($"You don't have '{itemname}'.");
        return;
    }
	switch (itemname)
		{
			case "poision":
				player.Heal(30);
				Console.WriteLine("You drank the health poision. +30 HP!");
            Console.WriteLine($"Current health: {player.GetHealth()}/100");
			break;
			case "blade":
            Console.WriteLine("You swing the Dragon Slayer blade menacingly!");
            Console.WriteLine("(This weapon can be used in combat)");
            break;
			case "backpack":
            Console.WriteLine("You equip the backpack. Inventory space increased!");
            player.inventory.GetItem(itemname, player);
			player.inventory.IncreaseCapacity(2);
            break;
			default:
            Console.WriteLine($"You can't use the {item.Name} right now.");
            break;

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
				if (GoRoom(command)) return true;
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
				TakeItem(command);
				break;
			case "drop":
				DropItem(command);
			break;
			case "inv":
				player.inventory.ShowInv();
			break;
			case "use":
                UseCommand(command);
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
	// room, otherwise print an error message.
	private bool GoRoom(Command command)
	{
		if(!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go where?");
			return false;
		}

		string direction = command.SecondWord;

		// Try to go to the next room.
		Room nextRoom = player.CurrentRoom.GetExit(direction);

		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to "+direction+"!");
			return false;
		}
		// check the door 
		foreach (var doorPair in nextRoom.Doors.Values)
{
    if (doorPair.IsLocked)
    {
        Console.WriteLine($"The way is blocked by a locked door: {doorPair.Name}");
        Console.WriteLine($"You need a key to unlock it. Use: 'use <key> {direction}'");
        return false;
    }
}
		if (nextRoom == player.EndRoom)
		{
			Console.WriteLine("Congratulations! You have found the admin office and won the game!");
			Console.WriteLine("Thank you for playing.");
			Console.WriteLine("Press [Enter] to continue.");
			Console.ReadLine();
			return true; // gameover
		}

		player.Damage(15); // speler verliest health bij elke move
		Console.WriteLine("You have taken 15 damage.");
		player.CurrentRoom = nextRoom;
        Console.WriteLine(player.CurrentRoom.GetLongDescription());
		if (nextRoom.Doors.Count > 0)
		{
			Console.WriteLine("\nDoor:");
			foreach (var Door in nextRoom.Doors.Values) 
			{
				string status = Door.IsOpen ? "[is open.]" : Door.IsLocked ? "[is locked.]" : "[is closed.]";
				Console.WriteLine($"{Door.Name} {status}");
			}
		}
		return false;
	}
}
