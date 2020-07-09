using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuQuestionPanelV2 : MonoBehaviour
{
    [Header("TEXT")]
    public Text question;
    public Text[] answers;
    public Image[] ansColors;
    [Header("VOICE")]
    

    private int correctAns;

    private void OnEnable()
    {

    }

    public void updateInfomation(QuestionV2 data)
    {
        //update question
        if(data.Question.Type == EContent.TEXT)
        {
            question.text = data.Question.GetValue() as string;
        }
        else
        {

        }

        Content ans;
        for (int i = 0; i < answers.Length; i++)
        {
            ans = data.Choose[i];
            if (ans.Type == EContent.TEXT) {
                answers[i].text = ans.GetValue() as string;
            }
            else
            {

            }
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
        TagAlongManager.Instance.ControllerOn();
    }
}
