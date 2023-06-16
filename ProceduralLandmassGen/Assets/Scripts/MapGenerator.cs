using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColorMap, Mesh, FalloffMap };
    public DrawMode DrawModeType;

    public Noise.NormalizeMode NormalizeType;


    public const int MapChunkSize = 241;
    [Range(0, 6)]
    public int PreviewLOD;
    public float NoiseScale;

    public int Octaves;
    [Range(0, 1)]
    public float Persistance;
    public float Lacunarity;

    public int Seed;
    public Vector2 Offset;

    public bool UseFalloff;

    public float MeshHeightMultiplier;
    public AnimationCurve MeshHeightCurve;

    public bool AutoUpdate;

    public TerrainType[] Regions;

    [SerializeField] MapDisplay _mapDisplay;


    private float[,] _falloffMap;

    private Queue<MapThreadInfo<MapData>> _mapDataThreadInfoQueue = new Queue<MapThreadInfo<MapData>>();
    private Queue<MapThreadInfo<MeshData>> _meshDataThreadInfoQueue = new Queue<MapThreadInfo<MeshData>>();


    private void OnValidate()
    {
        if (Octaves < 0) Octaves = 0;
        if (Lacunarity < 1) Lacunarity = 1;

        _falloffMap = FalloffGenerator.GenerateFalloffMap(MapChunkSize);
    }

    private void Awake()
    {
        _falloffMap = FalloffGenerator.GenerateFalloffMap(MapChunkSize);
    }

    private void Update()
    {
        RunInUpdate();
    }


    public void DisplayMap()
    {
        MapData mapData = GenerateMapData(Vector2.zero);

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
                _mapDisplay.DrawMesh(MeshGenerator.GenerateTerrainMesh(mapData.HeightMap, MeshHeightMultiplier, MeshHeightCurve, PreviewLOD), TextureGenerator.TextureFromColorMap(mapData.ColorMap, MapChunkSize, MapChunkSize));
            }
            else if (DrawModeType == DrawMode.FalloffMap)
            {
                _mapDisplay.DrawTexture(TextureGenerator.TextureFromHeightMap(FalloffGenerator.GenerateFalloffMap(MapChunkSize)));
            }
        }
    }


    // Requesting Map Data & Running the Thread
    public void RequestMapData(Vector2 center, Action<MapData> callback)
    {
        ThreadStart threadStart = delegate
        {
            MapDataThread(center, callback);
        };

        new Thread(threadStart).Start();
    }
    // Thread Action for Map Data
    private void MapDataThread(Vector2 center, Action<MapData> callback)
    {
        MapData mapData = GenerateMapData(center);
        lock (_mapDataThreadInfoQueue)
        {
            _mapDataThreadInfoQueue.Enqueue(new MapThreadInfo<MapData>(callback, mapData));
        }
    }

    // Requesting Mesh Data & Running the Thread
    public void RequestMeshData(MapData mapData, int lod, Action<MeshData> callback)
    {
        ThreadStart threadStart = delegate
        {
            MeshDataThread(mapData, lod, callback);
        };

        new Thread(threadStart).Start();
    }
    // Thread Action for Mesh Data
    private void MeshDataThread(MapData mapData, int lod, Action<MeshData> callback)
    {
        MeshData meshData = MeshGenerator.GenerateTerrainMesh(mapData.HeightMap, MeshHeightMultiplier, MeshHeightCurve, lod);
        lock (_meshDataThreadInfoQueue)
        {
            _meshDataThreadInfoQueue.Enqueue(new MapThreadInfo<MeshData>(callback, meshData));
        }
    }



    private void RunInUpdate()
    {
        if (_mapDataThreadInfoQueue.Count > 0)
        {
            for (int ma = 0; ma < _mapDataThreadInfoQueue.Count; ma++)
            {
                MapThreadInfo<MapData> threadInfoMa = _mapDataThreadInfoQueue.Dequeue();
                threadInfoMa.Callback(threadInfoMa.Parameter);
            }
        }

        if (_meshDataThreadInfoQueue.Count > 0)
        {
            for (int me = 0; me < _meshDataThreadInfoQueue.Count; me++)
            {
                MapThreadInfo<MeshData> threadInfoMe = _meshDataThreadInfoQueue.Dequeue();
                threadInfoMe.Callback(threadInfoMe.Parameter);
            }
        }
    }

    private MapData GenerateMapData(Vector2 center)
    {
        if (MapChunkSize > 0)
        {
            // Generating the Map Height Array
            float[,] noiseMap = Noise.GenerateNoiseMap(MapChunkSize, MapChunkSize, Seed, NoiseScale, Octaves, Persistance, Lacunarity, center + Offset, NormalizeType);

            // Setting Regions from Heights / Via Color Map
            Color[] colorMap = new Color[MapChunkSize * MapChunkSize];
            for (int y = 0; y < MapChunkSize; y++)
            {
                for (int x = 0; x < MapChunkSize; x++)
                {
                    if (UseFalloff)
                    {
                        noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - _falloffMap[x, y]);
                    }
                    float currentHeight = noiseMap[x, y];
                    for (int r = 0; r < Regions.Length; r++)
                    {
                        if (currentHeight >= Regions[r].Height)
                        {
                            colorMap[y * MapChunkSize + x] = Regions[r].TColor;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            return new MapData(noiseMap, colorMap);
        }
    }

    private struct MapThreadInfo<T>
    {
        public readonly Action<T> Callback;
        public readonly T Parameter;

        public MapThreadInfo(Action<T> callback, T parameter)
        {
            Callback = callback;
            Parameter = parameter;
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
    public readonly float[,] HeightMap;
    public readonly Color[] ColorMap;

    public MapData(float[,] heightMap, Color[] colorMap)
    {
        HeightMap = heightMap;
        ColorMap = colorMap;
    }
}
