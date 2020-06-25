using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuQuestionPanel : MonoBehaviour
{
    public Text question;
    public Text[] answers;
    public Image[] ansColors;
    private int correctAns;

    private void OnEnable()
    {
        
    }

    public void updateInfomation(Question data)
    {
        question.text = data.QuestionText;
        for(int i=0; i<answers.Length; i++)
        {
            answers[i].text = data.Choose[i];
        }
        correctAns = data.Answer;
    }

    public void chooseAnswer(int ans)
    {
        if (correctAns == ans)
        {
            //show correct ans
            Debug.Log("correct");
            ansColors[ans].color = Color.green;
        }
        else
        {
            //show wrong ans
            Debug.Log("wrong");
            ansColors[ans].color = Color.red;
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

}
