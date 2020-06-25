using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LessonDetail : MonoBehaviour
{
    [Header("INFORMATION")]
    public TMP_Text title;
    public TMP_Text description;
    public TMP_Text author;
    public TMP_Text createTime;
    public Image background;

    [Header("RESOURCE")]
    public Animator roomOption;

    private Context context;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadData(Context context)
    {
        this.context = context;

        title.SetText(context.Name);
        description.SetText(context.Description);
        author.SetText("Unknown");
        TimeSpan time = TimeSpan.FromMilliseconds(context.CreateTime);
        DateTime dt = new DateTime(1970, 1, 1) + time;
        createTime.SetText(dt.ToShortDateString());
        string pathImage = ResourceManager.LESSON_AVATARS[context.AvatarId];
        background.sprite = Resources.Load<Sprite>(pathImage);

        //update to MRDataHolder
        //MRDataHolder.Instance.CurrentContext = this.context;
        MRDataHolder.Instance.updateCurrentContext(this.context);
        Debug.Log(context.Content);
    }

    public void PlayContext()
    {
        MRDataHolder.Instance.IsEdit = false;
        roomOption.Play("Modal Window In");
    }

    public void EditLesson()
    {
        MRDataHolder.Instance.IsEdit = true;
        LoadSceneManager.Instance.LoadScene(LoadSceneManager.SceneType.MREDIT, false);
    }

    public void DeleteLesson()
    {

    }
}
