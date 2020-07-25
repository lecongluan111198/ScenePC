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
        //settingPanel = MRContextManager.Instance.settingPanel;
        //settingMennu = settingPanel.GetComponent<SettingMenuPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDown && !MRDataHolder.Instance.IsRecord)
        {
            //TODO: show setting panel
            //settingMennu.CurrentObject = gameObject;
            MRDataHolder.Instance.CurrentClickObject = gameObject;
            //settingPanel.SetActive(true);
            //TagAlongManager.Instance.ControllerOut();
            if (MRDataHolder.Instance.IsEdit)
            {
                SettingMenuPanel.Instance.OnController();
                SettingMenuPanel.Instance.EnableObjectController();
            }
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
        //MRContextManager.Instance.CurrentObject = gameObject;
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
