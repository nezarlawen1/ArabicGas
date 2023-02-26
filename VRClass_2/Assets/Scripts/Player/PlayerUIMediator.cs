using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIMediator : MonoBehaviour
{
    public static PlayerUIMediator Instance;

    [SerializeField] private GameObject _instaKillIcon, _doublePointsIcon;

    public GameObject InstaKillIcon { get => _instaKillIcon;}
    public GameObject DoublePointsIcon { get => _doublePointsIcon;}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _instaKillIcon.SetActive(false);
        _doublePointsIcon.SetActive(false);
    }
}
