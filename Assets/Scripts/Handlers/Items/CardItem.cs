using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardItem : MonoBehaviour
{
    [Header("Infomation")]
    public TMP_Text title;
    public TMP_Text description;
    public Image background;

    private Course course;

    // Start is called before the first frame update
    void Start()
    {
        title.text = "HCMUS Mixed Reality";
        description.text = "This is a sample description for HCMUS Mixed Reality";
    }

    public void LoadData(Course course)
    {
        this.course = course;
        this.title.text = course.Name;
        this.description.text = course.Discription;
        string pathImage = ResourceManager.COURSE_AVATARS[course.AvatarId];
        background.sprite = Resources.Load<Sprite>(pathImage);
    }

    public void DetailClick()
    {

    }

    public void DeleteClick()
    {

    }

}
