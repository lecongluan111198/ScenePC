using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    GameObject MenuManager;

    [Header("INFORMATION")]
    //public TextMeshProUGUI title;
    //public TextMeshProUGUI description;
    public Image background;

    private Context context;

    // Start is called before the first frame update
    void Start()
    {
        MenuManager = GameObject.Find("Menu Manager");
        //title.text = "MR Lesson";
        //description.text = "Description";
    }

    public Context getContext()
    {
        return context;
    }

    public void UpdateInformation(Context context)
    {
        this.context = context;
        //this.title.text = context.Name;
        //this.description.text = context.Description;
        string pathImage = ResourceManager.COURSE_AVATARS[context.AvatarId];
        background.sprite = Resources.Load<Sprite>(pathImage);
    }

    public void Detail()
    {
        Debug.Log("Context detail");
        //MenuManager.GetComponent<MainBoardHandler>().LoadLessonDetail(context);
    }
}
