using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePointsDrop : MonoBehaviour
{
    [SerializeField] private GameObject _gFX;
    [SerializeField] private float _duration = 30f;
    private float _timer;
    [SerializeField] private AudioSource _doubleXPSFX;
    private bool _activated;


    // Update is called once per frame
    void Update()
    {
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
