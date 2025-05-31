using UnityEngine;
using UnityEngine.U2D.Animation;

public abstract class Harvestable
{

    protected SpriteResolver spriteResolver;

    public GameObject GameObject { get; }

    public Harvestable(GameObject gameObject)
    {
        GameObject = gameObject;
        spriteResolver = gameObject.GetComponent<SpriteResolver>();
    }

}