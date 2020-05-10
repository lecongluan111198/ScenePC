using Michsky.UI.Frost;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    [Header("RESOURCE")]
    public Animator loadingAnim;
    public ScrollRect scrollRect;

    private int length = 12;
    private int maxOwnOffset = 2;
    private int currentOwnOffset = -1;
    private int currentAccessOffset = -1;

    private List<GameObject> ownCourses = new List<GameObject>();
    private List<GameObject> accessCourses = new List<GameObject>();
    
    GameObject MenuManager;
    static readonly object _object = new object();
    // Start is called before the first frame update
    void Start()
    {
        MenuManager = GameObject.Find("Menu Manager");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        scrollRect.onValueChanged.AddListener(scrollRectCallBack);
    }

    private void scrollRectCallBack(Vector2 value)
    {
        Debug.Log("ScrollRect Changed: " + value);
        if (value.y <= -0.1f)
        {
            LoadOwnCourse();
        }
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
        if (Monitor.TryEnter(_object))
        {
            bool isUpdate = false;
            if (currentOwnOffset < 0)
            {
                loadingAnim.Play("Modal Window In");
                currentOwnOffset = 0;
                isUpdate = true;
            }
            else if (currentOwnOffset < maxOwnOffset)
            {
                currentOwnOffset++;
                isUpdate = true;
            }

            if (isUpdate)
            {
                Debug.Log(length + " " + currentOwnOffset);
                CourseModel.Instance.loadOwnCourse(length, currentOwnOffset, (data) =>
                {
                    if (data != null)
                    {
                        //var group = listOwn.GetComponent<GridLayoutGroup>();
                        if (data.Count < length)
                        {
                            maxOwnOffset = currentOwnOffset;
                        }
                        foreach (Course course in data)
                        {
                            GameObject item = Instantiate(courseItem, listOwn.transform, false);
                            item.GetComponent<CardItem>().LoadData(course);
                            item.transform.SetParent(listOwn.transform);
                            ownCourses.Add(item);
                        }
                    }
                    loadingAnim.Play("Modal Window Out");
                    Monitor.Exit(_object);
                });
            }
        }
    }

    private void LoadAccessCourse()
    {
        if (currentAccessOffset < 0)
        {
            loadingAnim.Play("Modal Window In");
            currentAccessOffset = 0;
            CourseModel.Instance.loadAccessCourse(length, currentAccessOffset, (data) =>
            {
                if (data != null)
                {
                    Debug.Log(data);
                    foreach (Course course in data)
                    {
                        GameObject item = Instantiate(courseItem, listAccess.transform, false);
                        item.GetComponent<CardItem>().LoadData(course);
                        item.transform.SetParent(listAccess.transform);
                        accessCourses.Add(item);
                    }
                }
                loadingAnim.Play("Modal Window Out");
            });
        }
    }

    public void AllOwnCourse()
    {
        foreach (GameObject go in ownCourses)
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

    public void CreateCourse()
    {
        MenuManager.GetComponent<MainBoardHandler>().LoadCourseCreate();
    }
}
