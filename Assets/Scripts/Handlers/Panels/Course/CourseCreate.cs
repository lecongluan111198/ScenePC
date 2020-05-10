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
    public TMP_Text errorLoginMessage;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(avatar.sprite.name);
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

        CourseModel.Instance.createCourse(course, (data) =>
        {
            if (data.Key)
            {

            }
            else
            {
                errorLoginMessage.text = data.Value;
            }
        });
    }

    public void UpdateCourse()
    {

    }
}
