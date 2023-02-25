using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private WaveSystem _waveSystem;
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private List<GameObject> _spawnedEnemies = new List<GameObject>();
    [SerializeField] private int _spawnCount;
    [SerializeField] private bool _empty;

    [SerializeField] private bool _canSpawn = true;
    [SerializeField] private Door _doorToAllowSpawn;

    public List<GameObject> SpawnedEnemies { get => _spawnedEnemies; }
    public bool Empty { get => _empty; }
    public bool CanSpawn { get => _canSpawn; }

    private void Awake()
    {
        _waveSystem = GetComponentInParent<WaveSystem>();
    }

    private void Update()
    {
        SpawnerStateCounter();

        SpawnStateFromDoor();
    }

    public void SpawnEnemy()
    {
        GameObject enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity, transform);
        _spawnedEnemies.Add(enemy);
        _spawnCount++;
    }

    private void SpawnerStateCounter()
    {
        for (int i = 0; i < _spawnedEnemies.Count; i++)
        {
            if (_spawnedEnemies[i] == null)
            {
                _spawnedEnemies.RemoveAt(i);
                _spawnCount--;
                _waveSystem.SubtractEnemyCount();
            }
        }

        if (_spawnCount == 0)
        {
            _empty = true;
        }
        else
        {
            _empty = false;
        }
    }

    private void SpawnStateFromDoor()
    {
        if (!_doorToAllowSpawn)
        {
            _canSpawn = true;
        }
        else
        {
            if (_doorToAllowSpawn.Open)
            {
                _canSpawn = true;
            }
            else
            {
                _canSpawn = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_canSpawn)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireCube(transform.position + new Vector3(0, 0.5f, 0), new Vector3(1, 1, 1));
    }
}
