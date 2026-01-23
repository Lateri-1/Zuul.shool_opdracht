class Item
{
    // fields

    public int ID { get; }
    public int Weight { get; }
    public string Description { get; }

    // constructor
    public Item(int id, int weight, string description)
    {
        ID = id;
        Weight = weight;
        Description = description;
    }
} 
