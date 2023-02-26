using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class Pouch : MonoBehaviour
{
    [SerializeField] private XRSocketInteractorTag Socket;
    [SerializeField] private GameObject LeftHand;
    [SerializeField] private GameObject RightHand;

    [Header("Ammo Amount")]
    public int PistolAmmo = 0;
    public int ARAmmo = 0;
    public int ShotgunAmmo = 0;

    [Header("Ammo Text Refs")]
    [SerializeField] private TextMeshProUGUI PistolAmmoNum;
    [SerializeField] private TextMeshProUGUI ARAmmoNum;
    [SerializeField] private TextMeshProUGUI ShotgunAmmoNum;

    [Header("Maga Prefabs")]
    [SerializeField] private GameObject Mag1911;
    [SerializeField] private GameObject MagAK47;
    [SerializeField] private GameObject MagShotgun;

    public bool Reload = false;

    private int pistolMaxAmmo = 8;
    private int arMaxAmmo = 30;
    private int shotgunMaxAmmo = 1;

    // Start is called before the first frame update
    void Start()
    {
        SetBulletAmmount();

    }
    private void SetBulletAmmount()
    {
        //PistolAmmoNum = LeftHand.GetComponent
        PistolAmmoNum.text = PistolAmmo.ToString();
        ARAmmoNum.text = ARAmmo.ToString();
        ShotgunAmmoNum.text = ShotgunAmmo.ToString();
    }

    public void IsMagIn()
    {
        /*if (!Reload)
        {*/
        if (Socket.selectTarget.gameObject.TryGetComponent(out Magazine mag))
        {
            switch (Socket.selectTarget.gameObject.tag)
            {
                case "Mag1911":
                    PistolAmmo += mag.BulletCount;
                    SetBulletAmmount();
                    break;
                case "MagAK47":
                    ARAmmo += mag.BulletCount;
                    SetBulletAmmount();
                    break;
            }
        }
        else if (Socket.selectTarget.gameObject.TryGetComponent(out NoMagReload noMag))
        {
            if (Socket.selectTarget.gameObject.tag == "MagShotGun")
            {
                ShotgunAmmo++;
                SetBulletAmmount();
            }
        }

        Destroy(Socket.selectTarget.gameObject);
        //}
    }

    public void IsMagOut()
    {
        //Reload = true;
        GetComponent<Collider>().enabled = false;
        switch (RightHand.GetComponent<RightHandGunCheck>().gunHeld)
        {
            case RightHandGunCheck.GunHeld.AK:
                if (ARAmmo > 0)
                {
                    GetComponent<XRGrabInteractable>().enabled = false;
                    GameObject newMagAK47 = Instantiate(MagAK47, transform.position, Quaternion.identity);
                    if (ARAmmo < arMaxAmmo && ARAmmo > 0)
                    {
                        newMagAK47.GetComponent<Magazine>().SetBulletCount(ARAmmo);
                        ARAmmo = 0;
                    }
                    else
                    {
                        ARAmmo -= arMaxAmmo;
                    }
                }
                StartCoroutine(TurnOnGrab());
                break;
            case RightHandGunCheck.GunHeld.M1911:
                if (PistolAmmo > 0)
                {
                    GetComponent<XRGrabInteractable>().enabled = false;
                    GameObject newMag1911 = Instantiate(Mag1911, transform.position, Quaternion.identity);
                    if (PistolAmmo < pistolMaxAmmo && PistolAmmo > 0)
                    {
                        newMag1911.GetComponent<Magazine>().SetBulletCount(PistolAmmo);
                        PistolAmmo = 0;
                    }
                    else
                    {
                        PistolAmmo -= pistolMaxAmmo;
                    }
                }
                StartCoroutine(TurnOnGrab());
                break;
            case RightHandGunCheck.GunHeld.Shotgun:
                if (ShotgunAmmo > 0)
                {
                    GetComponent<XRGrabInteractable>().enabled = false;
                    GameObject newMagShotgun = Instantiate(MagShotgun, transform.position, Quaternion.identity);
                    ShotgunAmmo -= shotgunMaxAmmo;
                }
                StartCoroutine(TurnOnGrab());
                break;
        }
        SetBulletAmmount();
    }

    IEnumerator TurnOnGrab()
    {
        yield return new WaitForSeconds(2);
        GetComponent<XRGrabInteractable>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }
}
