using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public GameObject HUD;
    public Text PosText;
    public Transform CompassPivot;

    // Update is called once per frame
    void Update()
    {
        ToggleHUD();
        RefreshPosition();
        RefreshCompass();
    }

    private void ToggleHUD()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (HUD.activeInHierarchy)
            {
                HUD.SetActive(false);
            }
            else
            {
                HUD.SetActive(true);
            }
        }
    }

    private void RefreshPosition()
    {
        Vector3 pos = transform.position;
        PosText.text = "Position: " + pos.ToString();
    }

    private void RefreshCompass()
    {
        float yRot = transform.rotation.eulerAngles.y;
        CompassPivot.rotation = Quaternion.Euler(0f, 180f, yRot);
    }
}
