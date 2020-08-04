using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScaleController : MonoBehaviour
{
    [Header("SLIDERS")]
    public PinchSlider scale;
    [Header("TEXT VALUES")]
    public TextMesh currScale;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowPanel()
    {
        GameObject currentObject = MRDataHolder.Instance.CurrentClickObject;
        float s = currentObject.transform.localScale.x < 1 ? 1 : currentObject.transform.localScale.x;
        Vector3 rotation = currentObject.transform.localEulerAngles;
        if (rotation.x < 0 && rotation.x >= -2)
        {
            rotation.x = 0;
        }
        if (rotation.y < 0 && rotation.y >= -2)
        {
            rotation.y = 0;
        }
        //update slider
        scale.SliderValue = (s - 1) / 9;
        //update text
        currScale.text = Math.Round(scale.SliderValue + 1, 1).ToString();
        gameObject.SetActive(true);
        //SettingMenuPanel.Instance.OffController();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        //SettingMenuPanel.Instance.OnController();
    }

    public void OnUpdateScale()
    {
        GameObject currentObject = MRDataHolder.Instance.CurrentClickObject;
        float v = scale.SliderValue * 9 + 1;
        currentObject.transform.localScale = new Vector3(v, v, v);
        currScale.text = Math.Round(v, 1).ToString();
    }
}
