using Photon.Pun;
using Photon.Voice.PUN;
using Photon.Voice.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerSetup : MonoBehaviour
{
    //public Animator anim;
    //public TextMesh username;
    public Speaker speaker;
    public TMP_Text username;

    private PhotonView PV;
    private GameObject voiceController;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        PV = gameObject.GetComponent<PhotonView>();
        PhotonVoiceView voice = gameObject.GetComponent<PhotonVoiceView>();
        if (PV.IsMine)
        {
            voiceController = GameObject.Find("VoiceController");
            if (voice != null && voiceController != null)
            {
                voice.RecorderInUse = voiceController.GetComponent<Recorder>();
            }
            PlayerController.Instance.AddMinePlayer(gameObject);
        }
        else
        {
            //Destroy(voice);
            //Destroy(speaker);
        }
        SetUserName(PV.Owner.NickName);
    }

    private void Update()
    {
        //if (Input.GetKey(KeyCode.P))
        //{
        //    Debug.Log("udpate 1");
        //    DoAction(EAction.BORING);
        //}
        //else if (Input.GetKey(KeyCode.O))
        //{
        //    Debug.Log("udpate 2");
        //    DoAction(EAction.YAWN);
        //}
    }

    public void SetUserName(string name)
    {
        username.text = name;
    }

    public void DoAction(EAction action)
    {
        Debug.Log("Play action " + action.ToString());
        switch (action)
        {
            case EAction.BORING:
                anim.SetTrigger("Boring");
                break;
            case EAction.YAWN:
                anim.SetTrigger("Yawn");
                break;
            case EAction.ANGRY:
                anim.SetTrigger("Angry");
                break;
            case EAction.HELLO:
                anim.SetTrigger("Hello");
                break;
        }
    }
}
