using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject LoginMenu;
    public GameObject RegisterMenu;
    public GameObject DataToSaveMenu;
    public GameObject LeaderBoardMenu;

    public void OpenLoginMenu()
    {
        LoginMenu.SetActive(true);
        RegisterMenu.SetActive(false);
        DataToSaveMenu.SetActive(false);
        LeaderBoardMenu.SetActive(false);
    }
    public void OpenRegisterMenu()
    {
        LoginMenu.SetActive(false);
        RegisterMenu.SetActive(true);
    }
    public void OpenSaveData()
    {
        LoginMenu.SetActive(false);
        LeaderBoardMenu.SetActive(false);
        DataToSaveMenu.SetActive(true);
    }
    public void OpenLeaderBoard()
    {
        LeaderBoardMenu.SetActive(true);
        DataToSaveMenu.SetActive(false);
    }
}
