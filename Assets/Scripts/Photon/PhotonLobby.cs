using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static PhotonLobby instance;

    public GameObject battleButton; //click to start joining room
    public GameObject cancelButton;

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
        PhotonNetwork.ConnectUsingSettings(); //Connects to Master photon server (reading configuration file "PhotonServerSetting")
    }

    //this callback will be called whether connection to server is successfull
    public override void OnConnectedToMaster()
    {
        //base.OnConnectedToMaster();
        Debug.Log("Player has connected to the Photon master server.");
        //when calling PhotonNetwork.LoadLevel(..) that action will be synchronized accross all client
        //that means all client will load into the same scene at the same time
        PhotonNetwork.AutomaticallySyncScene = true; 
        battleButton.SetActive(true);
    }

    public void OnBattleButtonClicked()
    {
        battleButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();//looks at all available rooms and picks random one room for player to join
    }

    public void OnCancelButtonClicked()
    {
        cancelButton.SetActive(false);
        battleButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

    private void CreateRoom()
    {
        Debug.Log("Creating room!");
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 10
        };
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
        Debug.Log("Room" + randomRoomName);
    }

    //this callback will be called if player fail to join random room
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join random game but failed. There must be no open game available. returnCode: " + returnCode + " message: " + message);
        CreateRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create new room but failed. There must already be a room with same name. returnCode: " + returnCode + " message: " + message);
        CreateRoom();
    }

    ////this callback will be called if player joins a room successfully
    //public override void OnJoinedRoom()
    //{
    //    Debug.Log("We are now in a room");
    //}

}
