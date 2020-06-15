using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChange : MonoBehaviour
{
    private AudioSource AudioSrc;
    public Slider slider; 
    private float AudioVolume = 0.5f;

    void Start()
    {
        AudioSrc = GetComponent<AudioSource>();
    }

    public void Update()
    {
        AudioSrc.volume = AudioVolume;
    }

    public void SetVolume()
    {
        AudioVolume = (slider.value)/100;
    }
}
