using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

class Player
{
// auto property
    private Player player;
    private Inventory inventory;
    public Item items;

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
        if (!player.inventory.items.ContainsKey(itemname))
        {
            return "If you want to use it, you need to have it";
        }



        return "You have used {itemname}";

    }
// methods
// methods
    public void Damage(int value) { health -= value; } // speler verliest health
    public void Heal(int value) { health += value; } // speler regen health 
    public bool IsAlive() { return health > 0; } // checkt of speler nog leeft
}