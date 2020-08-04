using Microsoft.MixedReality.Toolkit.UI;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpeedController : MonoBehaviour
{
    [Header("SLIDERS")]
    public PinchSlider speedSlider;
    [Header("TEXT VALUES")]
    public TextMesh speedText;
    [Header("PHOTON")]
    public PhotonView PV;
    private float speed = 50;
    public static PlanetSpeedController Instance = null;

    //public float Speed { get => speed; set => speed = value; }

    //// Start is called before the first frame update
    //private void Awake()
    //{

    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //    else if (Instance != this)
    //    {
    //        Destroy(this);
    //    }
    //}
    void Start()
    {
        //PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {

    }

    public void ShowPanel()
    {
        speed = MRDataHolder.Instance.Speed;
        speedSlider.SliderValue = speed / 100;
        speedText.text = Math.Round(speed, 1).ToString();
        gameObject.SetActive(true);
    }

    public void OnUpdateSpeed()
    {
        speed = speedSlider.SliderValue * 100;
        speedText.text = Math.Round(speed, 1).ToString();
        PV.RPC("UpdateSpeed", RpcTarget.AllBuffered, speed);
    }

    [PunRPC]
    public void UpdateSpeed(float speed)
    {
        this.speed = speed;
        MRDataHolder.Instance.Speed = speed;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        //SettingMenuPanel.Instance.OnController();
    }
}
