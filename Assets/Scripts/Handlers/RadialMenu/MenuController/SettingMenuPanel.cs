using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenuPanel : MonoBehaviour
{
    [Header("PANELS")]
    //public GameObject animationPanel;
    //public GameObject notifyRecord;
    public GameObject questionPanel;
    public GameObject recordOptionPanel;
    public GameObject tooltipPanel;
    public GameObject tooltipSettingPanel;
    public GameObject confirmExitPanel;
    public GameObject confirmDeletePanel;
    public GameObject transformPanel;
    public GameObject animationController;
    [Header("BUTTONS")]
    public List<RadialMenuItem> menuItems = new List<RadialMenuItem>();
    [Header("ANIMATION")]
    public Animator anim;

    public static SettingMenuPanel Instance = null;

    private bool isOn = false;

    public bool IsOn { get => isOn; set => isOn = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DisableObjectController();
        OffController();
    }

    private void OnEnable()
    {
        OnController();
        EnableObjectController();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OffController()
    {
        anim.SetBool("open", false);
        IsOn = false;
    }

    public void OnController()
    {
        anim.SetBool("open", true);
        IsOn = true;
    }

    public void DisableObjectController()
    {
        for (int i = 3; i < menuItems.Count; i++)
        {
            menuItems[i].isActive = false;
        }
    }

    public void EnableObjectController()
    {
        for (int i = 3; i < menuItems.Count; i++)
        {
            menuItems[i].isActive = true;
        }
    }

    public void AddModel()
    {
        MREditContextManager.Instance.ShowMenuAddModel();
        OffController();
    }

    public void Save()
    {
        MREditContextManager.Instance.SaveEditContext();
        OffController();
    }

    public void Exit()
    {
        confirmExitPanel.GetComponent<ConfirmExitPanel>().ShowConfirm();
    }

    public void AddQuestion()
    {
        if (menuItems[3].isActive)
        {
            questionPanel.SetActive(true);
            OffController();
        }
    }

    public void ShowTooltipMenu()
    {
        //tooltipPanel.SetActive(true);
        if (menuItems[4].isActive)
        {
            tooltipSettingPanel.SetActive(true);
            OffController();
        }
    }

    public void ShowTransformController()
    {
        //TODO: show available animation
        //MRContextManager.Instance.ShowAnimation();

        //TODO: get data from server and add to list of animations
        if (menuItems[5].isActive)
        {
            //animationPanel.SetActive(true);
            transformPanel.GetComponent<TransformControllerPanel>().ShowTransformController();
            OffController();
        }
    }

    public void ShowAnimationController()
    {
        if (menuItems[6].isActive)
        {
            //notifyRecord.SetActive(true);
            OffController();
            animationController.GetComponent<AnimationControllerPanel>().OnController();
        }
    }

    public void Delete()
    {
        if (menuItems[7].isActive)
        {
            //Destroy(MRDataHolder.Instance.CurrentClickObject);
            //MRDataHolder.Instance.CurrentClickObject = null;
            //DisableObjectController();
            confirmDeletePanel.GetComponent<ConfirmDeletePanel>().ShowConfirm();
        }
    }

    public void ShowRecordOption()
    {
        recordOptionPanel.SetActive(true);
        //OffController();
    }

    public void ShowTooltipSetting(string itemName)
    {
        tooltipSettingPanel.SetActive(true);
        tooltipSettingPanel.GetComponent<TooltipSettingPanel>().objName.text = itemName;
        //OffController();
    }

    public void Close()
    {
        DisableObjectController();
        OffController();
        MRDataHolder.Instance.CurrentClickObject = null;
    }
}
