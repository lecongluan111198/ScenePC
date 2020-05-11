using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static PhotonLobby instance;
    private Queue<Action<bool>> queue = new Queue<Action<bool>>();
    private Action<bool> currentCallBack;

    private string createdRoomName;


    private void Awake()
    {
        instance = this;
        //if (instance == null)
        //{
        //    instance = this;
        //}
        //else
        //{
        //    if (instance != this)
        //    {
        //        Destroy(instance.gameObject);
        //        instance = this;
        //    }
        //}
        //DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    //this callback will be called whether connection to server is successfull
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Player has connected to the Photon master server.");
        PhotonNetwork.AutomaticallySyncScene = true;
        if (queue.Count > 0)
        {
            Action<bool> callback = queue.Dequeue();
            callback(true);
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create new room but failed. There must already be a room with same name. returnCode: " + returnCode + " message: " + message);
        OnCreatedRoom();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        foreach (RoomInfo info in roomList)
        {
            if (info.Name.EndsWith(createdRoomName))
            {
                if (info.IsOpen)
                {
                    Debug.Log("room already open");
                    //show room name

                }
                else
                {
                    //Notify error

                }
            }
        }
    }

    public void Connect(Action<bool> callback)
    {
        Debug.Log(PhotonNetwork.IsConnected);
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings(); //Connects to Master photon server (reading configuration file "PhotonServerSetting")
            queue.Enqueue(callback);
        }
        else
        {
            callback(true);
        }
    }

    public void CreateRoom()
    {
        Debug.Log("Creating room!");
        string roomName = PhotonRoomUtils.GetRoomName();
        RoomOptions roomOps = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 10
        };
        PhotonNetwork.CreateRoom(roomName, roomOps);
        Debug.Log("Room: " + roomName);
        createdRoomName = roomName;
        PhotonRoom.instance.RoomName = roomName;
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

}
