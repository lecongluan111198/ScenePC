using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomInfoPanel : MonoBehaviour
{
    public Text roomName;
    public Text player;
    public Text host;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowRoomInfo()
    {
        gameObject.SetActive(true);
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        player.text = PhotonNetwork.CountOfPlayers + " players";
        host.text = PhotonNetwork.MasterClient.NickName;
        FeatureController.Instance.OffController();
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
        FeatureController.Instance.OnController();
    }
}
