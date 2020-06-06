using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    public string Ques;
    public List<string> listChoose;
    public int ans;

    //private string questionText = "ABC la gi?";
    //private List<string> choose = new List<string> { "Dap an A","Dap an B","Dap an C"};
    //private int answer = 1;

    private string questionText;
    private List<string> choose;
    private int answer;

    public string QuestionText { get => questionText; set => questionText = value; }
    public List<string> Choose { get => choose; set => choose = value; }
    public int Answer { get => answer; set => answer = value; }

    public void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            showQuestion();
        }   
    }

    public void showQuestion()
    {
        questionText = Ques;
        choose = listChoose;
        answer = ans;
        string path = "Assets/Resources/Prefabs/UI/MenuQuestion.prefab";
        GameObject menuQuestion = (GameObject)Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)));
        menuQuestion.GetComponentInChildren<Text>().text = QuestionText;
        menuQuestion.GetComponent<AddQuestionToMenu>().questionText = QuestionText;
        menuQuestion.GetComponent<AddQuestionToMenu>().choose = Choose;
        menuQuestion.GetComponent<AddQuestionToMenu>().answer = Answer;
    }
}
