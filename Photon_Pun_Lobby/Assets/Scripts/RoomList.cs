using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomList : MonoBehaviourPunCallbacks
{
    private static RoomList roomListInstance;
    public List<string> roomNames = new List<string>();

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (roomListInstance == null)
        {
            roomListInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        Debug.Log("Updated List");
        foreach (RoomInfo ri in roomList)
        {
            Debug.Log(ri.Name);
            roomNames.Clear();
            roomNames.Add(ri.Name);
            if (ri.PlayerCount == 0)
            {
                roomNames.Remove(ri.Name);
            }
        }
    }
}
