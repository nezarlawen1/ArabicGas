using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONReader : MonoBehaviour
{    
    [System.Serializable]
    public class Player
    {
        public string Name;
        public int Health;
        public string PlayerItemsRef;
    }

    [System.Serializable]
    public class PlayerList
    {
        public Player[] documents;
    }


    [SerializeField] private PlayerList _playersList = new PlayerList();


    //private void Start()
    //{
    //    if (File.Exists(Application.dataPath + "/Scripts/JSONText.txt"))
    //    {
    //        string saveString = File.ReadAllText(Application.dataPath + "/Scripts/JSONText.txt");
    //        _playersList = JsonUtility.FromJson<PlayerList>(saveString);
    //    }
    //}

    public void UpdateJSONData()
    {
        if (File.Exists(Application.dataPath + "/Scripts/JSONText.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/Scripts/JSONText.txt");
            _playersList = JsonUtility.FromJson<PlayerList>(saveString);
        }
    }
}
