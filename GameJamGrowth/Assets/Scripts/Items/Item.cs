abstract class Item
{
    public string Name { get; set; }
    public string Id { get; set; }
    public string Description { get; set; }
    public ItemType ItemType { get; set; }

    public Item(string name, string id, string description, ItemType itemType)
    {
        Name = name;
        Id = id;
        Description = description;
        ItemType = itemType;
    }

    public abstract void Use();
}

enum ItemType
{
    Seed,
}