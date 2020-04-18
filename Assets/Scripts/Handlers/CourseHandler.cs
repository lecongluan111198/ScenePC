using Michsky.UI.Frost;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseHandler : MonoBehaviour
{
    [Header("Manager")]
    public TopPanelManager topManager;

    [Header("Prefab")]
    public GameObject courseItem;

    [Header("Container")]
    public GameObject listOwn;
    public GameObject listAccess;

    private int length = 12;
    private int currentOwnOffset = -1;
    private int currentAccessOffset = -1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadCourse()
    {
        int currentPanel = topManager.currentPanelIndex;
        if (currentPanel == 0)
        {
            LoadOwnCourse();
        }
        else if (currentPanel == 1)
        {
            LoadAccessCourse();
        }
    }
    
    private void LoadOwnCourse()
    {
        if (currentOwnOffset < 0)
        {
            currentOwnOffset = 0;
            CourseModel.Instance.loadOwnCourse(length, currentOwnOffset, (data) => {
                if(data != null)
                {
                    foreach(Course course in data)
                    {
                        GameObject item = Instantiate(courseItem);
                        item.GetComponent<CardItem>().LoadData(course);
                        item.transform.SetParent(listOwn.transform);
                    }
                }
            });
        }
    }

    private void LoadAccessCourse()
    {
        if (currentAccessOffset < 0)
        {
            currentAccessOffset = 0;
            CourseModel.Instance.loadAccessCourse(length, currentAccessOffset, (data) => {
                if (data != null)
                {
                    foreach (Course course in data)
                    {
                        GameObject item = Instantiate(courseItem);
                        item.GetComponent<CardItem>().LoadData(course);
                        item.transform.SetParent(listAccess.transform);
                    }
                }
            });
        }
    }
}
