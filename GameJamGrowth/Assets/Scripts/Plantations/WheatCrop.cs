

using UnityEngine;
using UnityEngine.U2D.Animation;

public class WheatCrop : Plantation
{
    private int growthStage = 0; // Represents the growth stage of the wheat crop
    private const int maxGrowthStage = 3; // Maximum growth stage for wheat

    private float growthTime = 5f;

    public WheatCrop(GameObject gameObject, int x, int y) : base(gameObject, x, y)
    {
        Debug.Log($"Initializing WheatCrop at position ({x}, {y})");
        spriteResolver.SetCategoryAndLabel("Wheat", "Age_1");
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
                spriteResolver.SetCategoryAndLabel("Wheat", $"Age_{growthStage}");
                Debug.Log($"Wheat crop grown to stage {growthStage}");
            }
            else
            {
                Debug.Log("Wheat crop is fully grown and ready for harvest.");
                // Logic for harvesting can be added here
            }
        }
    }
}