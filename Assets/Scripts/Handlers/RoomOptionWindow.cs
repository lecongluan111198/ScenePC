using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomOptionWindow : MonoBehaviour
{

    private void Start()
    {
        //clear data
    }

    public void CreateRoom()
    {
        //SceneManager.LoadScene("HUD");
        PhotonLobby.instance.ConnectToServer((isConnect)=> {
            PhotonLobby.instance.CreateRoom();
        });
    }

    public void JoinRoom()
    {
        
    }
}
