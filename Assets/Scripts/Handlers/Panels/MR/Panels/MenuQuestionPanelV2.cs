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
    public GameObject voiceButton;
    public GameObject quesionText;

    private int correctAns;
    private AudioSource audioSrc;
    private int position = 0;
    private int samplerate = 44100;
    private float frequency = 440;

    private void OnEnable()
    {
        foreach(Image image in ansColors)
        {
            image.color = Color.white;
        }
    }

    public void ShowMenuQuestion(QuestionV2 data)
    {
        UpdateInfomation(data);
        gameObject.SetActive(true);
        FeatureController.Instance.OffController();
    }

    public void UpdateInfomation(QuestionV2 data)
    {
        //update question
        if (data.Question.Type == EContent.TEXT)
        {
            voiceButton.SetActive(false);
            quesionText.SetActive(true);
            question.text = data.Question.GetValue() as string;

        }
        else
        {
            voiceButton.SetActive(true);
            quesionText.SetActive(false);
            audioSrc = ConvertContextUtils.AddComponent<AudioSource>(gameObject);
            float[] clipData = data.Question.GetValue() as float[];
            AudioClip clip = AudioClip.Create("QuesVoice", clipData.Length, 1, samplerate, false);
            Debug.Log(clipData.Length);
            clip.SetData(clipData, 0);
            audioSrc.clip = clip;
        }

        Content ans;
        for (int i = 0; i < answers.Length; i++)
        {
            ans = data.Choose[i];
            if (ans.Type == EContent.TEXT)
            {
                answers[i].text = ans.GetValue() as string;
            }
            else
            {

            }
        }
        correctAns = data.Answer;
    }

    public void ChooseAnswer(int ans)
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
        Debug.Log(ansColors[ans].color == Color.red);
    }

    public void Play()
    {
        if (audioSrc != null && !audioSrc.isPlaying)
        {
            Debug.Log(audioSrc.clip.channels * audioSrc.clip.length);
            Debug.Log("play");
            audioSrc.Play();
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
        FeatureController.Instance.OnController();
    }
}
