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
        //Debug.Log(Socket.selectTarget.gameObject);
        if (Socket.selectTarget.gameObject.TryGetComponent(out Magazine mag))
        {
            Debug.Log(Socket.selectTarget.gameObject);
            switch (Socket.selectTarget.gameObject.tag)
            {
                case "Mag1911":
                    PistolAmmo += mag.BulletCount;
                    SetBulletAmmount();
                    Debug.Log("mag1911");
                    break;
                case "MagAK47":
                    Debug.Log("magAK");
                    ARAmmo += mag.BulletCount;
                    SetBulletAmmount();
                    break;
                case "MagShotGun":
                    ShotgunAmmo++;
                    SetBulletAmmount();
                    break;
            }
        }
        else if (Socket.selectTarget.gameObject.TryGetComponent(out NoMagReload noMag))
        {
            if (Socket.selectTarget.gameObject.tag == "MagShotGun")
            {
                ShotgunAmmo += noMag.BulletCount;
            }
        }

        Destroy(Socket.selectTarget.gameObject);
        SetBulletAmmount();
    }

    public void IsMagOut()
    {
        switch (RightHand.GetComponent<XRDirectInteractor>().selectTarget.gameObject.tag)
        {
            case "AK47Gun":
                GameObject newMagAK47 = Instantiate(MagAK47);
                newMagAK47.transform.SetParent(null);
                break;
            case "1911Gun":
                GameObject newMag1911 = Instantiate(Mag1911);
                newMag1911.transform.SetParent(null);
                break;
            case "ShotgunGun":
                GameObject newMagShotgun = Instantiate(MagShotgun);
                newMagShotgun.transform.SetParent(null);
                break;
        }

    }
}
