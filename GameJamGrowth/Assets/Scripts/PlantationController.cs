using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlantationController : MonoBehaviour
{
    [SerializeField] private List<Plantation> plantations = new List<Plantation>();

    [Tooltip("Prefab for creating new plantations.")]
    [SerializeField] private GameObject plantationPrefab;

    public static PlantationController instance;

    public static float time = 0f; // Global time variable to track the game time

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

    private void Start()
    {
        // Initialize plantations or load from a save file if necessary
        foreach (var plantation in plantations)
        {
            // Example: plantation.Initialize();
        }
    }

    private void OnEnable()
    {

    }

    private void Update()
    {
        time += Time.deltaTime;

        // Update each plantation
        foreach (var plantation in plantations)
        {
            plantation.Update();
        }
    }

    public void InteractWithPlantation(int index)
    {
        if (index < 0 || index >= plantations.Count)
        {
            Debug.LogWarning("Invalid plantation index.");
            return;
        }

        // Logic to interact with the specified plantation
        Debug.Log($"Interacting with plantation: {plantations[index].GameObject.name}");
    }

    public void CreatePlantation(string itemName, int x, int y)
    {
        Debug.Log($"Creating plantation at coordinates: ({x}, {y}) of type {itemName}");

        // Get the item type from the item name
        string itemType = itemName.Split(':')[0]; // Assuming itemName is formatted like "Wheat_Crop"

        // TODO: Check if the coordinates are valid for creating a plantation
        GameObject newPlantationObject = Instantiate(plantationPrefab, new Vector3(x, 0, y), Quaternion.identity);

        switch (itemType)
        {
            case "Wheat":
                WheatCrop newPlantation = new(newPlantationObject, x, y);
                plantations.Add(newPlantation);
                break;
            case "Carrot":
                CarrotCrop newCarrotPlantation = new(newPlantationObject, x, y);
                plantations.Add(newCarrotPlantation);
                break;
            // Add more cases for different plantation types as needed
            default:
                Debug.LogWarning($"Unknown plantation type: {itemType}");
                return; // Exit if the type is unknown
        }

        
    }
}