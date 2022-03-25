using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        networkManager.StartUDP();
    }

    public NetworkManager networkManager; //don't forget to drag in inspector

    public Text[] buttons;

    public void GotNetworkMessage(string message)
    {
        if (IsPlayer1)
        {
            buttons[int.Parse(message)].text = "O";
        }
        else
        {
            buttons[int.Parse(message)].text = "X";
        }
        Debug.Log("got network message: " + message);
        switch (message)
        {
            //do something with the message
            //case 5:
            //Do something
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
        WinCon();
    }

    public void CheckButtonClicked()
    {
        GameObject thisPosition = EventSystem.current.currentSelectedGameObject;
        if (thisPosition.GetComponentInChildren<Text>().text == "")
        {
            if (IsPlayer1)
            {
                thisPosition.GetComponentInChildren<Text>().text = "X";
                networkManager.SendMessage(thisPosition);
            }
            else
            {
                thisPosition.GetComponentInChildren<Text>().text = "O";
                networkManager.SendMessage(thisPosition);
            }
        }
    }
    public void WinCon()
    {
        Debug.Log(buttons[0]);
        if (buttons[0].text == buttons[1].text && buttons[1].text == buttons[2].text && buttons[0].text != "")
        {
            Debug.Log(buttons[0].text + " u win");
        }
        else if (buttons[3].text == buttons[4].text && buttons[4].text == buttons[5].text && buttons[3].text != "")
        {
            Debug.Log(buttons[3].text + " u win");
        }
        else if (buttons[6].text == buttons[7].text && buttons[7].text == buttons[8].text && buttons[6].text != "")
        {
            Debug.Log(buttons[6].text + " u win");
        }
        else if (buttons[0].text == buttons[3].text && buttons[3].text == buttons[6].text && buttons[0].text != "")
        {
            Debug.Log(buttons[0].text + " u win");
        }
        else if (buttons[1].text == buttons[4].text && buttons[4].text == buttons[7].text && buttons[1].text != "")
        {
            Debug.Log(buttons[1].text + " u win");
        }
        else if (buttons[2].text == buttons[5].text && buttons[5].text == buttons[8].text && buttons[2].text != "")
        {
            Debug.Log(buttons[2].text + " u win");
        }
        else if (buttons[0].text == buttons[4].text && buttons[4].text == buttons[8].text && buttons[0].text != "")
        {
            Debug.Log(buttons[0].text + " u win");
        }
        else if (buttons[2].text == buttons[4].text && buttons[4].text == buttons[6].text && buttons[2].text != "")
        {
            Debug.Log(buttons[2].text + " u win");
        }
    }
}
