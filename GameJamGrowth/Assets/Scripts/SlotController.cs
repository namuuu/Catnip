using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.U2D.Animation;

public class SlotController : MonoBehaviour
{

    [Header("References")]
    public PlayerInput playerInput;
    public GameObject hotbar;
    private Vector3 hotbarOrigin;
    public SpriteLibraryAsset spriteLibraryAsset;

    [Header("Hotbar Settings")]
    public const int HOTBAR_DISPLAY_MAX = 5;
    public const float PADDING = 120f;
    private int currentSlotIndex = 0;
    private int topSlotIndex = 0;
    private GameObject[] hotbarSlots;


    void OnEnable()
    {
        InputPolling.OnNumRowPressed += SetRelativeSlot;

        hotbarOrigin = hotbar.transform.position;
    }

    void OnDisable()
    {
        InputPolling.OnNumRowPressed -= SetRelativeSlot;

        // Reset the hotbar position when the script is disabled
        hotbar.transform.position = hotbarOrigin;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hotbarSlots = GetHotbarSlots();

        // Change the color of the first slot to red
        hotbarSlots[currentSlotIndex].GetComponent<Image>().color = Color.red;
        SetActiveSlot(currentSlotIndex);
    }

    private GameObject[] GetHotbarSlots()
    {
        // Get the hotbar slots from the hotbar GameObject
        Transform[] hotbarSlots = hotbar.GetComponentsInChildren<Transform>();

        // Create an array to hold the hotbar slots
        GameObject[] slots = new GameObject[hotbarSlots.Length - 1];
        int slotIndex = 0;
        for (int i = 0; i < hotbarSlots.Length - 1; i++)
        {
            // Check if the element has the "Slot" tag
            if (hotbarSlots[i + 1].tag != "Slot")
                continue;

            // Skip the first element, which is the hotbar itself
            slots[slotIndex] = hotbarSlots[i + 1].gameObject;
            slotIndex++;
        }

        return slots;
    }

    private void SetRelativeSlot(int relativeSlotIndex)
    {
        SetActiveSlot(topSlotIndex + relativeSlotIndex);
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

    // Slides the hotbar so the active slot is in the middle of the screen
    private void MoveToActiveSlot(int index)
    {
        // Check if the index is out of bounds (lower bound)
        if (index + 1 >= topSlotIndex + HOTBAR_DISPLAY_MAX)
        {
            // Is it the last slot?
            if (index == hotbarSlots.Length - 1)
                topSlotIndex = index - HOTBAR_DISPLAY_MAX + 1;
            else
                topSlotIndex = index - HOTBAR_DISPLAY_MAX + 2;
        }

        // Check if the index is out of bounds (upper bound)
        if (index <= topSlotIndex)
        {
            // Is it the first slot?
            if (index == 0)
                topSlotIndex = index;
            else
                topSlotIndex = index - 1;
        }

        hotbar.transform.DOMoveY(hotbarOrigin.y + topSlotIndex * PADDING, 0.5f);
    }

    private void SetItemInSlot(int index, string category, string label)
    {
        GameObject slot = hotbarSlots[index];
        Sprite sprite = spriteLibraryAsset.GetSprite(category, label);
        sprite.name = category + "_" + label;
        slot.GetComponentsInChildren<Image>()[1].sprite = sprite;
    }

    private string GetItemInSlot(int index)
    {
        GameObject slot = hotbarSlots[index];
        return slot.GetComponentsInChildren<Image>()[1].sprite.name;
    }

    private string GetActiveItem()
    {
        return GetItemInSlot(currentSlotIndex);
    }
}
