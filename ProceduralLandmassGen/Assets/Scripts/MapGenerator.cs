using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
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
            float[,] noiseMap = Noise.GenerateNoiseMap(MapWidth, MapHeight, Seed, NoiseScale, Octaves, Persistance, Lacunarity, Offset);

            if (_mapDisplay) _mapDisplay.DrawNoiseMap(noiseMap);
        }
    }
}
