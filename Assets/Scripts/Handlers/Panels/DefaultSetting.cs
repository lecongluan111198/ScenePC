using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultSetting : MonoBehaviour
{
    [Header("MUSIC")]
    public Slider musicSlider;
    [Header("SOUND")]
    public Slider soundSlider;
    private float defaultSlider = 50.0f;
    public void setDefault()
    {
        musicSlider.value = defaultSlider;
        soundSlider.value = defaultSlider;
    }
}
