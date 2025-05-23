using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{

    [Header("Refernces")]
    public PlayerInput playerInput;
    public GameObject hotbar;

    private const int HOTBAR_DISPLAY_MAX = 5;
    private int currentSlotIndex = 0;
    private int topSlotIndex = 0;
    private GameObject[] hotbarSlots;

    void OnEnable()
    {
        playerInput.actions["Interact"].performed += OnInteract;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hotbarSlots = GetHotbarSlots();

        // Change the color of the first slot to red
        hotbarSlots[currentSlotIndex].GetComponent<Image>().color = Color.red;

        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            // if (i < topSlotIndex)
            // {
            //     hotbarSlots[i].SetActive(false);
            //     continue;
            // }

            // if (i >= topSlotIndex + HOTBAR_DISPLAY_MAX)
            // {
            //     hotbarSlots[i].SetActive(false);
            //     continue;
            // }

            // hotbarSlots[i].SetActive(true);            
        }
    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        // Check if the action is triggered
        if (context.performed)
        {
            // Get the current slot index
            int newSlotIndex = currentSlotIndex + 1;

            // Check if the new slot index is within bounds
            if (newSlotIndex >= hotbarSlots.Length)
            {
                newSlotIndex = 0;
            }

            // Set the active slot
            SetActiveSlot(newSlotIndex);
        }
    }

    private GameObject[] GetHotbarSlots()
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

    private void SetActiveSlot(int index)
    {
        // Debug.Log("Setting active slot to: " + index);

        if (index < 0 || index >= hotbarSlots.Length)
            return;

        MoveToActiveSlot(index);

        // Change previous slot to color white
        hotbarSlots[currentSlotIndex].GetComponent<Image>().color = Color.white;

        // Change current slot to color red
        hotbarSlots[index].GetComponent<Image>().color = Color.red;
        currentSlotIndex = index;
    }

    private void MoveToActiveSlot(int index)
    {
        // Check if the index is out of bounds (lower bound)
        if (index + 1 >= topSlotIndex + HOTBAR_DISPLAY_MAX)
        {
            // Is it the last slot?
            if (index == hotbarSlots.Length - 1)
            {
                // Debug.Log("index: " + index + " topSlotIndex: " + topSlotIndex);
                // Debug.Log(index - topSlotIndex - HOTBAR_DISPLAY_MAX + 1);
                hotbar.transform.DOMoveY(hotbar.transform.position.y + (index - topSlotIndex - HOTBAR_DISPLAY_MAX + 1) * 120, 0.5f);
                topSlotIndex = index - HOTBAR_DISPLAY_MAX + 1;
                // Debug.Log("topSlotIndex: " + topSlotIndex);
            }
            else
            {
                // Debug.Log("index: " + index + " topSlotIndex: " + topSlotIndex);
                // Debug.Log(index - topSlotIndex - HOTBAR_DISPLAY_MAX + 2);
                hotbar.transform.DOMoveY(hotbar.transform.position.y + (index - topSlotIndex - HOTBAR_DISPLAY_MAX + 2) * 120, 0.5f);
                topSlotIndex = index - HOTBAR_DISPLAY_MAX + 2;
                // Debug.Log("topSlotIndex: " + topSlotIndex);
            }
        }

        // Check if the index is out of bounds (upper bound)
        if (index < topSlotIndex)
        {
            // Is it the first slot?
            if (index == 0)
            {
                Debug.Log("index: " + index + " topSlotIndex: " + topSlotIndex);
                Debug.Log(index - topSlotIndex + 1);
                hotbar.transform.DOMoveY(hotbar.transform.position.y + (index - topSlotIndex) * 120, 0.5f);
                topSlotIndex = index;
                Debug.Log("topSlotIndex: " + topSlotIndex);
            }
            else
            {
                Debug.Log("index: " + index + " topSlotIndex: " + topSlotIndex);
                Debug.Log(index - topSlotIndex);
                hotbar.transform.DOMoveY(hotbar.transform.position.y + (index - topSlotIndex) * 120, 0.5f);
                topSlotIndex = index;
                Debug.Log("topSlotIndex: " + topSlotIndex);
            }
        }
    }
}
