using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    //Room info
    public static PhotonRoom instance;
    private PhotonView PV;

    //public bool isGameLoaded;
    //public string currentScene = "";
    //public string waitingRoomScene;
    //public string gamePlayScene;

    private string roomName;

    public string RoomName { get => roomName; set => roomName = value; }

    //Player info
    //Player[] photonPlayers;
    //public int playersInRoom;
    //public int myNumberInRoom;

    //public int playerInGame;


    private void Awake()
    {
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
        PV = GetComponent<PhotonView>();
    }


    public override void OnEnable()
    {
        Debug.Log("Photon room enable");
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        Debug.Log("Photon room disable");
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Has joined room");
        //photonPlayers = PhotonNetwork.PlayerList;
        //playersInRoom = photonPlayers.Length;
        //myNumberInRoom = playersInRoom;
        PhotonNetwork.NickName = AccountInfo.Instance.Username;
        if (!PhotonNetwork.IsMasterClient)
        {
            //off old scene
            LoadSceneManager.Instance.UnloadCurrentScene();
            Debug.Log("Not master");
            return;
        }
        //LoadWaitingRoomScene();
        LoadGamePlayScene();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log(otherPlayer.NickName + " has left the game");
    }

    private void LoadWaitingRoomScene()
    {
        Debug.Log("Loading WaitingRoomScene");
        //Debug.Log(currentScene);
        //if (currentScene != null && currentScene != "")
        //{
        //    SceneManager.UnloadSceneAsync(currentScene);
        //}
        //PhotonNetwork.LoadLevel(waitingRoomScene);
        LoadSceneManager.Instance.LoadScene(LoadSceneManager.SceneType.WAITING, true);
    }

    private void LoadGamePlayScene()
    {
        Debug.Log("Loading GamePlayScene");
        //Debug.Log(currentScene);
        //if (currentScene != null && currentScene != "")
        //{
        //    SceneManager.UnloadSceneAsync(currentScene);
        //}
        //PhotonNetwork.LoadLevel(gamePlayScene);

        LoadSceneManager.Instance.LoadScene(LoadSceneManager.SceneType.GAMEPLAY, true);
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //call when multiplayer scene is loaded

        //create player
        CreatePlayer();
        if (PhotonNetwork.IsMasterClient)
        {
            if (LoadSceneManager.Instance.CurrentScene == LoadSceneManager.Instance.gamePlayScene)
            {
                //load data
                MRGamePlayManager.Instance.LoadPlayContext();
            }
            else if (LoadSceneManager.Instance.CurrentScene == LoadSceneManager.Instance.waitingRoomScene)
            {
                //load waiting room

            }
        }
    }

    private void CreatePlayer()
    {
        //creates players network controller but not player character
        //if (PV.IsMine)
        //{
        if(LoadSceneManager.Instance.CurrentScene != LoadSceneManager.Instance.mainBoardScene)
        {
            Debug.Log("Create player");
            GameObject player = PhotonNetwork.Instantiate(ResourceManager.Avatar + "IKAvatar", new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
            player.transform.SetParent(Camera.main.transform);
            player.transform.localPosition = new Vector3(-0.05f, -0.83f, 0.07f);
        }
        //}
    }

    //public void StartGame()
    //{
    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        //load play scene
    //        PhotonNetwork.LoadLevel(currentScene, mainBoardScene);
    //    }
    //}

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        //if (PV.IsMine)
        //{
        //    Destroy(PhotonRoom.instance.gameObject);
        //    SceneManager.LoadScene("MainBoard");
        //}
    }

}
