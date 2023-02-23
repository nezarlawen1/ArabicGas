using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSystem : MonoBehaviour
{
    public static WaveSystem Instance;

    [SerializeField] private GameObject _player;
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private int _waveIndex = 1;
    [SerializeField] private int _startEnemyCount = 6;
    [SerializeField] private int _totalEnemyCount = 6;
    [SerializeField] private int _spawnAtOnceMax = 2;
    [SerializeField] private int _enemySpawnCap = 15;
    [SerializeField] private int _enemyAmountIndexer = 4;

    [SerializeField] private EnemySpawner[] enemySpawners = new EnemySpawner[0];

    public GameObject Player { get => _player;}


    private void OnValidate()
    {
        _totalEnemyCount = _startEnemyCount;
        _totalEnemyCount += _enemyAmountIndexer * (_waveIndex - 1);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _player = GameObject.FindGameObjectWithTag("Player");

        enemySpawners = GetComponentsInChildren<EnemySpawner>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitiateEnemies();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitiateEnemies()
    {

        if (enemySpawners.Length != 0)
        {
            int enemiesLeft = _totalEnemyCount;
            while (enemiesLeft != 0)
            {
                for (int i = 0; i < enemySpawners.Length; i++)
                {
                    if (enemiesLeft - _spawnAtOnceMax >= 0)
                    {
                        enemiesLeft -= _spawnAtOnceMax;
                        enemySpawners[i].SpawnEnemies(_spawnAtOnceMax);
                    }
                    else if (enemiesLeft > 0 && enemiesLeft - _spawnAtOnceMax < 0)
                    {
                        enemiesLeft = 0;
                        enemySpawners[i].SpawnEnemies(Mathf.Abs(enemiesLeft - _spawnAtOnceMax));
                        return;
                    }
                }
            }

        }
    }

    private void DestroyAllEnemies()
    {
        for (int a = 0; a < enemySpawners.Length; a++)
        {
            foreach (var enemyInstance in enemySpawners[a].SpawnedEnemies)
            {
                Destroy(enemyInstance);
            }
            enemySpawners[a].SpawnedEnemies.Clear();
        }
    }

    [ContextMenu("Next Round")]
    private void NextRound()
    {
        DestroyAllEnemies();
        _waveIndex++;
        _totalEnemyCount = _startEnemyCount;
        _totalEnemyCount += _enemyAmountIndexer * (_waveIndex - 1);
        InitiateEnemies();
    }

    [ContextMenu("Reset Rounds")]
    private void ResetRounds()
    {
        DestroyAllEnemies();
        _waveIndex = 1;
        _totalEnemyCount = _startEnemyCount;
        InitiateEnemies();
    }
}
