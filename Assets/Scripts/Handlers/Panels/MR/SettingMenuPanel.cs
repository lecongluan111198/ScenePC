using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenuPanel : MonoBehaviour
{
    private GameObject currentObject;
    [SerializeField]
    private GameObject notifyRecord;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        currentObject = MRContextManager.Instance.CurrentObject;
        Debug.Log(Camera.main.transform.forward.y);
        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 distance = MRDataHolder.Instance.Distance;
        transform.position = new Vector3(cameraPos.x + distance.x, cameraPos.y + distance.y, cameraPos.z + distance.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AvailableAnimation()
    {
        //TODO: show available animation
        MRContextManager.Instance.ShowAnimation();
    }

    public void ShowNotify()
    {
        //TODO: show notify recording animation
        notifyRecord.SetActive(true);
    }

    public void AddQuestion()
    {

    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }
}
