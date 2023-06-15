using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColorMap };
    public DrawMode DrawModeType;


    public int MapWidth;
    public int MapHeight;
    public float NoiseScale;

    public int Octaves;
    [Range(0, 1)]
    public float Persistance;
    public float Lacunarity;

    public int Seed;
    public Vector2 Offset;

    public bool AutoUpdate;

    public TerrainType[] Regions;

    [SerializeField] MapDisplay _mapDisplay;


    private void OnValidate()
    {
        if (MapWidth < 1) MapWidth = 1;
        if (MapHeight < 1) MapHeight = 1;
        if (Octaves < 0) Octaves = 0;
        if (Lacunarity < 1) Lacunarity = 1;
    }


    [ContextMenu("Generate")]
    public void GenerateMap()
    {
        if (MapWidth > 0 && MapHeight > 0)
        {
            // Generating the Map Height Array
            float[,] noiseMap = Noise.GenerateNoiseMap(MapWidth, MapHeight, Seed, NoiseScale, Octaves, Persistance, Lacunarity, Offset);

            // Setting Regions from Heights / Via Color Map
            Color[] colorMap = new Color[MapWidth * MapHeight];
            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    float currentHeight = noiseMap[x, y];
                    for (int r = 0; r < Regions.Length; r++)
                    {
                        if (currentHeight <= Regions[r].Height)
                        {
                            colorMap[y * MapWidth + x] = Regions[r].TColor;
                            break;
                        }
                    }
                }
            }

            // Displaying the Map
            if (_mapDisplay)
            {
                if (DrawModeType == DrawMode.NoiseMap)
                {
                    _mapDisplay.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
                }
                else if (DrawModeType == DrawMode.ColorMap)
                {
                    _mapDisplay.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, MapWidth, MapHeight));
                }
            }
        }
    }
}


[System.Serializable]
public struct TerrainType
{
    public string Name;
    [Range(0, 1)]
    public float Height;
    public Color TColor;
}
