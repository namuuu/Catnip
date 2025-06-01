

using UnityEngine;

public class CarrotCrop : Plantation
{
    private int growthStage = 1; // Represents the growth stage of the wheat crop
    private const int maxGrowthStage = 3; // Maximum growth stage for wheat

    private float growthTime = 5f;

    public CarrotCrop(GameObject gameObject, int x, int y) : base(gameObject, "Carrot", x, y)
    {
        Debug.Log($"Initializing {id} at position ({x}, {y})");
        spriteResolver.SetCategoryAndLabel(id, "Age_1");
    }

    public override bool ValidatePlant()
    {
        return PlantValidationUtil.IsFreeFarmland(x, y)
            && PlantValidationUtil.IsWithinBounds(x, y);
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
            Debug.Log("Carrot crop is not ready for harvest yet.");
            return false; // Crop is not ready for harvest
        }

        return true; // Harvest successful
    }
    
    public override void Delete()
    {
        BaseDelete(); // Call the base delete method to handle common deletion logic
        Debug.Log($"Carrot crop at ({x}, {y}) deleted.");
    }
}