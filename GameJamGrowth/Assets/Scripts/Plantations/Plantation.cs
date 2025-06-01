using UnityEngine;
using UnityEngine.U2D.Animation;

public abstract class Plantation
{

    public int x;
    public int y;
    public string id;

    protected SpriteResolver spriteResolver;

    protected float burriedTime = 0f; // Time since the crop was planted

    public GameObject GameObject { get; }

    public Plantation(GameObject gameObject, string id, int x, int y)
    {
        GameObject = gameObject;
        spriteResolver = gameObject.GetComponent<SpriteResolver>();
        burriedTime = PlantationController.time; // Initialize with the current game time
        this.x = x;
        this.y = y;
        this.id = id;

        MapController.Plantations[x, y] = this; // Register the plantation in the map controller
    }

    public abstract void Update();

    public abstract bool Harvest();

    public abstract void Delete();

    protected void BaseDelete()
    {
        GameObject.SetActive(false);
        MapController.Plantations[x, y] = null; // Remove the plantation from the map controller
        PlantationController.instance.plantations.Remove(this); // Remove from the plantation controller
    }
}