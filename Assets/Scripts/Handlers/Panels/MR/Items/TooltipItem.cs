using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipItem : MonoBehaviour
{
    public GameObject check;
    public Text itemName;
    private GameObject go;

    private bool hasTooltip = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTooltipSetting()
    {
        SettingMenuPanel.Instance.ShowTooltipSetting(itemName.text);
    }

    public void UpdateInfo(string name, GameObject go, bool isTooltip)
    {
        itemName.text = name;
        this.go = go;
        hasTooltip = isTooltip;
        if (hasTooltip)
        {
            check.SetActive(true);
        }
        else
        {
            check.SetActive(false);
        }
    }
}
