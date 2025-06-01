using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{

    [Header("Map Generation Settings")]
    [SerializeField] public int mapWidth = 100;
    [SerializeField] public int mapHeight = 100;
    [SerializeField] private float noiseScale = 0.1f; // Scale for Perlin noise

    [Header("References")]
    [SerializeField] private Tilemap tilemapTerrain;
    [SerializeField] private Tilemap tilemapGrass;
    // [SerializeField] private Tilemap tilemapFarmland;
    [SerializeField] private TileBase grassTile;
    [SerializeField] private TileBase waterTile;
    [SerializeField] private TileBase rockTile;

    public static TileType[,] TileMap { get; private set; }
    public static Plantation[,] Plantations { get; private set; }

    // instance
    public static MapController instance;

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

        // Initialize the TileMap array
        TileMap = new TileType[mapWidth, mapHeight];
        Plantations = new Plantation[mapWidth, mapHeight];
    }


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
                TileType tileType = GetTileTypeByPerlin(x, y); // Adjust coordinates to center the map
                TileBase tile = GetTileBase(tileType);
                
                tilemapGrass.SetTile(new Vector3Int(x - mapHeight / 2, y - mapHeight / 2, 0), grassTile); // Clear grass tile if not grass

                if (tileType != TileType.Grass)
                {
                    tilemapTerrain.SetTile(new Vector3Int(x - mapHeight / 2, y - mapHeight / 2, 0), tile);
                }

                TileMap[x, y] = tileType; // Store the tile type in the TileMap array
            }
        }
    }

    TileType GetTileTypeByPerlin(int x, int y)
    {
        
        if (x == mapHeight-1 || y == mapWidth-1 || x == 0 || y == 0)
        {
            return TileType.Rock; // Border tiles are rock
        }
        int circleSize = 5;
        // Circle of 5 tiles around the center are grass
        if (Mathf.Pow(x - mapWidth / 2, 2) + Mathf.Pow(y - mapHeight / 2, 2) < Mathf.Pow(circleSize, 2))
        {
            return TileType.Grass; // Grass tile
        }

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
