using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Protocol
    /*
     * This is a proposal only
     Message = playerID [space] positionID

     int that represents position of the player's step.
     [0][1][2]
     [3][4][5]
     [6][7][8]
     */
    #endregion

    public bool IsPlayer1 = true;
    public bool IsPlayer1Turn = true;

    public TextMeshProUGUI TurnText;
    public TextMeshProUGUI WinText;
    public GameObject ResetButton;


    private void Start()
    {
        ResetButton.SetActive(false);
        WinText.gameObject.SetActive(false);
        networkManager.StartUDP();
    }

    public NetworkManager networkManager; //don't forget to drag in inspector

    public Text[] buttons;

    public void GotNetworkMessage(string message)
    {
        if (message == "Reset")
        {
            ResetButton.SetActive(false);
            WinText.gameObject.SetActive(false);
            foreach (var button in buttons)
            {
                button.text = "";
            }
            IsPlayer1Turn = true;
        }
        else
        {
            if (IsPlayer1)
            {
                buttons[int.Parse(message.Substring(0, 1))].text = "O";
            }
            else
            {
                buttons[int.Parse(message.Substring(0, 1))].text = "X";
            }
            Debug.Log("got network message: " + message + "\n" + message.Substring(1, 1));
            switch (message.Substring(1, 1))
            {
                case "1":
                    IsPlayer1Turn = true;
                    break;
                case "0":
                    IsPlayer1Turn = false;
                    break;
            }
        }
    }

    public void PositionClicked(int position)
    {
        //draw the shape on the UI

        //update the other player about the shape
        networkManager.SendMessage("");// your job to finish it
    }

    //for debug purpouses only
    private void Update()
    {
        TurnDisplay();
        WinCon();
    }

    public void CheckButtonClicked()
    {
        GameObject thisPosition = EventSystem.current.currentSelectedGameObject;
        if (thisPosition.GetComponentInChildren<Text>().text == "")
        {
            if (IsPlayer1 && IsPlayer1Turn)
            {
                thisPosition.GetComponentInChildren<Text>().text = "X";
                networkManager.SendMessage(thisPosition, false);
                IsPlayer1Turn = false;
            }
            else if (!IsPlayer1 && !IsPlayer1Turn)
            {
                thisPosition.GetComponentInChildren<Text>().text = "O";
                networkManager.SendMessage(thisPosition, true);
                IsPlayer1Turn = true;
            }
        }
    }

    public void ResetGame()
    {
        networkManager.SendMessage("Reset");
    }

    public void TurnDisplay()
    {
        if (IsPlayer1Turn)
        {
            TurnText.text = "X's Turn";
        }
        else
        {
            TurnText.text = "O's Turn";
        }
    }

    public void WinCon()
    {
        Debug.Log(buttons[0]);
        if (buttons[0].text == buttons[1].text && buttons[1].text == buttons[2].text && buttons[0].text != "")
        {
            Debug.Log(buttons[0].text + " Wins!");
            WinText.gameObject.SetActive(true);
            WinText.text = buttons[0].text + " Wins!";
            ResetButton.SetActive(true);
        }
        else if (buttons[3].text == buttons[4].text && buttons[4].text == buttons[5].text && buttons[3].text != "")
        {
            Debug.Log(buttons[3].text + " Wins!");
            WinText.gameObject.SetActive(true);
            WinText.text = buttons[3].text + " Wins!";
            ResetButton.SetActive(true);
        }
        else if (buttons[6].text == buttons[7].text && buttons[7].text == buttons[8].text && buttons[6].text != "")
        {
            Debug.Log(buttons[6].text + " Wins!");
            WinText.gameObject.SetActive(true);
            WinText.text = buttons[6].text + " Wins!";
            ResetButton.SetActive(true);
        }
        else if (buttons[0].text == buttons[3].text && buttons[3].text == buttons[6].text && buttons[0].text != "")
        {
            Debug.Log(buttons[0].text + " Wins!");
            WinText.gameObject.SetActive(true);
            WinText.text = buttons[0].text + " Wins!";
            ResetButton.SetActive(true);
        }
        else if (buttons[1].text == buttons[4].text && buttons[4].text == buttons[7].text && buttons[1].text != "")
        {
            Debug.Log(buttons[1].text + " Wins!");
            WinText.gameObject.SetActive(true);
            WinText.text = buttons[1].text + " Wins!";
            ResetButton.SetActive(true);
        }
        else if (buttons[2].text == buttons[5].text && buttons[5].text == buttons[8].text && buttons[2].text != "")
        {
            Debug.Log(buttons[2].text + " Wins!");
            WinText.gameObject.SetActive(true);
            WinText.text = buttons[2].text + " Wins!";
            ResetButton.SetActive(true);
        }
        else if (buttons[0].text == buttons[4].text && buttons[4].text == buttons[8].text && buttons[0].text != "")
        {
            Debug.Log(buttons[0].text + " Wins!");
            WinText.gameObject.SetActive(true);
            WinText.text = buttons[0].text + " Wins!";
            ResetButton.SetActive(true);
        }
        else if (buttons[2].text == buttons[4].text && buttons[4].text == buttons[6].text && buttons[2].text != "")
        {
            Debug.Log(buttons[2].text + " Wins!");
            WinText.gameObject.SetActive(true);
            WinText.text = buttons[2].text + " Wins!";
            ResetButton.SetActive(true);
        }
    }
}
