class Inventory
    {
    // fields
    private int maxWeight;

    public Dictionary<string, Item> items = new Dictionary<string, Item>();
    // constructor
    public Inventory(int maxWeight)
        {
            this.maxWeight = maxWeight;
        }
private int GetCurrentWeight()
    {
        int total = 0;

        foreach (Item item in items.Values)
        {
            total += item.Weight;
        }

        return total;
    }
    // methods
public int FreeWeight()
    {
        return maxWeight - GetCurrentWeight();
    }
        // Vergelijk MaxWeight en TotalWeight

public bool Take(string itemname, Player player)
{
    itemname = itemname.ToLower();
    
    // does this item exist?
    if (!player.CurrentRoom.Items.ContainsKey(itemname))
    {
        Console.WriteLine("This room does not contain anything with this word.");
        return false;
    }
    
    // take it from room
    Item item = player.CurrentRoom.Items[itemname];
    
    // is there enough space?
    if (FreeWeight() < item.Weight)
    {
        Console.WriteLine("You are too weak to take this.");
        return false;
    }
    
    // if ok go to inventory
    items.Add(itemname, item);
    
    // remove from room
    player.CurrentRoom.Items.Remove(itemname);
    
    Console.WriteLine($"You picked up {item.Name}.");
    return true;
}

    public Item GetItem(string itemname, Player player)
        {
        itemname = itemname.ToLower();

        // TODO implementeer:

        // Zoek Item in de Dictionary
        //chekt of this item exist
        if (!items.ContainsKey(itemname))
        {
            Console.WriteLine("You don't have this item.");
            return null;
        }

        // Verwijder Item uit Dictionary (als gevonden) 
        Item item = items[itemname];
        items.Remove(itemname);
        // Return Item of null
        return item;
        }
        public void ShowInv()
        {
            string inv = "";
            Console.WriteLine("Inventory: ");
            foreach (Item Item in items.Values)
            {
                inv += Item.Name;
            }
            Console.WriteLine(inv);
        }
        public Item Getitem(string itemname)
    {
        itemname = itemname.ToLower();

        if (!items.ContainsKey(itemname)) {
        return null;
        }

        return items[itemname];
    }
    public void IncreaseCapacity(int amount)
{
    maxWeight += amount;
}
    }