using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordNotifyPanel : MonoBehaviour
{
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
        //currentObject = MRContextManager.Instance.CurrentObject;
        //Vector3 cameraPos = Camera.main.transform.position;
        //Vector3 distance = MRDataHolder.Instance.Distance;
        //transform.position = new Vector3(cameraPos.x + distance.x, cameraPos.y + distance.y, cameraPos.z + distance.z - 0.15f);
    }

    public void Cancel()
    {
        Debug.Log("Cancel");
        gameObject.SetActive(false);
        //TagAlongManager.Instance.ControllerOn();
        SettingMenuPanel.Instance.OnController();
    }

    public void Record()
    {
        RecordTransform record = currentObject.GetComponent<RecordTransform>();
        if (record != null)
        {
            record.EnableStartRecord();
            gameObject.SetActive(false);
            //MRContextManager.Instance.Record();
            //settingPanel.SetActive(false);
            //recordPanel.SetActive(false);
            //animationPanel.SetActive(false);
            ////questionPanel.SetActive(false);
        }
    }
}
