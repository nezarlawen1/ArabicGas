using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerParams
{
    public Vector3 pos;
}

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public KeyCode savingKey = KeyCode.Alpha1;
    public KeyCode loadingKey = KeyCode.Alpha2;
    public KeyCode toggleAutoKey = KeyCode.T;

    private bool AutoEnabled = true;

    private static string saveFolder;

    private void Awake()
    {
        saveFolder = Application.dataPath + "/PlayerParams/";
        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
        }
        LoadFromJson();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(toggleAutoKey))
        {
            ToggleAutoSave();
        }
        if (AutoEnabled == true)
        {
            StartCoroutine(AutoSave());
        }
        else
        {
            if (Input.GetKey(savingKey))
            {
                SaveToJson();
            }
            if (Input.GetKey(loadingKey))
            {
                LoadFromJson();
            }
        }
    }

    IEnumerator AutoSave()
    {
        SaveToJson();
        yield return new WaitForEndOfFrame();
    }

    private void SaveToJson()
    {
        PlayerParams pp = new PlayerParams();
        pp.pos = Player.transform.position;

        string pp_json = JsonUtility.ToJson(pp);
        Debug.Log(pp_json);

        PlayerParams ppSave = JsonUtility.FromJson<PlayerParams>(pp_json);
        Debug.Log("Saved Position");


        File.WriteAllText(saveFolder + "/Player.json", pp_json);
    }

    private string LoadFromJson()
    {
        if (File.Exists(saveFolder + "/Player.json"))
        {
            Debug.Log("Loaded Position");
            var load = File.ReadAllText(saveFolder + "/Player.json");
            Debug.Log(load);
            PlayerParams pp = JsonUtility.FromJson<PlayerParams>(load);
            Player.transform.position = pp.pos;
            return load;
        }
        else
        {
            return null;
        }
    }

    public void ToggleAutoSave()
    {
        if (AutoEnabled == true)
        {
            AutoEnabled = false;
            Debug.Log("Auto Save: " + AutoEnabled);
        }
        else
        {
            AutoEnabled = true;
            Debug.Log("Auto Save: " + AutoEnabled);
        }
    }
}
