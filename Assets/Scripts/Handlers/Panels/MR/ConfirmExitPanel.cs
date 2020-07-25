using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmExitPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowConfirm()
    {
        gameObject.SetActive(true);
        if (MRDataHolder.Instance.IsEdit)
        {
            SettingMenuPanel.Instance.OffController();
        }
        else
        {
            FeatureController.Instance.OffController();
        }
    }

    public void Yes()
    {
        if (MRDataHolder.Instance.IsEdit)
        {
            MREditContextManager.Instance.Exit();
        }
        else
        {
            MRGamePlayManager.Instance.Exit();
        }
    }

    public void No()
    {
        gameObject.SetActive(false);
        if (MRDataHolder.Instance.IsEdit)
        {
            SettingMenuPanel.Instance.OnController();
        }
        else
        {
            FeatureController.Instance.OnController();
        }
    }
}
