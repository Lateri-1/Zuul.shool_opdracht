class Inventory
    {
    // fields
    private int maxWeight;
    private Dictionary<string, Item> items;
    // constructor
    public Inventory(int maxWeight)
        {
        this.maxWeight = maxWeight;
        this.items = new Dictionary<string, Item>();
        }
    // methods
    public bool Put(string itemName, Item item)
        {
        // TODO implementeer:
        // Check het gewicht van het Item
        // Is er genoeg ruimte in de Inventory?
        // Past het Item?
        // Zet Item in de Dictionary    
        // Return true/false voor succes/mislukt
        return false;
        }
    public Item Take(string itemName)
        {
        // TODO implementeer:
        // Zoek Item in de Dictionary
        // Verwijder Item uit Dictionary (als gevonden)
        // Return Item of null
        return null;
        }
    }