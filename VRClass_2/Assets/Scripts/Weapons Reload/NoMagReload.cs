using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMagReload : MonoBehaviour
{
    public int BulletCount;
    [SerializeField] List<GameObject> Bullets;
    private int pointer;

    private void Start()
    {
        BulletCount = Bullets.Count;
        pointer = Bullets.Count - 1;
    }
    public void RemoveBullet()
    {
        if (BulletCount >= 0)
        {
            Bullets[pointer].SetActive(false);
            pointer--;
            BulletCount--;
        }
    }
}
