using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private int _waveIndex = 1;
    [SerializeField] private int _startEnemyCount = 6;
    [SerializeField] private int _totalEnemyCount = 6;
    [SerializeField] private int _enemySpawnCap = 15;
    [SerializeField] private int _enemyAmountIndexer = 3;

    private void OnValidate()
    {
        _totalEnemyCount = _startEnemyCount;
        _totalEnemyCount += _enemyAmountIndexer * (_waveIndex - 1);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("Next Round")]
    private void NextRound()
    {
        _waveIndex++;
        _totalEnemyCount = _startEnemyCount;
        _totalEnemyCount += _enemyAmountIndexer * (_waveIndex - 1);
    }

    [ContextMenu("Reset Rounds")]
    private void ResetRounds()
    {
        _waveIndex = 1;
        _totalEnemyCount = _startEnemyCount;
    }
}
