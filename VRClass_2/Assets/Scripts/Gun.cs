using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab, shootPoint;
    [SerializeField] AudioSource _gunSound;
    public float bulletSpeed;


    private Rigidbody _rb;
    private XRGrabInteractable _interactableGun;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _interactableGun = GetComponent<XRGrabInteractable>();
        SetupInteractableEvents();
    }

    private void Update()
    {

    }


    private void SetupInteractableEvents()
    {
        _interactableGun.activated.AddListener(StartShooting);
    }

    private void StartShooting(ActivateEventArgs args)
    {
        Shoot();
    }


    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed * Time.deltaTime, ForceMode.Impulse);

        _gunSound.pitch = Random.Range(0.9f, 1.2f);
        _gunSound.Play();
    }
}
