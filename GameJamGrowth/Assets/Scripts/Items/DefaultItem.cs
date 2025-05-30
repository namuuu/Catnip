using UnityEngine;

class DefaultItem : Item
{

    public DefaultItem(GameObject gameObject) : base(gameObject, "Default Item", "default_item", "A debug item.", ItemType.Unknown)
    {
        SetMaxStackSize(1); // Set maximum stack size to 1
        SetCount(1); // Set initial count to 1
    }

    public override void Use()
    {
        // Logic for using the carrot item
        Debug.Log("You used the default item!");
    }
}