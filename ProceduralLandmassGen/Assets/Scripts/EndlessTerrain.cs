using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndlessTerrain : MonoBehaviour
{
    public const float MaxViewDist = 450;
    public Transform Viewer;
    public Material MapMaterial;

    public static Vector2 ViewerPosition;

    private static MapGenerator _mapGenerator;

    private int _chunkSize;
    private int _chunkVisibleInViewDist;

    Dictionary<Vector2, TerrainChunk> _terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
    List<TerrainChunk> _terrainChunksVisibleLastUpdate = new List<TerrainChunk>();

    private void Start()
    {
        _mapGenerator = FindObjectOfType<MapGenerator>();
        _chunkSize = MapGenerator.MapChunkSize - 1;
        _chunkVisibleInViewDist = Mathf.RoundToInt(MaxViewDist / _chunkSize);
    }

    private void Update()
    {
        Vector3 position = Viewer.position;
        ViewerPosition = new Vector2(position.x, position.z);
        UpdateVisibleChunks();
    }


    void UpdateVisibleChunks()
    {
        // Disabling Previous Chunks
        for (int i = 0; i < _terrainChunksVisibleLastUpdate.Count; i++)
        {
            _terrainChunksVisibleLastUpdate[i].SetVisible(false);
        }
        _terrainChunksVisibleLastUpdate.Clear();


        int currentChunkCoordX = Mathf.RoundToInt(ViewerPosition.x / _chunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(ViewerPosition.y / _chunkSize);

        for (int yOffset = -_chunkVisibleInViewDist; yOffset <= _chunkVisibleInViewDist; yOffset++)
        {
            for (int xOffset = -_chunkVisibleInViewDist; xOffset <= _chunkVisibleInViewDist; xOffset++)
            {
                Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

                if (_terrainChunkDictionary.ContainsKey(viewedChunkCoord))
                {
                    _terrainChunkDictionary[viewedChunkCoord].UpdateTerrainChunk();
                    if (_terrainChunkDictionary[viewedChunkCoord].IsVisible())
                    {
                        _terrainChunksVisibleLastUpdate.Add(_terrainChunkDictionary[viewedChunkCoord]);
                    }
                }
                else
                {
                    _terrainChunkDictionary.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord, _chunkSize, transform, MapMaterial));
                }
            }
        }
    }

    public class TerrainChunk
    {
        GameObject _meshObj;
        Vector2 _position;
        Bounds _bounds;

        MeshRenderer _meshRenderer;
        MeshFilter _meshFilter;

        public TerrainChunk(Vector2 coord, int size, Transform parent, Material mat)
        {
            _position = coord * size;
            _bounds = new Bounds(_position, Vector2.one * size);
            Vector3 positionV3 = new Vector3(_position.x, 0, _position.y);

            _meshObj = new GameObject("Terrain Chunk");
            _meshRenderer = _meshObj.AddComponent<MeshRenderer>();
            _meshFilter = _meshObj.AddComponent<MeshFilter>();
            _meshRenderer.material = mat;

            _meshObj.transform.position = positionV3;
            _meshObj.transform.parent = parent;
            SetVisible(false);

            _mapGenerator.RequestMapData(OnMapDataReceived);
        }

        private void OnMapDataReceived(MapData mapData)
        {
            _mapGenerator.RequestMeshData(mapData, OnMeshDataReceived);
        }

        private void OnMeshDataReceived(MeshData meshData)
        {
            _meshFilter.mesh = meshData.CreateMesh();
        }

        public void UpdateTerrainChunk()
        {
            float viewerDistFromNearestEdge = _bounds.SqrDistance(ViewerPosition);
            bool visible = viewerDistFromNearestEdge <= MaxViewDist * MaxViewDist;
            SetVisible(visible);
        }

        public void SetVisible(bool visible)
        {
            _meshObj.SetActive(visible);
        }

        public bool IsVisible()
        {
            return _meshObj.activeSelf;
        }
    }
}
