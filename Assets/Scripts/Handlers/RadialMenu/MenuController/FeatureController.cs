using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureController : MonoBehaviour
{
    [Header("PANELS")]
    public GameObject roomInfoPanel;
    public GameObject questionPanel;
    public GameObject confirmExitPanel;
    [Header("BUTTONS")]
    public List<RadialMenuItem> menuItems = new List<RadialMenuItem>();
    [Header("ANIMATION")]
    public Animator anim;

    public static FeatureController Instance = null;

    private bool isOn = false;

    public bool IsOn { get => isOn; set => isOn = value; }

    public enum EFeature
    {
        QUESTION = 2,
        ANIMATION = 3
    }

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

    public void DisableFeature(EFeature feature)
    {
        menuItems[(int) feature].isActive = false;
    }

    public void EnableFeature(EFeature feature)
    {
        menuItems[(int) feature].isActive = true;
    }

    public void DisableObjectController()
    {
        for (int i = 2; i < menuItems.Count; i++)
        {
            menuItems[i].isActive = false;
        }
    }

    public void EnableObjectController()
    {
        for (int i = 2; i < menuItems.Count; i++)
        {
            menuItems[i].isActive = true;
        }
    }

    private void OnEnable()
    {
        //currentObject = MRDataHolder.Instance.CurrentClickObject;
    }

    public void ShowRoomInfo()
    {
        roomInfoPanel.GetComponent<RoomInfoPanel>().ShowRoomInfo();
    }

    public void Exit()
    {
        confirmExitPanel.GetComponent<ConfirmExitPanel>().ShowConfirm();
    }

    public void ShowQuestion()
    {
        if (menuItems[2].isActive)
        {
            QuestionV2 question = MRDataHolder.Instance.CurrentClickObject.GetComponent<QuestionV2>();
            if (question != null)
            {
                questionPanel.GetComponent<MenuQuestionPanelV2>().ShowMenuQuestion(question);
            }
        }
    }

    public void PlayAnimation()
    {
        if (menuItems[3].isActive)
        {
            CustAnimation anim = MRDataHolder.Instance.CurrentClickObject.GetComponent<CustAnimation>();
            if (anim != null)
            {
                anim.Play();
            }
            else
            {
                RecordAnimation record = MRDataHolder.Instance.CurrentClickObject.GetComponent<RecordAnimation>();
                if (record != null)
                {
                    record.PlayAnimation();
                }
            }
        }

    }

    public void Close()
    {
        //TagAlongManager.Instance.ControllerIn();
        DisableObjectController();
        OffController();
    }
}
