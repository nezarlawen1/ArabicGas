using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private WaveSystem _waveSystem;
    private GameObject _player;
    [SerializeField] private Transform _spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        _waveSystem = WaveSystem.Instance;
        _player = _waveSystem.Player;
        _waveSystem.CanSpawn = false;
    }

    [ContextMenu("Begin Game")]
    public void BeginGame()
    {
        _player.transform.SetPositionAndRotation(_spawnPos.position, _spawnPos.rotation);
        _waveSystem.CanSpawn = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_spawnPos.position, 1);
    }
}
