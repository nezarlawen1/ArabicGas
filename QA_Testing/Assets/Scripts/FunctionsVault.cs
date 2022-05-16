using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerParams
{
    public int HP;
}

public class FunctionsVault : MonoBehaviour
{
    public int PlayerHP = 10;
    private static string saveFolder;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitSaveSystem()
    {
        saveFolder = Application.dataPath + "/PlayerParams/";
        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
        }
    }

    public void DataReset()
    {
        PlayerParams pp = new PlayerParams();
        pp.HP = 0;

        string pp_json = JsonUtility.ToJson(pp);
        Debug.Log(pp_json);

        Debug.Log("Cleared Data");


        File.WriteAllText(saveFolder + "/Player.json", pp_json);
    }

    public void SaveToJson()
    {
        PlayerParams pp = new PlayerParams();
        pp.HP = PlayerHP;

        string pp_json = JsonUtility.ToJson(pp);
        Debug.Log(pp_json);

        Debug.Log("Saved HP");


        File.WriteAllText(saveFolder + "/Player.json", pp_json);
    }

    public string LoadFromJson()
    {
        if (File.Exists(saveFolder + "/Player.json"))
        {
            Debug.Log("Loaded HP");
            var load = File.ReadAllText(saveFolder + "/Player.json");
            Debug.Log(load);
            PlayerParams pp = JsonUtility.FromJson<PlayerParams>(load);
            PlayerHP = pp.HP;
            return load;
        }
        else
        {
            return null;
        }
    }

    public int RetrieveSaveFileHP()
    {
        if (File.Exists(saveFolder + "/Player.json"))
        {
            Debug.Log("Loaded Position");
            var load = File.ReadAllText(saveFolder + "/Player.json");
            Debug.Log(load);
            PlayerParams pp = JsonUtility.FromJson<PlayerParams>(load);
            return pp.HP;
        }
        else
        {
            return 0;
        }
    }

    //public bool MyFunc()
    //{
    //    float tryVar = Random.Range(0, 3);
    //    if (tryVar > 1)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
}
