using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    public TMP_Text RoomName;
    // Start is called before the first frame update
    void Start()
    {
        RoomName.text = PhotonRoom.instance.RoomName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeToScene(string sceneName)
    {
        Destroy(PhotonRoom.instance.gameObject);
        StartCoroutine(DisconnectAndLoad(sceneName));
    }

    IEnumerator DisconnectAndLoad(string sceneName)
    {
        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.NetworkClientState == Photon.Realtime.ClientState.Leaving)
        {
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
    }

}
