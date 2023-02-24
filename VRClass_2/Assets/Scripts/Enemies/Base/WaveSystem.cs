using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSystem : MonoBehaviour
{
    public static WaveSystem Instance;

    [SerializeField] private GameObject _player;
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private int _waveIndex = 0;
    [SerializeField] private int _startEnemyCount = 6;
    [SerializeField] private int _totalEnemyCount = 6;
    [SerializeField] private int _enemiesLeft;
    [SerializeField] private int _enemiesSpawned;
    [SerializeField] private int _enemySpawnCap = 15;
    [SerializeField] private int _enemyAmountIndexer = 4;
    [SerializeField] private bool _roundOver;
    [SerializeField] private float _roundStartDelay = 5;
    private float _roundStartDelayTimer;

    [SerializeField] private EnemySpawner[] enemySpawners = new EnemySpawner[0];

    public GameObject Player { get => _player; }


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
        if (_waveIndex == 0)
        {
            ResetRounds();
        }
        else
        {
            InitiateEnemies();
        }
    }

    // Update is called once per frame
    void Update()
    {
        RefilSpawners();

        RoundEndChecker();

        RefreshText();
    }

    private void RoundEndChecker()
    {
        if (_enemiesLeft <= 0)
        {
            _roundOver = true;
            _enemiesLeft = 0;
            _enemiesSpawned = 0;
        }

        if (_roundOver)
        {
            if (_roundStartDelayTimer >= _roundStartDelay)
            {
                _roundStartDelayTimer = 0;
                _roundOver = false;
                NextRound();
            }
            else
            {
                _roundStartDelayTimer += Time.deltaTime;
            }
        }
    }

    private void InitiateEnemies()
    {
        if (enemySpawners.Length != 0)
        {
            _enemiesLeft = _totalEnemyCount;
            int enemiesLeft = _totalEnemyCount;
            int enemiesCap = _enemySpawnCap;
            while (enemiesLeft != 0)
            {
                int spawnerIndex = Random.Range(0, enemySpawners.Length);

                enemySpawners[spawnerIndex].SpawnEnemy();
                enemiesLeft--;
                enemiesCap--;
                _enemiesSpawned++;

                if (enemiesCap <= 0)
                {
                    break;
                }
            }
        }
    }

    private void RefilSpawners()
    {
        if (_enemiesSpawned < _enemySpawnCap && _enemiesSpawned < _enemiesLeft)
        {
            int spawnerIndex = Random.Range(0, enemySpawners.Length);
            enemySpawners[spawnerIndex].SpawnEnemy();
            _enemiesSpawned++;
        }
    }

    public void SubtractEnemyCount()
    {
        _enemiesLeft--;
        _enemiesSpawned--;
    }

    [ContextMenu("Destroy Random Enemy")]
    private void DestroyRandomEnemy()
    {
        bool gotOne = false;
        foreach (var spawner in enemySpawners)
        {
            foreach (var enemy in spawner.SpawnedEnemies)
            {
                if (!gotOne)
                {
                    Destroy(enemy);
                    gotOne = true;
                }
            }
        }
    }

    [ContextMenu("Destroy All Enemies")]
    private void DestroyAllEnemies()
    {
        for (int a = 0; a < enemySpawners.Length; a++)
        {
            foreach (var enemyInstance in enemySpawners[a].SpawnedEnemies)
            {
                Destroy(enemyInstance);
            }
        }
    }

    private void NextRound()
    {
        _waveIndex++;
        _totalEnemyCount = _startEnemyCount;
        _totalEnemyCount += _enemyAmountIndexer * (_waveIndex - 1);
        InitiateEnemies();
    }

    [ContextMenu("Finish Round")]
    private void FinishRound()
    {
        _enemiesLeft = 0;
        _enemiesSpawned = 0;
        DestroyAllEnemies();
    }

    [ContextMenu("Reset Rounds")]
    private void ResetRounds()
    {
        _enemiesLeft = 0;
        _enemiesSpawned = 0;
        DestroyAllEnemies();
        _waveIndex = 0;
        _totalEnemyCount = _startEnemyCount;
    }

    private void RefreshText()
    {
        _waveText.text = _waveIndex.ToString();
    }
}
