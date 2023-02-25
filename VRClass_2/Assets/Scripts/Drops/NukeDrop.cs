using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukeDrop : MonoBehaviour
{
    [SerializeField] private GameObject _gFX;
    [SerializeField] private int _points = 400;
    [SerializeField] private AudioSource _explosion;
    private bool _activated;

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
