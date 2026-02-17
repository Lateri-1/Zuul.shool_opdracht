using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

class Inventory
    {
    // fields
    private Parser parser;
    private Player player;
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

    public bool Take(string itemname, Item item)
        {
        Item item1 = player.CurrentRoom.Items[itemname];
        item = item1;

        // TODO implementeer:
        items.Add(itemname, item);
        items.Remove(itemname);
        // Check het gewicht van het Item
        // Is er genoeg ruimte in de Inventory?
        // Past het Item
            if (FreeWeight() < item.Weight)
        {
            Console.WriteLine("Not enough space in inventory.");
            return false;
        }

        // Zet Item in de Dictionary  


        
        // Return true/false voor succes/mislukt
        if (!player.CurrentRoom.Items.ContainsKey(itemname))
        {
            Console.WriteLine("This room does not contain anything with this word.");
            return false;
        }
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
    }
