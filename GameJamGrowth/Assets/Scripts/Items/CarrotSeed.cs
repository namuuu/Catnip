using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

class CarrotSeed : Item
{

    public CarrotSeed(GameObject gameObject) : base(gameObject, "Carrot seed", "Carrot:Seed", "A crunchy carrot.", ItemType.Seed)
    {
        // Change sprite
        SetSprite(Id);
        
        SetMaxStackSize(10); // Set maximum stack size to 10
        SetCount(1); // Set initial count to 1

    }

    public override void Use()
    {
        // Logic for using the carrot item
        Debug.Log("You used the carrot!");
    }
}