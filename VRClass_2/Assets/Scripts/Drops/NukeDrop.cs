using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukeDrop : MonoBehaviour
{
    [SerializeField] private GameObject _gFX;
    [SerializeField] private int _points = 400;
    [SerializeField] private float _despawnDuration = 12f;
    private float _despawntimer;
    [SerializeField] private AudioSource _explosion;
    private bool _activated;

    private void Update()
    {
        // Despawn Timer
        if (!_activated)
        {
            _despawntimer += Time.deltaTime;
            if (_despawntimer >= _despawnDuration)
            {
                Destroy(gameObject);
            }
            else if (_despawntimer >= _despawnDuration / 10 * 6 && _despawntimer < _despawnDuration / 10 * 7)
            {
                _gFX.SetActive(false);
            }
            else if (_despawntimer >= _despawnDuration / 10 * 7 && _despawntimer < _despawnDuration / 10 * 8)
            {
                _gFX.SetActive(true);
            }
            else if (_despawntimer >= _despawnDuration / 10 * 8 && _despawntimer < _despawnDuration / 10 * 9)
            {
                _gFX.SetActive(false);
            }
            else if (_despawntimer >= _despawnDuration / 10 * 9 && _despawntimer < _despawnDuration / 10 * 10)
            {
                _gFX.SetActive(true);
            }
        }
    }

    private void Activate()
    {
        if (!_activated)
        {
            _activated = true;
            WaveSystem.Instance.DestroyAllEnemies();
            PointMediator.Instance.AddPoints(_points);
            _explosion.Play();
            _gFX.SetActive(false);
            Destroy(gameObject, 3);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Activate();
        }
    }
}
