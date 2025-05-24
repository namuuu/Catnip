using UnityEngine;

public abstract class Plantation
{

    public int x;
    public int y;

    public GameObject GameObject { get; }

    protected Plantation(GameObject gameObject, int x, int y)
    {
        GameObject = gameObject;
        this.x = x;
        this.y = y;
    }

    protected Plantation()
    {

    }

    public void Update()
    {
        // This method can be overridden by derived classes to implement specific update logic
        // For example, checking if the plantation is ready for harvesting or needs watering
    }
}