using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerationController : MonoBehaviour
{

    [Header("Map Generation Settings")]
    [SerializeField] private int mapWidth = 100;
    [SerializeField] private int mapHeight = 100;
    [SerializeField] private float noiseScale = 0.1f; // Scale for Perlin noise

    [Header("References")]
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase grassTile;
    [SerializeField] private TileBase waterTile;
    [SerializeField] private TileBase rockTile;

    public static TileType[,] TileMap { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize the TileMap array
        TileMap = new TileType[mapWidth, mapHeight];

        // Generate the map
        GenerateMap();
    }

    void GenerateMap()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                TileType tileType = GetTileType(x, y); // Adjust coordinates to center the map
                TileBase tile = GetTileBase(tileType);
                tilemap.SetTile(new Vector3Int(x - mapHeight / 2, y - mapHeight / 2, 0), tile);
                TileMap[x, y] = tileType; // Store the tile type in the TileMap array
            }
        }
    }

    TileType GetTileType(int x, int y)
    {
        float noiseValue = Mathf.PerlinNoise(x * noiseScale, y * noiseScale);

        if (noiseValue < 0.2f)
        {
            return TileType.Water; // Water tile
        }
        else if (noiseValue < 0.6f)
        {
            return TileType.Grass; // Grass tile
        }
        else
        {
            return TileType.Rock; // Rock tile
        }
    }

    TileBase GetTileBase(TileType tileType)
    {
        return tileType switch
        {
            TileType.Grass => grassTile,
            TileType.Water => waterTile,
            TileType.Rock => rockTile,
            _ => null,
        };
    }
}

public enum TileType
{
    Grass,
    Water,
    Rock
}
