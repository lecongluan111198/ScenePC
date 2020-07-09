using Michsky.UI.Frost;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CourseCreate : MonoBehaviour
{
    [Header("INFORMATION")]
    public TMP_InputField Name;
    public TMP_InputField Description;
    public SwitchManager status;
    public Image avatar;
    
    [Header("RESOURCE")]
    public TMP_Text errorMessage;
    public TMP_Text successMessage;
    public GameObject AddButton;
    public GameObject EditButton;
    

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(avatar.sprite.name);
    }

    public void ChangeAvatar()
    {

    }

    public void ClearInformation()
    {
        Name.text = "";
        Description.text = "";
        status.isOn = true;
        string pathImage = ResourceManager.COURSE_AVATARS[0];
        avatar.sprite = Resources.Load<Sprite>(pathImage);
        AddButton.SetActive(true);
        EditButton.SetActive(false);
    }

    public void UpdateInformation(Course course)
    {
        Name.text = course.Name;
        Description.text = course.Description;
        status.isOn = course.Status;
        string pathImage = ResourceManager.COURSE_AVATARS[course.AvatarId];
        avatar.sprite = Resources.Load<Sprite>(pathImage);
        AddButton.SetActive(false);
        EditButton.SetActive(true);
    }

    public void AddCourse()
    {
        Course course = new Course();
        course.Author = AccountInfo.Instance.Username;
        course.AvatarId = ResourceManager.GetCourseAvatarId(avatar.sprite.name);
        course.Name = Name.text;
        course.Description = Description.text;
        course.Status = status.isOn;
        course.TeacherId = AccountInfo.Instance.UID;
        course.Type = "Education"; //hard code
        errorMessage.text = "";
        successMessage.text = "";
        Debug.Log(course);
        CourseModel.Instance.createCourse(course, (data) =>
        {
            Debug.Log(data);
            if (data.Key)
            {
                successMessage.text = data.Value;
                AddButton.SetActive(false);
                EditButton.SetActive(true);
            }
            else
            {
                errorMessage.text = data.Value;
            }
        });
    }

    public void UpdateCourse()
    {
        Course course = new Course();
        course.Author = AccountInfo.Instance.Username;
        course.AvatarId = ResourceManager.GetCourseAvatarId(avatar.sprite.name);
        course.Name = Name.text;
        course.Description = Description.text;
        course.Status = status.isOn;
        course.TeacherId = AccountInfo.Instance.UID;
        course.Type = "Education"; //hard code
        errorMessage.text = "";
        successMessage.text = "";
        CourseModel.Instance.updateCourse(course, (data) =>
        {
            Debug.Log(data);
            if (data.Key)
            {
                successMessage.text = data.Value;
            }
            else
            {
                errorMessage.text = data.Value;
            }
        });
    }
}
