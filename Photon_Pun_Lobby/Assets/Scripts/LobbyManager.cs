using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField roomToCreateJoin;
    [SerializeField] Text roomListText;
    [SerializeField] List<string> roomListNames;
    private RoomList roomListRef;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        roomListRef = FindObjectOfType<RoomList>();
        roomListNames = roomListRef.roomNames;
    }

    // Update is called once per frame
    void Update()
    {
        RefreshRoomsListText();
    }

    public void UIJoinRoom()
    {
        string roomName = roomToCreateJoin.text;
        PhotonNetwork.JoinRoom(roomName);
    }

    public void UICreateRoom()
    {
        string roomName = roomToCreateJoin.text;
        PhotonNetwork.CreateRoom(roomName);
        //roomListNames.Add(roomName);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected To Master");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Joined Lobby");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined Room");
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("Created Room");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("Room Creation Failed: " + returnCode + "," + message);
    }

    //public override void OnRoomListUpdate(List<RoomInfo> roomList)
    //{
    //    base.OnRoomListUpdate(roomList);
    //    Debug.Log("Updated List");
    //    foreach (RoomInfo ri in roomList)
    //    {
    //        Debug.Log(ri.Name);
    //        roomListRef.roomNames.Clear();
    //        roomListRef.roomNames.Add(ri.Name);
    //        if (ri.PlayerCount == 0)
    //        {
    //            roomListRef.roomNames.Remove(ri.Name);
    //        }
    //    }
    //    if (roomList.Count == 0)
    //    {
    //        roomListRef.roomNames.Clear();
    //    }
    //    RefreshRoomsListText();
    //}

    public void RefreshRoomsListText()
    {
        roomListNames = roomListRef.roomNames;

        roomListText.text = ("List Of Rooms:");
        foreach (var item in roomListNames)
        {
            roomListText.text += "\n" + item;
        }
    }
}