using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject LoginMenu;
    public GameObject DataToSaveMenu;
    public GameObject LeaderBoardMenu;

    public void OpenLoginMenu()
    {
        LoginMenu.SetActive(true);
        DataToSaveMenu.SetActive(false);
    }
    public void OpenSaveData()
    {
        LoginMenu.SetActive(false);
        DataToSaveMenu.SetActive(true);
    }
    public void OpenLeaderBoard()
    {
        LeaderBoardMenu.SetActive(true);
        DataToSaveMenu.SetActive(false);
    }
}
