using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmDeletePanel : MonoBehaviour
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
        SettingMenuPanel.Instance.OffController();
    }

    public void Yes()
    {
        Destroy(MRDataHolder.Instance.CurrentClickObject);
        MRDataHolder.Instance.CurrentClickObject = null;
        SettingMenuPanel.Instance.DisableObjectController();
    }

    public void No()
    {
        gameObject.SetActive(false);
        SettingMenuPanel.Instance.OnController();
    }
}
