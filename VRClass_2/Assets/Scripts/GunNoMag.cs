using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

public class GunNoMag : MonoBehaviour
{
    [Header("GameObjects References")]
    [SerializeField] GameObject bulletPrefab, shootPoint, _magPrefab;
    [SerializeField] NoMagReload Magazine;

    [Header("Sounds")]
    [SerializeField] AudioSource _gunSound;
    [SerializeField] AudioClip _gunFire;
    [SerializeField] AudioClip _magazineLoad;
    [SerializeField] AudioClip _cockSound;

    [Header("Variables")]
    public float Force;
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

    [SerializeField] float SpreadAngle;
    List<Quaternion> Pellets;
    private void Awake()
    {
        Pellets = new List<Quaternion>(10);
        for (int i = 0; i < 10; i++)
        {
            Pellets.Add(Quaternion.Euler(Vector3.zero));
        }


        //RightHandTrig = GameObject.Find("RightHand Controller").GetComponent<HandsTriggerCheck>();
        //LeftHandTrig = GameObject.Find("LeftHand Controller").GetComponent<HandsTriggerCheck>();
        //recoilBody = RightHandTrig.GetComponentInChildren<Rigidbody>();

        _interactableGun = GetComponent<XRGrabInteractable>();
        //Magazine = interactor.selectTarget.gameObject.GetComponent<Magazine>();
        SetupInteractableEvents();
    }
    private void Update()
    {
        if (RightHandTrig != null)
        {
            if ((RightHandTrig.SliderTriggered || LeftHandTrig.SliderTriggered) && ControllerTriggerPushed)
            {
                CockedGun = true;
            }
        }

        if (!isMagIn)
            CockedGun = false;
    }

    public void isMagazineIn(bool state)
    {
        Magazine.ActivateBullet();
        //interactor.selectTarget.gameObject.SetActive(false);
        Destroy(interactor.selectTarget.gameObject);
        _gunSound.PlayOneShot(_magazineLoad, 1);
        isMagIn = state;
    }
    public void IsControllerTriggerPushed(bool state)
    {
        ControllerTriggerPushed = state;
    }
    public void SetMagazine()
    {
        //Magazine = interactor.selectTarget.gameObject.GetComponent<Magazine>();
    }
    public void CreateMag(Transform transform)
    {
        Instantiate(_magPrefab, transform.position, transform.rotation, null);
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
        if (Magazine.BulletCount > 0)
        {
            /*GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(shootPoint.transform.forward * bulletSpeed * Time.deltaTime, ForceMode.Impulse);
            Magazine.RemoveBullet();
            recoilBody.AddForce(-shootPoint.transform.forward * Force, ForceMode.Impulse);
            recoilBody.transform.localRotation = Quaternion.AngleAxis(-10 * Force, Vector3.right);
            _gunSound.pitch = Random.Range(0.9f, 1.2f);
            _gunSound.PlayOneShot(_gunFire, 1);*/

            for (int i = 0; i < Pellets.Count; i++)
            {
                Pellets[i] = Random.rotation;
                GameObject p = Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
                p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, Pellets[i], SpreadAngle);
                p.GetComponent<Rigidbody>().AddForce(p.transform.forward * bulletSpeed);
            }
            if (recoilBody != null)
            {
                recoilBody.AddForce(-shootPoint.transform.forward * Force, ForceMode.Impulse);
                recoilBody.transform.localRotation = Quaternion.AngleAxis(-10 * Force, Vector3.right);
            }
            Magazine.RemoveBullet();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (RightHandTrig == null && other.CompareTag("Player"))
        {
            var handTriggers = other.GetComponentsInChildren<HandsTriggerCheck>();
            foreach (var item in handTriggers)
            {
                if (item.gameObject.name == "RightHand Controller")
                {
                    RightHandTrig = item;
                }
                else
                {
                    LeftHandTrig = item;
                }
            }
            recoilBody = RightHandTrig.GetComponentInChildren<Rigidbody>();
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(shootPoint.transform.position, shootPoint.transform.forward, Color.blue);
    }
    private void OnValidate()
    {
        _rb = GetComponent<Rigidbody>();
        _interactableGun = GetComponent<XRGrabInteractable>();
    }
}
