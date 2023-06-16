using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColorMap, Mesh };
    public DrawMode DrawModeType;


    public const int MapChunkSize = 241;
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


    public void DisplayMap()
    {
        MapData mapData = GenerateMapData();

        if (_mapDisplay)
        {
            if (DrawModeType == DrawMode.NoiseMap)
            {
                _mapDisplay.DrawTexture(TextureGenerator.TextureFromHeightMap(mapData.HeightMap));
            }
            else if (DrawModeType == DrawMode.ColorMap)
            {
                _mapDisplay.DrawTexture(TextureGenerator.TextureFromColorMap(mapData.ColorMap, MapChunkSize, MapChunkSize));
            }
            else if (DrawModeType == DrawMode.Mesh)
            {
                _mapDisplay.DrawMesh(MeshGenerator.GenerateTerrainMesh(mapData.HeightMap, MeshHeightMultiplier, MeshHeightCurve, LevelOfDetail), TextureGenerator.TextureFromColorMap(mapData.ColorMap, MapChunkSize, MapChunkSize));
            }
        }
    }

    private MapData GenerateMapData()
    {
        if (MapChunkSize > 0 && MapChunkSize > 0)
        {
            // Generating the Map Height Array
            float[,] noiseMap = Noise.GenerateNoiseMap(MapChunkSize, MapChunkSize, Seed, NoiseScale, Octaves, Persistance, Lacunarity, Offset);

            // Setting Regions from Heights / Via Color Map
            Color[] colorMap = new Color[MapChunkSize * MapChunkSize];
            for (int y = 0; y < MapChunkSize; y++)
            {
                for (int x = 0; x < MapChunkSize; x++)
                {
                    float currentHeight = noiseMap[x, y];
                    for (int r = 0; r < Regions.Length; r++)
                    {
                        if (currentHeight <= Regions[r].Height)
                        {
                            colorMap[y * MapChunkSize + x] = Regions[r].TColor;
                            break;
                        }
                    }
                }
            }

            return new MapData(noiseMap, colorMap);
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

public struct MapData
{
    public float[,] HeightMap;
    public Color[] ColorMap;

    public MapData(float[,] heightMap, Color[] colorMap)
    {
        HeightMap = heightMap;
        ColorMap = colorMap;
    }
}
