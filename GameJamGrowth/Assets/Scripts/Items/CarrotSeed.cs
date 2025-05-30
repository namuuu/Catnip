using UnityEngine;

class CarrotSeed : Item
{

    public CarrotSeed() : base("Carrot seed", "carrot_seed", "A crunchy carrot.", ItemType.Seed)
    {
    }

    public override void Use()
    {
        // Logic for using the carrot item
        Debug.Log("You used the carrot!");
    }
}