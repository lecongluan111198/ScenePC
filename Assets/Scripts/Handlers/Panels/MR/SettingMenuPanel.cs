using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenuPanel : MonoBehaviour
{
    private GameObject currentObject;

    public GameObject animationPanel;
    public GameObject notifyRecord;
    public GameObject questionPanel;
    public GameObject recordOptionPanel;

    public static SettingMenuPanel Instance = null;

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

    private void OnEnable()
    {
        //currentObject = MRContextManager.Instance.CurrentObject;
        currentObject = MRDataHolder.Instance.CurrentClickObject;
        //Debug.Log(Camera.main.transform.forward.y);
        //Vector3 cameraPos = Camera.main.transform.position;
        //Vector3 distance = MRDataHolder.Instance.Distance;
        //transform.position = new Vector3(cameraPos.x + distance.x, cameraPos.y + distance.y, cameraPos.z + distance.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AvailableAnimation()
    {
        //TODO: show available animation
        //MRContextManager.Instance.ShowAnimation();

        //TODO: get data from server and add to list of animations
        animationPanel.SetActive(true);
    }

    public void RecordAnim()
    {
        //TODO: show notify recording animation
        notifyRecord.SetActive(true);
    }

    public void ShowRecordOption()
    {
        recordOptionPanel.SetActive(true);
    }

    public void AddQuestion()
    {
        questionPanel.SetActive(true);
    }

    public void Close()
    {
        Debug.Log("close");
        //gameObject.SetActive(false);
        TagAlongManager.Instance.ControllerIn();
    }
}
