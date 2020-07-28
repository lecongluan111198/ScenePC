using Michsky.UI.Frost;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBoardHandler : MonoBehaviour
{
    [Header("Panel")]
    public GameObject CourseDetail;
    public GameObject CourseCreate;
    public GameObject LessonDetail;
    public GameObject LessonCreate;

    [Header("TOP PANNEL")]
    public TopPanelManager topManager;

    [Header("RESOURCES")]
    public GameObject mainPanels;
    public Animator backgroundAnimator;
    public Animator startFadeIn;

    private Animator mainPanelsAnimator;
    // Start is called before the first frame update
    void Start()
    {
        mainPanelsAnimator = mainPanels.GetComponent<Animator>();
        mainPanels.SetActive(true);
        mainPanelsAnimator.Play("Panel Start");
        backgroundAnimator.Play("Switch");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadCourseDetail(Course course)
    {
        CourseDetail.GetComponent<CourseDetail>().LoadData(course);
        topManager.ExtraPanelAnim(0);
    }

    public void LoadLessonDetail(Context context)
    {
        LessonDetail.GetComponent<LessonDetail>().LoadData(context);
        topManager.ExtraPanelAnim(1);
    }

    public void LoadLessonCreate()
    {
        //LessonCreate.GetComponent<LessonCreate>();
        topManager.ExtraPanelAnim(3);
    }

    public void LoadCourseCreate()
    {
        CourseCreate.GetComponent<CourseCreate>().ClearInformation();
        topManager.ExtraPanelAnim(2);
    }
    public void LoadCourseEdit(Course course)
    {
        CourseCreate.GetComponent<CourseCreate>().UpdateInformation(course);
        topManager.ExtraPanelAnim(2);
    }



    public void ExitGame()
    {
        Application.Quit();
    }
}
