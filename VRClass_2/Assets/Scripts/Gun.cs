using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab, shootPoint;
    [SerializeField] AudioSource _gunSound;
    [SerializeField] Magazine Magazine;
    [SerializeField] XRSocketInteractorTag interactor;
    [SerializeField] GameObject Slider;
    public float bulletSpeed;
    public bool isMagIn = false;

    private Rigidbody _rb;
    private XRGrabInteractable _interactableGun;
    private bool CockedGun = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _interactableGun = GetComponent<XRGrabInteractable>();
        //Magazine = interactor.selectTarget.gameObject.GetComponent<Magazine>();
        SetupInteractableEvents();
    }
    private void Update()
    {
        if (Slider.transform.localPosition.y >= 1 && Slider.transform.localPosition.y <= 1.2)
        {
            CockedGun = true;
        }
        if (!isMagIn)
            CockedGun = false;
    }
    public void isMagazineIn(bool state)
    {
        isMagIn = state;
    }
    public void SetMagazine()
    {
        Magazine = interactor.selectTarget.gameObject.GetComponent<Magazine>();
    }
    private void SetupInteractableEvents()
    {
        _interactableGun.activated.AddListener(StartShooting);
    }

    private void StartShooting(ActivateEventArgs args)
    {
        if (isMagIn && CockedGun)
            Shoot();
    }

    public void Shoot()
    {
        if (Magazine.BulletCount >0) 
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(shootPoint.transform.forward * bulletSpeed * Time.deltaTime, ForceMode.Impulse);
            Magazine.RemoveBullet();
            _gunSound.pitch = Random.Range(0.9f, 1.2f);
            _gunSound.Play();
        }
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(shootPoint.transform.position, shootPoint.transform.forward, Color.blue);
    }
}
