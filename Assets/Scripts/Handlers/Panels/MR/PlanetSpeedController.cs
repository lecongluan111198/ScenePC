﻿using Microsoft.MixedReality.Toolkit.UI;
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

    private float speed = 50;
    public static PlanetSpeedController Instance = null;

    public float Speed { get => speed; set => speed = value; }

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {

    }

    public void OnUpdateSpeed()
    {
        speed = speedSlider.SliderValue * 100;
        speedText.text = Math.Round(speed, 1).ToString();
    }
}