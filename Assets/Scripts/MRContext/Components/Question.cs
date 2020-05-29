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

    private void Start()
    {
        GameObject go = new GameObject();
        string path = "Assets/Prefabs/UI/MenuQuestion.prefab";
        string pathAnswer = "Assets/Prefabs/Question.prefab";
        GameObject menuQuestion = (GameObject)Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)));
        menuQuestion.transform.SetParent(go.transform);
        menuQuestion.GetComponentInChildren<Text>().text = QuestionText;
        int tmp = 0;
        foreach (var i in choose)
        {
            GameObject button = (GameObject)Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(pathAnswer, typeof(GameObject)));
            button.transform.SetParent(go.transform);
            button.name = i;
            button.GetComponentInChildren<TextMeshProUGUI>().text = i;
            Button buttonCtrl = button.GetComponent<Button>();
            if (answer != tmp)
            {
                buttonCtrl.onClick.AddListener(() => answerWrong());
            }
            else
            {
                buttonCtrl.onClick.AddListener(() => answerCorrect());
            }
        }
        go.transform.SetParent(transform);
    }

    private void Update()
    {
        
    }

    public Question showQuestion()
    {
        Question qs = new Question();
        string path = "Assets/Prefabs/UI/MenuQuestion.prefab";
        string pathAnswer= "Assets/Prefabs/Question.prefab";
        GameObject menuQuestion = (GameObject)Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)));
        menuQuestion.GetComponentInChildren<Text>().text = QuestionText;
        int tmp = 0;
        foreach (var i in choose)
        {
            GameObject button = (GameObject)Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(pathAnswer, typeof(GameObject)));
            button.name = i;
            button.GetComponentInChildren<TextMeshProUGUI>().text = i;
            Button buttonCtrl = button.GetComponent<Button>();
            if (answer != tmp) 
            {
                buttonCtrl.onClick.AddListener(() => answerWrong());
            }
            else
            {
                buttonCtrl.onClick.AddListener(() => answerCorrect());
            }
        }
        return qs;
    }
    private void answerCorrect()
    {
        Debug.Log("Dung");
    }
    private void answerWrong()
    {
        Debug.Log("Sai roi");
    }
}
