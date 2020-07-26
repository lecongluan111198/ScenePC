﻿using Photon.Pun;
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
        PV = gameObject.GetComponent<PhotonView>();
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
            LoadSceneManager.Instance.UpdateCurrentScene(LoadSceneManager.SceneType.GAMEPLAY);
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
        //create player
        
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
        
        if (LoadSceneManager.Instance.CurrentScene == LoadSceneManager.Instance.gamePlayScene || 
            (PhotonNetwork.InRoom))
        {
            CreatePlayer();
        }
    }

    private void CreatePlayer()
    {
        Debug.Log("Create player");
        //string role = AccountInfo.Instance.Role.ToLower();
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject player = PhotonNetwork.Instantiate(ResourceManager.Avatar + "Teacher", new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
            player.transform.SetParent(Camera.main.transform);
            player.transform.localPosition = new Vector3(-0.009f, -1.59f, -0.05f);
        }
        else
        {
            GameObject player = PhotonNetwork.Instantiate(ResourceManager.Avatar + "Student", new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
            player.transform.SetParent(Camera.main.transform);
            player.transform.localPosition = new Vector3(-0.215f, -1.587f, -0.22f);
        }
       
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
        //LoadSceneManager.Instance.LoadScene(LoadSceneManager.SceneType.MAINBOARD, false);

        //if (PV.IsMine)
        //{
        //    Destroy(PhotonRoom.instance.gameObject);
        //    SceneManager.LoadScene("MainBoard");
        //}
    }

}
