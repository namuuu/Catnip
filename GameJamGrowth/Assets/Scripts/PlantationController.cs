using UnityEngine;

public class PlantationController : MonoBehaviour
{
    [SerializeField] private Plantation[] plantations;

    [Tooltip("Prefab for creating new plantations.")]
    [SerializeField] private GameObject plantationPrefab;

    private void Start()
    {
        // Initialize plantations or load from a save file if necessary
        foreach (var plantation in plantations)
        {
            // Example: plantation.Initialize();
        }
    }

    private void Update()
    {
        // Update each plantation
        foreach (var plantation in plantations)
        {
            plantation.Update();
        }
    }

    public void InteractWithPlantation(int index)
    {
        if (index < 0 || index >= plantations.Length)
        {
            Debug.LogWarning("Invalid plantation index.");
            return;
        }

        // Logic to interact with the specified plantation
        Debug.Log($"Interacting with plantation: {plantations[index].GameObject.name}");
    }

    // public void CreatePlantation(int x, int y)
    // {
    //     // TODO: Check if the coordinates are valid for creating a plantation

        
    // }
}