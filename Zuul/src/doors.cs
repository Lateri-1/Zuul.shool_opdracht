using System.Runtime.CompilerServices;

class Door
{
    // fields
    private Inventory inventory;
    public Item item;
    public string Name {get; }
    public bool Islocked { get; private set;}
    public bool Isopen {get; private set;}
    public string Requierdkey {get;}

    public Door(string name, string requierdkey, bool locked=true) {
        Name = name;
        Requierdkey=requierdkey;
        Isopen=false;
        Islocked = locked;
    }
    public bool TryUnlock(List<Item> items)
    {
        if (!Islocked)
        {
            Console.WriteLine("U cannot go because of the locked door");
            return false;
        }
        Item key = items.Find(i=>i.Name==Requierdkey);
        if (key == null)
        {
            Console.WriteLine($"U must have [{Requierdkey}] to go trough this door");
            return false;
        }
        Isopen=false;
        Islocked = true;
        items.Remove(key);
        return true;
    }
}