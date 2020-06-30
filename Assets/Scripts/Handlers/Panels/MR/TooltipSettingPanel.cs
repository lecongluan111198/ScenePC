using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipSettingPanel : MonoBehaviour
{
    public TMP_InputField title;
    public Text objName;

    private GameObject currentObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        currentObject = MRDataHolder.Instance.CurrentClickObject;
    }

    public void Save()
    {
        if(title.text != "")
        {
            MRTooltip tooltip = ConvertContextUtils.addComponent<MRTooltip>(currentObject);
            tooltip.AddTooltip(objName.text, title.text);
        }
        gameObject.SetActive(false);
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }
}
