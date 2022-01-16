using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] public int maxHP = 100;
    public int currentHP = 100;

    public Image currentHPBar;
    public Image currentPixelHeartHPBar;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHP < 0)
        {
            currentHP = 0;
            //UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
        else if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }

        float hpRatio = (float)currentHP / maxHP;
        currentHPBar.fillAmount = hpRatio;
        currentPixelHeartHPBar.fillAmount = hpRatio;
    }
}
