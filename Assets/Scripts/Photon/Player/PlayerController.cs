using Photon.Pun;
using Photon.Voice.PUN;
using Photon.Voice.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("ANIMATION")]
    public Animator anim;

    public static PlayerController Instance = null;
    private GameObject minePlayer;

    private bool isOn = false;

    public bool IsOn { get => isOn; set => isOn = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    public void AddMinePlayer(GameObject player)
    {
        minePlayer = player;
    }

    public void DoAction(int num)
    {
        if(minePlayer != null)
        {
            minePlayer.GetComponent<PlayerSetup>().DoAction((EAction)num);
        }
    }

    public void OffController()
    {
        anim.SetBool("open", false);
        IsOn = false;
    }

    public void OnController()
    {
        anim.SetBool("open", true);
        IsOn = true;
    }

    public void Close()
    {
        OffController();
    }

}