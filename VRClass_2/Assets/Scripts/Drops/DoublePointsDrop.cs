using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePointsDrop : MonoBehaviour
{
    [SerializeField] private GameObject _gFX;
    [SerializeField] private float _duration = 30f;
    private float _timer;    
    [SerializeField] private float _despawnDuration = 12f;
    private float _despawntimer;
    [SerializeField] private AudioSource _doubleXPSFX;
    private bool _activated;


    // Update is called once per frame
    void Update()
    {
        // Activation Timer
        if (_activated)
        {
            if (_timer >= _duration)
            {
                PointMediator.Instance._doublePoints = false;
                Destroy(gameObject,0.1f);
            }
            else
            {
                _timer += Time.deltaTime;
            }
        }
        else
        {
            _timer = 0;
        }

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
            _doubleXPSFX.Play();
            _gFX.SetActive(false);
            PointMediator.Instance._doublePoints = true;
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
