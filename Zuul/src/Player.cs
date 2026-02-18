class Player
{
    // fields
    public Inventory inventory;
    public Room CurrentRoom { get; set; }
    public Room EndRoom { get; set; }
    private int health; 

    // constructor
    public Player()
    {
        inventory = new Inventory(5);
        health = 100;
        CurrentRoom = null;
    }
    public int GetHealth()
    {
        return health;
    }
    public string Use(string itemname)
    {
        itemname = itemname.ToLower();
        Item item = inventory.Getitem(itemname);

        if (item == null)
        {
            return "If you want to use it, you need to have it";
        }
        return $"You have used {itemname}";
    }
// methods
    public void Damage(int value) { health -= value; } // speler verliest health
    public void Heal(int value) { health += value; } // speler regen health 
    public bool IsAlive() { return health > 0; } // checkt of speler nog leeft
    public bool TakeItem(string itemname)
    {
        // i ad it cuzz of protection for inventory thoooo
        // ИСПРАВЛЕНО: передаём this, т.к. Take() теперь принимает Player как параметр
        return inventory.Take(itemname, this);
    }
    public Item GetInventoryItem(string itemname)
    {
        return inventory.Getitem(itemname);
    }
    
    
    public void IncreaseInventoryCapacity(int amount)
    {
        inventory.IncreaseCapacity(amount);
    }
}