using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    private string questionText;
    private List<string> choose;
    private int answer;

    public string QuestionText { get => questionText; set => questionText = value; }
    public List<string> Choose { get => choose; set => choose = value; }
    public int Answer { get => answer; set => answer = value; }

    private float t = 0.5f;
    public void Start()
    {

    }
    void Update()
    {
    }

    public void showQuestion()
    {
        //get position
        Vector3 src = gameObject.transform.position;
        Vector3 dest = Camera.main.transform.position;
        Vector3 newPos = new Vector3();
        newPos.x = src.x + (dest.x - src.x) * t;
        newPos.y = src.y + (dest.y - src.y) * t;
        newPos.z = src.z + (dest.z - src.z) * t;
        newPos.y += 0.6f;
        GameObject menuQuestion = Instantiate(Resources.Load(ResourceManager.MRCanvasPrefab + "MenuQuestion") as GameObject);
        menuQuestion.transform.position = newPos;
        MenuQuestionPanel panel = menuQuestion.GetComponent<MenuQuestionPanel>();
        if(panel != null)
        {
            panel.updateInfomation(this);
        }
    }
}
