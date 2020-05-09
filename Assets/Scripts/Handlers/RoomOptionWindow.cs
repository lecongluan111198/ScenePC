﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomOptionWindow : MonoBehaviour
{
    [Header("INFORMATION")]
    public TMP_InputField roomCode;

    private void Start()
    {
        //clear data
    }

    public void CreateRoom()
    {
        //SceneManager.LoadScene("HUD");
        Debug.Log("Create room!");
        PhotonLobby.instance.Connect((isConnect)=> {
            if (isConnect)
            {
                PhotonLobby.instance.CreateRoom();
            }
            else
            {
                Debug.Log("Connect to server fail!");
            }
        });
    }

    public void JoinRoom()
    {
        //SceneManager.LoadScene("HUD");
        Debug.Log("Create room!");
        PhotonLobby.instance.Connect((isConnect) => {
            if (isConnect)
            {
                PhotonLobby.instance.JoinRoom(roomCode.text);
            }
            else
            {
                Debug.Log("Connect to server fail!");
            }
        });
    }
}
