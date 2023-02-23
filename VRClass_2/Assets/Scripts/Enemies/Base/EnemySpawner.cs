using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();

    public List<GameObject> SpawnedEnemies { get => spawnedEnemies;}


    public void SpawnEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity, transform);
            spawnedEnemies.Add(enemy);
        }
    }
}
