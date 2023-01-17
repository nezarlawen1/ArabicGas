using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMagReload : MonoBehaviour
{
    public int BulletCount = 0;
    [SerializeField] List<GameObject> Bullets;
    //private int pointer;
    private int i = -1;

    private void Start()
    {
        //BulletCount = Bullets.Count;
        //pointer = Bullets.Count - 1;
    }
    public void ActivateBullet()
    {
        Bullets[++i].SetActive(true);
        BulletCount++;
    }
    public void RemoveBullet()
    {
        if (BulletCount >= 0)
        {
            Bullets[i--].SetActive(false);
            //pointer--;
            BulletCount--;
            //i--;
        }
    }
}
