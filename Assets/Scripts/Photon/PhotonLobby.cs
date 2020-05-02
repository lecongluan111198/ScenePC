using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static PhotonLobby instance;
    private Action<bool> currentCallBack;

    //public GameObject battleButton; //click to start joining room
    //public GameObject cancelButton;

    private string createdRoomName;


    private void Awake()
    {
        //instance = this;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(instance.gameObject);
                instance = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
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
        //battleButton.SetActive(true);
        currentCallBack(true);
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

    public void ConnectToServer(Action<bool> callback)
    {
        PhotonNetwork.ConnectUsingSettings(); //Connects to Master photon server (reading configuration file "PhotonServerSetting")
        currentCallBack = callback;
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
        Debug.Log("Room" + roomName);
        createdRoomName = roomName;
    }

    public void OnBattleButtonClicked()
    {
        PhotonNetwork.JoinRoom(PhotonRoomUtils.GetRoomName());
    }

    public void OnCancelButtonClicked()
    {
        PhotonNetwork.LeaveRoom();
    }

}
