using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item
{
    public string Name { get; set; }
    public string Id { get; set; }
    public string Description { get; set; }
    public static ItemType ItemType { get; set; }

    private int maxStackSize = 1;
    private int Count { get; set; } = 0;

    public GameObject GameObject { get; set; }
    private TMPro.TextMeshProUGUI textObject;

    public Item(GameObject gameObject, string name, string id, string description, ItemType itemType)
    {
        GameObject = gameObject;
        Name = name;
        Id = id;
        Description = description;
        ItemType = itemType;

        textObject = GameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    public void SetMaxStackSize(int value)
    {
        if (value < 1)
            return;

        maxStackSize = value;

        if (value == 1)
        {
            textObject.text = string.Empty; // Clear text if max stack size is 1
        }
        else
        {
            textObject.text = Count.ToString(); // Update text to current count
        }
    }





    public void SetCount(int value)
    {
        if (maxStackSize == 1 || value <= 0 || value > maxStackSize)
            return;

        Count = value;

        // Update the UI or any other necessary components to reflect the current stack size
        textObject.text = Count.ToString();
    }

    public void AddCount(int value)
    {
        if (maxStackSize == 1 || value <= 0 || Count + value > maxStackSize)
            return;

        Count += value;

        // Update the UI or any other necessary components to reflect the current stack size
        textObject.text = Count.ToString();
    }

    public int GetCount()
    {
        return Count;
    }

    protected void SetSprite(string spriteName)
    {
        Image image = GameObject.GetComponentsInChildren<Image>()[1];
        image.sprite = SlotController.instance.spriteLibraryAsset.GetSprite(spriteName.Split(':')[0], spriteName.Split(':')[1]);
    }

    public abstract void Use();

    
}

public enum ItemType
{
    Unknown,
    Seed,
}