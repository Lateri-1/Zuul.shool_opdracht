using System.Collections;
using System.Security.Cryptography.X509Certificates;

class Player
{
// auto property
    public Room CurrentRoom { get; set; }
    private int health;
// constructor
    public Player()
    {
        // fields
        health = Math.Max(0, 100);

        CurrentRoom = null;
    }

// methods
// methods
    public void Damage(int amount) { health -= amount; } // speler verliest health
    public void Heal(int amount) { health += amount; } // speler krijgt health 
    public bool IsAlive() { return health > 0; } // checkt of speler nog leeft
}