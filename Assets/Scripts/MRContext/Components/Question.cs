using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    public string question;
    public List<string> answer;
    public int correct;

    public Question showQuestion()
    {
        Question qs = new Question();
        string path = "Assets/Prefabs/UI/MenuQuestion.prefab";
        string pathAnswer= "Assets/Prefabs/Question.prefab";
        GameObject menuQuestion = (GameObject)Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)));
        menuQuestion.GetComponentInChildren<Text>().text = question;
        int tmp = 0;
        foreach (var i in answer)
        {
            GameObject button = (GameObject)Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(pathAnswer, typeof(GameObject)));
            button.name = i;
            button.GetComponentInChildren<TextMeshProUGUI>().text = i;
            Button buttonCtrl = button.GetComponent<Button>();
            if (correct != tmp) 
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
