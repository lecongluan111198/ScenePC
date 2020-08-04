using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformControllerPanel : MonoBehaviour
{
    [Header("SLIDERS")]
    public PinchSlider scale;
    public PinchSlider hRotation;
    public PinchSlider vRotation;
    [Header("TEXT VALUES")]
    public TextMesh currScale;
    public TextMesh currHRotation;
    public TextMesh currVRotation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowTransformController()
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
        scale.SliderValue = (s - 1) / 4;
        hRotation.SliderValue = (rotation.x < 0 ? 360 + rotation.x : rotation.x) / 360;
        vRotation.SliderValue = (rotation.y < 0 ? 360 + rotation.y : rotation.y) / 360;
        //update text
        currScale.text = Math.Round(scale.SliderValue + 1, 1).ToString();
        currHRotation.text = Math.Round(hRotation.SliderValue * 360, 1).ToString();
        currVRotation.text = Math.Round(vRotation.SliderValue * 360, 1).ToString();
        gameObject.SetActive(true);
        SettingMenuPanel.Instance.OffController();
    }

    public void OnUpdateScale()
    {
        GameObject currentObject = MRDataHolder.Instance.CurrentClickObject;
        float v = scale.SliderValue * 4 + 1;
        currentObject.transform.localScale = new Vector3(v, v, v);
        currScale.text = Math.Round(v, 1).ToString();
    }

    public void OnUpdateHorizontalRotation()
    {
        GameObject currentObject = MRDataHolder.Instance.CurrentClickObject;
        Vector3 rotation = currentObject.transform.localEulerAngles;
        rotation.x = hRotation.SliderValue * 360;
        currentObject.transform.rotation = Quaternion.Euler(rotation);
        currHRotation.text = Math.Round(rotation.x, 1).ToString();
    }

    public void OnUpdateVerticalRotation()
    {
        GameObject currentObject = MRDataHolder.Instance.CurrentClickObject;
        Vector3 rotation = currentObject.transform.localEulerAngles;
        rotation.y = vRotation.SliderValue * 360;
        currentObject.transform.rotation = Quaternion.Euler(rotation);
        currVRotation.text = Math.Round(rotation.y, 1).ToString();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        SettingMenuPanel.Instance.OnController();
    }
}
