using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerParams
{
    public Vector3 pos;
}

public class GameManager : MonoBehaviour
{
    public KeyCode savingKey = KeyCode.Alpha1;

    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(savingKey))
        {
            SaveToJson();
        }
    }

    private void SaveToJson()
    {
        PlayerParams pp = new PlayerParams();
        pp.pos = Player.transform.position;

        string pp_json = JsonUtility.ToJson(pp);
        Debug.Log(pp_json);

        PlayerParams ppSave = JsonUtility.FromJson<PlayerParams>(pp_json);
        Debug.Log("Saved Position");

    }
}
