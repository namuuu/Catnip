

using UnityEngine;

public class WheatCrop : Plantation
{
    private int growthStage = 1; // Represents the growth stage of the wheat crop
    private const int maxGrowthStage = 3; // Maximum growth stage for wheat

    private float growthTime = 5f;

    public WheatCrop(GameObject gameObject, int x, int y) : base(gameObject, "Wheat", x, y)
    {
        Debug.Log($"Initializing WheatCrop at position ({x}, {y})");
        spriteResolver.SetCategoryAndLabel(id, "Age_1");
    }

    public override void Update()
    {
        // Implement specific update logic for WheatCrop
        // For example, check if the crop is ready for harvesting or needs watering

        if (PlantationController.time - burriedTime >= growthTime * (growthStage + 1) && growthStage <= maxGrowthStage)
        {
            // Increment growth stage
            growthStage++;

            // Update the sprite based on the growth stage
            if (growthStage <= maxGrowthStage)
            {
                spriteResolver.SetCategoryAndLabel(id, $"Age_{growthStage}");
                Debug.Log($"Wheat crop grown to stage {growthStage}");
            }
            else
            {
                Debug.Log("Wheat crop is fully grown and ready for harvest.");
                // Logic for harvesting can be added here
            }
        }
    }

    public override bool Harvest()
    {
        if (growthStage < maxGrowthStage)
        {
            Debug.Log("Wheat crop is not ready for harvest yet.");
            return false; // Crop is not ready for harvest
        }
        
        return true; // Harvest successful
    }
}