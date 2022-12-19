using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    public int BulletCount;
    [SerializeField] List<GameObject> Bullets;
    private int pointer;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var bullet in Bullets)
        {
            bullet.SetActive(true);
        }
        BulletCount = Bullets.Count;
        pointer = Bullets.Count-1;
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
