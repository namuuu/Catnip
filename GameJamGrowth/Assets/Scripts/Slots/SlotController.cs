using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.U2D.Animation;
using System.Collections.Generic;

public class SlotController : MonoBehaviour
{

    [Header("References")]
    public PlayerInput playerInput;
    public GameObject hotbar;
    private Vector3 hotbarOrigin;
    public SpriteLibraryAsset spriteLibraryAsset;
    [SerializeField] private GameObject hotbarSlotPrefab;

    [Header("Hotbar Settings")]
    public const int HOTBAR_DISPLAY_MAX = 6;
    public int hotbarSize = 10; // Total number of slots in the hotbar
    public const float PADDING = 110f;
    private int currentSlotIndex = 0;
    private int topSlotIndex = 0;
    // private GameObject[] hotbarSlots;
    private List<Item> itemList;

    public static SlotController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

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
        itemList = GetHotbarSlots();

        // Change the color of the first slot to red
        SetActiveSlot(currentSlotIndex);
    }

    private List<Item> GetHotbarSlots()
    {
        // Get the hotbar slots from the hotbar GameObject
        Transform[] hotbarSlots = hotbar.GetComponentsInChildren<Transform>();

        // Create an array to hold the hotbar slots
        List<Item> slots = new();
        for (int i = 0; i < hotbarSlots.Length - 1; i++)
        {
            // Check if the element has the "Slot" tag
            if (hotbarSlots[i + 1].tag != "Slot")
                continue;

            // Skip the first element, which is the hotbar itself
            slots.Add(new DefaultItem(hotbarSlots[i + 1].gameObject));
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
        if (index < 0 || index >= itemList.Count)
            return;

        // Change previous slot to color black
        itemList[currentSlotIndex].GameObject.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.5f);

        // Change current slot to color white
        itemList[index].GameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
        currentSlotIndex = index;

        MoveToActiveSlot();
    }

    // Slides the hotbar so the active slot is in the middle of the screen
    private void MoveToActiveSlot()
    {
        // Check if the index is out of bounds (lower bound)
        if (currentSlotIndex + 1 >= topSlotIndex + HOTBAR_DISPLAY_MAX)
        {
            // Is it the last slot?
            if (currentSlotIndex == itemList.Count - 1)
                topSlotIndex = currentSlotIndex - HOTBAR_DISPLAY_MAX + 1;
            else
                topSlotIndex = currentSlotIndex - HOTBAR_DISPLAY_MAX + 2;
        }

        // Check if the index is out of bounds (upper bound)
        if (currentSlotIndex <= topSlotIndex)
        {
            // Is it the first slot?
            if (currentSlotIndex == 0)
                topSlotIndex = currentSlotIndex;
            else
                topSlotIndex = currentSlotIndex - 1;
        }

        hotbar.transform.DOMoveY(hotbarOrigin.y + topSlotIndex * PADDING, 0.5f);
    }

    public Item GetItemInSlot(int index)
    {
        return itemList[index];
    }

    public Item GetActiveItem()
    {
        return GetItemInSlot(currentSlotIndex);
    }

    public void GiveItem(string itemName)
    {
        foreach (Item item in itemList)
        {
            if(item.Id == itemName)
            {
                // If the item already exists, increase its count
                item.AddCount(1);
                return;
            }
        }

        // Check if the hotbar is full
            if (itemList.Count >= hotbarSize)
            {
                Debug.LogWarning("Hotbar is full. Cannot add more slots.");
                return;
            }

        // Create a new slot
        GameObject newSlot = Instantiate(hotbarSlotPrefab, hotbar.transform);

        // Add the new slot to the hotbar slots array

        itemList.Add(ItemUtil.CreateItem(newSlot, itemName));

        // Move the hotbar to the active slot
        MoveToActiveSlot();
    }

    // TODO : test this
    public void RemoveSlot(int index)
    {
        Item item = itemList[index];
        Destroy(item.GameObject);

        // Remove the slot from the itemList
        itemList.RemoveAt(index);

        // Check if the removed slot was the active slot
        if (index == currentSlotIndex)
        {
            SetActiveSlot(currentSlotIndex);
        }
    }
}
