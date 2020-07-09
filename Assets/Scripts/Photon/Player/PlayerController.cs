﻿using Photon.Pun;
using Photon.Voice.PUN;
using Photon.Voice.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public Animator anim;
    //public TextMesh username;
    //private PhotonView PV;
    private GameObject voiceController;

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        //PV = gameObject.GetComponent<PhotonView>();
        voiceController = GameObject.Find("VoiceController");
        PhotonVoiceView voice = gameObject.GetComponent<PhotonVoiceView>();
        if (voice != null && voiceController != null)
        {
            voice.RecorderInUse = voiceController.GetComponent<Recorder>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        //Debug.Log("mine " + PV.IsMine);
        //username.text = AccountInfo.Instance.Username;
        //if (PV.IsMine)
        //{
        //    Camera.main.transform.localPosition = new Vector3(0, 0.64f, -0.071f);
        //    transform.SetParent(Camera.main.transform);
        //    transform.localPosition = new Vector3(0, 0, 0);
        //}
        //else
        //{

        //}
    }

    public void SetUserName(string name)
    {
        //username.text = name;
    }

    private void FixedUpdate()
    {
    }
}
