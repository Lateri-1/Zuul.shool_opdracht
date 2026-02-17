class Command
{
	public string CommandWord { get; init; }
	public string SecondWord { get; init; }

	public string Thirdwoord { get; set; }
	
	// Create a command object. First and second word must be supplied, but
	// either one (or both) can be null. See Parser.GetCommand()
	public Command(string first, string second, string thierd)
	{
		CommandWord = first;
		SecondWord = second;
		Thirdwoord = thierd;
	}

	
	// Return true if this command was not understood.
	public bool IsUnknown()
	{
		return CommandWord == null;
	}

	
	// Return true if the command has a second word.
	public bool HasSecondWord()
	{
		return SecondWord != null;
	}

	//  Return true if the command has a 3 word.
	public bool Has3woord()
	{
		return Thirdwoord != null;
	}
}
