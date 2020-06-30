using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckListItem : MonoBehaviour
{
    //[Header("MenuManager")]
    GameObject MenuManager;

    [Header("INFORMATION")]
    public TextMeshProUGUI title;
    public int ID;
    //public TextMeshProUGUI description;
    //public Image background;

    private Context context;
    private ArrayList listID = new ArrayList();
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
        this.title.text = context.Name;
        this.ID = context.Id;
        //listID.Add((object)context.Id);
        //Debug.Log("Update");
        //foreach(var i in listID)
        //{
        //    Debug.Log(i);
        //}
        //this.description.text = context.Description;
        //string pathImage = ResourceManager.LESSON_AVATARS[context.AvatarId];
        //background.sprite = Resources.Load<Sprite>(pathImage);
    }



    public void Detail()
    {
        Debug.Log("Context detail");
        MenuManager.GetComponent<MainBoardHandler>().LoadLessonDetail(context);
    }

    public void Choose()
    {
        Destroy(gameObject);
    }

    public void Delete()
    {
        Debug.Log("delete");
    }

}
