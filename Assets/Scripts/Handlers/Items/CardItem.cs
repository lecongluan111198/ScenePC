using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardItem : MonoBehaviour
{
    //[Header("MenuManager")]
    GameObject MenuManager;

    [Header("Infomation")]
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public Image background;

    private Course course;

    // Start is called before the first frame update
    void Start()
    {
        MenuManager = GameObject.Find("Menu Manager");
        //title.text = "HCMUS Mixed Reality 1";
        //description.text = "This is a sample description for HCMUS Mixed Reality";
    }

    public Course getCourse()
    {
        return course;
    }

    public void LoadData(Course course)
    {
        this.course = course;
        title.SetText(course.Name);
        description.SetText(course.Description, true);
        //title.text = course.Name;
        //description.text = course.Description;
        string pathImage = ResourceManager.COURSE_AVATARS[course.AvatarId];
        background.sprite = Resources.Load<Sprite>(pathImage);
    }

    public void DetailClick()
    {
        Debug.Log("Detail click");
        MenuManager.GetComponent<MainBoardHandler>().LoadCourseDetail(course);
    }

    public void EditClick()
    {
        Debug.Log("Edit click");
        MenuManager.GetComponent<MainBoardHandler>().LoadCourseEdit(course);
    }

    public void DeleteClick()
    {

    }

}
