using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ScoreElement : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text killsText;
    public TMP_Text deathsText;
    public TMP_Text xpText;
    public void NewScoreElement(string _username,int _kills,int _deaths,int _xp)
    {
        usernameText.text = _username;
        killsText.text = _kills.ToString();
        deathsText.text = _deaths.ToString();
        xpText.text = _xp.ToString();
    }
}
