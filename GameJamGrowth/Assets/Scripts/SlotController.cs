using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class SlotController : MonoBehaviour
{

    [Header("Refernces")]
    public PlayerInput playerInput;
    public GameObject hotbar;

    private const int HOTBAR_DISPLAY_MAX = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        // If left click
        if (playerInput.actions["Attack"].triggered)
        {
            
        }
    }

      private GameObject[] getHotbarSlots()
    {
        // Get the hotbar slots from the hotbar GameObject
        Transform[] hotbarSlots = hotbar.GetComponentsInChildren<Transform>();

        // Create an array to hold the hotbar slots
        GameObject[] slots = new GameObject[hotbarSlots.Length - 1];
        for (int i = 0; i < hotbarSlots.Length - 1; i++)
        {
            // Skip the first element, which is the hotbar itself
            slots[i] = hotbarSlots[i + 1].gameObject;
        }

        return slots;
    }
    

}
