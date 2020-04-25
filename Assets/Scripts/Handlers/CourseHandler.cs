using Michsky.UI.Frost;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private List<GameObject> ownCourses = new List<GameObject>();
    private List<GameObject> accessCourses = new List<GameObject>();
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
        //int currentPanel = topManager.currentPanelIndex;
        //if (currentPanel == 0)
        //{
        //    LoadOwnCourse();
        //}
        //else if (currentPanel == 1)
        //{
        //    LoadAccessCourse();
        //}
    }

    private void LoadOwnCourse()
    {
        if (currentOwnOffset < 0)
        {
            currentOwnOffset = 0;
            CourseModel.Instance.loadOwnCourse(length, currentOwnOffset, (data) =>
            {
                if (data != null)
                {
                    //var group = listOwn.GetComponent<GridLayoutGroup>();
                    foreach (Course course in data)
                    {
                        GameObject item = Instantiate(courseItem, listOwn.transform, false);
                        item.GetComponent<CardItem>().LoadData(course);
                        item.transform.SetParent(listOwn.transform);
                        ownCourses.Add(item);
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
            CourseModel.Instance.loadAccessCourse(length, currentAccessOffset, (data) =>
            {
                if (data != null)
                {
                    foreach (Course course in data)
                    {
                        GameObject item = Instantiate(courseItem, listAccess.transform, false);
                        item.GetComponent<CardItem>().LoadData(course);
                        item.transform.SetParent(listAccess.transform);
                        accessCourses.Add(item);
                    }
                }
            });
        }
    }

    public void AllOwnCourse()
    {
        foreach(GameObject go in ownCourses)
        {
            go.SetActive(true);
        }
    }

    public void PublicOwnCourse()
    {
        foreach (GameObject go in ownCourses)
        {
            if (go.GetComponent<CardItem>().getCourse().Status == false)
            {
                go.SetActive(false);
            }
            else
            {
                go.SetActive(true);
            }
        }
    }

    public void PrivateOwnCourse()
    {
        foreach (GameObject go in ownCourses)
        {
            if (go.GetComponent<CardItem>().getCourse().Status == true)
            {
                go.SetActive(false);
            }
            else
            {
                go.SetActive(true);
            }
        }
    }
}
