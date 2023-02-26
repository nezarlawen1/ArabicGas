using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RightHandGunCheck : MonoBehaviour
{
    public enum GunHeld
    {
        None,
        AK,
        M1911,
        Shotgun
    }

    public GunHeld gunHeld;
    [SerializeField] private XRDirectInteractor interactor;
    // Start is called before the first frame update
    void Start()
    {
        gunHeld = GunHeld.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (interactor.hasSelection)
        {
            switch (interactor.selectTarget.tag)
            {
                case "Ak47Gun":
                    gunHeld = GunHeld.AK;
                    break;
                case "1911Gun":
                    gunHeld = GunHeld.M1911;
                    break;
                case "ShotgunGun":
                    gunHeld = GunHeld.Shotgun;
                    break;
                default:
                    gunHeld = GunHeld.None;
                    break;
            }
        }
        else
        {
            gunHeld = GunHeld.None;
        }
    }
}
