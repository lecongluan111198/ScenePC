using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerSetup : MonoBehaviour
{
    private PhotonView PV;

    [Header("INFORMATION")]
    public Camera myCamera;
    public AudioListener myAL;
    public ThirdPersonUserControl thirdController;
    public PlayerController myController;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {

        }
        else
        {
            Destroy(myCamera);
            Destroy(myAL);
            Destroy(thirdController);
            //Destroy(myController);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
