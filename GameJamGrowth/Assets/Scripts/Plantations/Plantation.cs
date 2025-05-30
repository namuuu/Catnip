using UnityEngine;
using UnityEngine.U2D.Animation;

public abstract class Plantation
{

    public int x;
    public int y;

    protected SpriteResolver spriteResolver;

    protected float burriedTime = 0f; // Time since the crop was planted

    public GameObject GameObject { get; }

    public Plantation(GameObject gameObject, int x, int y)
    {
        GameObject = gameObject;
        spriteResolver = gameObject.GetComponent<SpriteResolver>();
        burriedTime = PlantationController.time; // Initialize with the current game time
        this.x = x;
        this.y = y;
    }

    public abstract void Update();
}