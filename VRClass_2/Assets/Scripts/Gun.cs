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
    [Header("GameObjects References")]
    [SerializeField] GameObject bulletPrefab, shootPoint;
    [SerializeField] Magazine Magazine;

    [Header("Sounds")]
    [SerializeField] AudioSource _gunSound;
    [SerializeField] AudioClip _gunFire;
    [SerializeField] AudioClip _magazineLoad;
    [SerializeField] AudioClip _cockSound;

    [Header("Variables")]
    [SerializeField] float Force;
    public float bulletSpeed;
    public bool isMagIn = false;
    public bool CockedGun = false;
    public bool ControllerTriggerPushed = false;

    [Space(5)]
    private Rigidbody _rb;
    [SerializeField] Rigidbody recoilBody;
    [SerializeField] XRSocketInteractorTag interactor;
    private XRGrabInteractable _interactableGun;

    [Header("Hands References")]
    [SerializeField] HandsTriggerCheck RightHandTrig;
    [SerializeField] HandsTriggerCheck LeftHandTrig;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _interactableGun = GetComponent<XRGrabInteractable>();
        //Magazine = interactor.selectTarget.gameObject.GetComponent<Magazine>();
        SetupInteractableEvents();
    }
    private void Update()
    {
        if ((RightHandTrig.SliderTriggered || LeftHandTrig.SliderTriggered) && ControllerTriggerPushed)
        {
            CockedGun = true;
            
        }
        if (!isMagIn)
            CockedGun = false;
    }
    public void isMagazineIn(bool state)
    {
        _gunSound.PlayOneShot(_magazineLoad, 1);
        isMagIn = state;
    }
    public void IsControllerTriggerPushed(bool state)
    {
        ControllerTriggerPushed = state;
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
            recoilBody.AddForce(-shootPoint.transform.forward * Force, ForceMode.Impulse);
            recoilBody.transform.localRotation = Quaternion.AngleAxis(-10 * Force,Vector3.right);
            _gunSound.pitch = Random.Range(0.9f, 1.2f);
            _gunSound.PlayOneShot(_gunFire, 1);
        }
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(shootPoint.transform.position, shootPoint.transform.forward, Color.blue);
    }
}
