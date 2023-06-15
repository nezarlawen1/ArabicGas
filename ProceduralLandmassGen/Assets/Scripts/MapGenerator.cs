using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColorMap, Mesh };
    public DrawMode DrawModeType;


    const int _mapChunkSize = 241;
    [Range(0,6)]
    public int LevelOfDetail;
    public float NoiseScale;

    public int Octaves;
    [Range(0, 1)]
    public float Persistance;
    public float Lacunarity;

    public int Seed;
    public Vector2 Offset;

    public float MeshHeightMultiplier;
    public AnimationCurve MeshHeightCurve;

    public bool AutoUpdate;

    public TerrainType[] Regions;

    [SerializeField] MapDisplay _mapDisplay;


    private void OnValidate()
    {
        if (Octaves < 0) Octaves = 0;
        if (Lacunarity < 1) Lacunarity = 1;
    }


    [ContextMenu("Generate")]
    public void GenerateMap()
    {
        if (_mapChunkSize > 0 && _mapChunkSize > 0)
        {
            // Generating the Map Height Array
            float[,] noiseMap = Noise.GenerateNoiseMap(_mapChunkSize, _mapChunkSize, Seed, NoiseScale, Octaves, Persistance, Lacunarity, Offset);

            // Setting Regions from Heights / Via Color Map
            Color[] colorMap = new Color[_mapChunkSize * _mapChunkSize];
            for (int y = 0; y < _mapChunkSize; y++)
            {
                for (int x = 0; x < _mapChunkSize; x++)
                {
                    float currentHeight = noiseMap[x, y];
                    for (int r = 0; r < Regions.Length; r++)
                    {
                        if (currentHeight <= Regions[r].Height)
                        {
                            colorMap[y * _mapChunkSize + x] = Regions[r].TColor;
                            break;
                        }
                    }
                }
            }

            // Displaying the Map
            DisplayMap(noiseMap, colorMap);
        }
    }

    private void DisplayMap(float[,] noiseMap, Color[] colorMap)
    {
        if (_mapDisplay)
        {
            if (DrawModeType == DrawMode.NoiseMap)
            {
                _mapDisplay.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
            }
            else if (DrawModeType == DrawMode.ColorMap)
            {
                _mapDisplay.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, _mapChunkSize, _mapChunkSize));
            }
            else if (DrawModeType == DrawMode.Mesh)
            {
                _mapDisplay.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, MeshHeightMultiplier, MeshHeightCurve, LevelOfDetail), TextureGenerator.TextureFromColorMap(colorMap, _mapChunkSize, _mapChunkSize));
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
