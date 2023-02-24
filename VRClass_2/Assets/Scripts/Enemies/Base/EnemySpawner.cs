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

    public List<GameObject> SpawnedEnemies { get => _spawnedEnemies; }
    public bool Empty { get => _empty; }

    private void Awake()
    {
        _waveSystem = GetComponentInParent<WaveSystem>();
    }

    private void Update()
    {
        SpawnerStateCounter();
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
}
