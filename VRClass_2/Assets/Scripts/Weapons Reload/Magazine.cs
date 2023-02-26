using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    public int BulletCount;
    [SerializeField] List<GameObject> Bullets;
    private int pointer;
    // Start is called before the first frame update
    private void Awake()
    {
        BulletCount = Bullets.Count;
    }
    void Start()
    {
        foreach (var bullet in Bullets)
        {
            bullet.SetActive(true);
        }

        pointer = Bullets.Count - 1;
    }
    public void SetBulletCount(int count)
    {
        BulletCount = count;
        for (int i = 0; i < Bullets.Count; i++)
        {
            if (i >= count)
                Bullets[i].SetActive(false);
        }
        pointer = count - 1;
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
