using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CourseDetail : MonoBehaviour
{
    [Header("INFORMATION")]
    public TMP_Text title;
    public TMP_Text description;
    public TMP_Text author;
    public TMP_Text createTime;
    public Image background;

    [Header("CONTAINER LESS")]
    public GameObject list;

    [Header("PREFAB")]
    public GameObject contextItem;

    private List<GameObject> lessons = new List<GameObject>();
    private Course course;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void destroyAllLesson()
    {
        foreach(GameObject go in lessons)
        {
            Destroy(go);
        }
        lessons.Clear();
    }

    public void LoadData(Course course)
    {
        this.course = course;

        title.SetText(course.Name);
        description.SetText(course.Description);
        author.SetText("Unknown");
        TimeSpan time = TimeSpan.FromMilliseconds(course.CreateTime);
        DateTime dt = new DateTime(1970, 1, 1) + time;
        createTime.SetText(dt.ToShortDateString());
        string pathImage = ResourceManager.COURSE_AVATARS[course.AvatarId];
        background.sprite = Resources.Load<Sprite>(pathImage);

        destroyAllLesson();
        foreach (Context context in course.Contexts)
        {
            GameObject item = Instantiate(contextItem, list.transform, false);
            item.GetComponent<ContextItem>().LoadData(context);
            item.transform.SetParent(list.transform);
            lessons.Add(item);
        }
    }

    public void addContext()
    {

    }

    public void seeAllContext()
    {

    }
}
