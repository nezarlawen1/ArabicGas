using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndlessTerrain : MonoBehaviour
{
    const float _scale = 2f;

    private const float _moveThresholdToUpdateChunks = 25f;
    private const float _sqrMoveThresholdToUpdateChunks = _moveThresholdToUpdateChunks * _moveThresholdToUpdateChunks;

    public LODInfo[] DetailLevels;
    public static float MaxViewDist;

    public Transform Viewer;
    public Material MapMaterial;

    public static Vector2 ViewerPosition;
    private Vector2 _viewerPositionOld;

    private static MapGenerator _mapGenerator;

    private int _chunkSize;
    private int _chunkVisibleInViewDist;

    Dictionary<Vector2, TerrainChunk> _terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
    static List<TerrainChunk> _terrainChunksVisibleLastUpdate = new List<TerrainChunk>();

    private void Start()
    {
        _mapGenerator = FindObjectOfType<MapGenerator>();

        MaxViewDist = DetailLevels[DetailLevels.Length - 1].VisibleDistThreshold;
        _chunkSize = MapGenerator.MapChunkSize - 1;
        _chunkVisibleInViewDist = Mathf.RoundToInt(MaxViewDist / _chunkSize);

        UpdateVisibleChunks();
    }

    private void Update()
    {
        Vector3 position = Viewer.position;
        ViewerPosition = new Vector2(position.x, position.z) / _scale;

        if ((_viewerPositionOld - ViewerPosition).sqrMagnitude > _sqrMoveThresholdToUpdateChunks)
        {
            _viewerPositionOld = ViewerPosition;
            UpdateVisibleChunks();
        }
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
                }
                else
                {
                    _terrainChunkDictionary.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord, _chunkSize, DetailLevels, transform, MapMaterial));
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
        MeshCollider _meshCollider;

        LODInfo[] _detailLevels;
        LODMesh[] _lodMeshes;
        LODMesh _collisionLODMesh;
        int _previousLODIndex = -1;

        MapData _mapData;
        bool _mapDataReceived;

        public TerrainChunk(Vector2 coord, int size, LODInfo[] detailLvls, Transform parent, Material mat)
        {
            _detailLevels = detailLvls;

            _position = coord * size;
            _bounds = new Bounds(_position, Vector2.one * size);
            Vector3 positionV3 = new Vector3(_position.x, 0, _position.y);

            _meshObj = new GameObject("Terrain Chunk");
            _meshRenderer = _meshObj.AddComponent<MeshRenderer>();
            _meshFilter = _meshObj.AddComponent<MeshFilter>();
            _meshCollider = _meshObj.AddComponent<MeshCollider>();
            _meshRenderer.material = mat;

            _meshObj.transform.position = positionV3 * _scale;
            _meshObj.transform.parent = parent;
            _meshObj.transform.localScale = Vector3.one * _scale;
            SetVisible(false);

            _lodMeshes = new LODMesh[detailLvls.Length];
            for (int i = 0; i < detailLvls.Length; i++)
            {
                _lodMeshes[i] = new LODMesh(detailLvls[i].LOD, UpdateTerrainChunk);
                if (detailLvls[i].UseForCollider)
                {
                    _collisionLODMesh = _lodMeshes[i];
                }
            }

            _mapGenerator.RequestMapData(_position, OnMapDataReceived);
        }

        private void OnMapDataReceived(MapData mapData)
        {
            _mapData = mapData;
            _mapDataReceived = true;

            Texture2D texture = TextureGenerator.TextureFromColorMap(mapData.ColorMap, MapGenerator.MapChunkSize, MapGenerator.MapChunkSize);
            _meshRenderer.material.mainTexture = texture;

            UpdateTerrainChunk();
        }

        public void UpdateTerrainChunk()
        {
            if (_mapDataReceived)
            {
                float viewerDistFromNearestEdge = _bounds.SqrDistance(ViewerPosition);
                bool visible = viewerDistFromNearestEdge <= MaxViewDist * MaxViewDist;

                if (visible)
                {
                    int lodIndex = 0;
                    for (int i = 0; i < _detailLevels.Length - 1; i++)
                    {
                        if (viewerDistFromNearestEdge > _detailLevels[i].VisibleDistThreshold)
                        {
                            lodIndex = i + 1;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (lodIndex != _previousLODIndex)
                    {
                        LODMesh lodMesh = _lodMeshes[lodIndex];
                        if (lodMesh.HasMesh)
                        {
                            _previousLODIndex = lodIndex;
                            _meshFilter.mesh = lodMesh.LMesh;
                        }
                        else if (!lodMesh.HasRequestedMesh)
                        {
                            lodMesh.RequestMesh(_mapData);
                        }
                    }

                    if (lodIndex == 0)
                    {
                        if (_collisionLODMesh.HasMesh)
                        {
                            _meshCollider.sharedMesh = _collisionLODMesh.LMesh;
                        }
                        else if (!_collisionLODMesh.HasRequestedMesh)
                        {
                            _collisionLODMesh.RequestMesh(_mapData);
                        }
                    }

                    _terrainChunksVisibleLastUpdate.Add(this);
                }

                SetVisible(visible);
            }
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

    class LODMesh
    {
        public Mesh LMesh;
        public bool HasRequestedMesh;
        public bool HasMesh;
        private int _lod;

        private System.Action _updateCallback;


        public LODMesh(int lod, System.Action updateCallback)
        {
            _lod = lod;
            _updateCallback = updateCallback;
        }

        private void OnMeshDataReceived(MeshData meshData)
        {
            LMesh = meshData.CreateMesh();
            HasMesh = true;

            _updateCallback();
        }

        public void RequestMesh(MapData mapData)
        {
            HasRequestedMesh = true;
            _mapGenerator.RequestMeshData(mapData, _lod, OnMeshDataReceived);
        }
    }

    [System.Serializable]
    public struct LODInfo
    {
        public int LOD;
        public float VisibleDistThreshold;
        public bool UseForCollider;
    }
}
