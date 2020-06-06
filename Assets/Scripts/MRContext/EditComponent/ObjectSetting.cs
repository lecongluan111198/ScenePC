using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSetting : MonoBehaviour, IMixedRealityPointerHandler
{
    private GameObject settingPanel;
    private SettingMenuPanel settingMennu;

    bool isDown = false;
    bool isDrag = false;

    // Start is called before the first frame update
    void Start()
    {
        settingPanel = MRContextManager.Instance.settingPanel;
        settingMennu = settingPanel.GetComponent<SettingMenuPanel>();
        //settingPanel = gameObject.transform.Fi
        //foreach (Transform child in transform)
        //{
        //    if (child.name.Equals(MRDataHolder.Instance.SettingPanelName))
        //    {
        //        //Debug.Log("Child found. Mame: " + eachChild.name);
        //        settingPanel = child.gameObject;
        //        settingPanel.SetActive(false);
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (isDown && !MRDataHolder.Instance.IsRecord)
        {
            //TODO: show setting panel
            //settingMennu.CurrentObject = gameObject;
            settingPanel.SetActive(true);
            //TODO: set transform directly with player
            isDown = false;
        }
        else
        {
            //settingPanel.SetActive(false);
        }
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {

    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        isDown = !isDown;
        MRContextManager.Instance.CurrentObject = gameObject;
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        isDrag = true;
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        isDrag = false;
        isDown = false;
    }
}
