
using UnityEngine;

public class PlantValidationUtil
{

    /// <summary>
    /// Checks if the specified coordiantes (x, y) are within the bounds of the map.
    /// </summary>
    public static bool IsWithinBounds(int x, int y)
    {
        if (x < 0 || x >= MapController.instance.mapWidth || y < 0 || y >= MapController.instance.mapHeight)
            return false;
        return true;
    }

    /// <summary>
    /// Check if the coordinates (x, y) is occupied by free farmland.
    /// </summary>
    public static bool IsFreeFarmland(int x, int y)
    {
        if (MapController.Plantations[x, y] != null)
            return false;
        return true;
    }
}