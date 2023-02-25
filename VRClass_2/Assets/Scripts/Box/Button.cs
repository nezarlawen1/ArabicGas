using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject _button;
    [SerializeField] private AudioSource _pressSFX, _releaseSFX;
    private bool _isPressed;
    private GameObject _presser;
    public UnityEvent OnPress;
    public UnityEvent OnRelease;

    private void OnTriggerEnter(Collider other)
    {
        if (!_isPressed)
        {
            _button.transform.localPosition = new Vector3(0, 0.003f, 0);
            _presser = other.gameObject;
            if(OnPress != null) OnPress.Invoke();
            _pressSFX.Play();
            _isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _presser)
        {
            _button.transform.localPosition = new Vector3(0, 0.015f, 0);
            if (OnRelease != null) OnRelease.Invoke();
            _releaseSFX.Play();
            _isPressed = false;
        }
    }
}
