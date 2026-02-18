using System.Runtime.CompilerServices;

class Door
{
    // fields
    public string Name {get; }
    public bool IsLocked { get; set;}
    public bool IsOpen {get;set;}
    public string RequiredKey {get;}

    public Door(string name, string requiredKey, bool locked=false) {
        Name = name;
        RequiredKey=requiredKey;
        IsOpen=false;
        IsLocked = locked;
    }
    public bool TryUnlock(List<Item> items)
    {
        if (!IsLocked)
        {
            Console.WriteLine("The door is already unlocked.");
            return true;
        }

        Item key = items.Find(i => i.Name.ToLower() == RequiredKey.ToLower());
        if (key == null)
        {
            Console.WriteLine($"You must have [{RequiredKey}] to go through this door");
            return false;
        }
        IsOpen = true;
        IsLocked = false;
        items.Remove(key);
        return true;
    }
}
