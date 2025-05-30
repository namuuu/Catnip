using UnityEngine;

public class ItemUtil
{
    public static Item CreateItem(GameObject gameObject, string itemName)
    {
        string category = itemName.Split(':')[0];
        string label = itemName.Split(':')[1];

        switch (itemName)
        {
            case "Carrot:Seed":
                Debug.Log("Creating CarrotSeed item.");
                return new CarrotSeed(gameObject);
            default:
                Debug.LogWarning($"Item '{category}:{label}' not recognized. Returning DefaultItem.");
                return new DefaultItem(gameObject);
        }
    }
}