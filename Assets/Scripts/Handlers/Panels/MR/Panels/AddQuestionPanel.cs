using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddQuestionPanel : MonoBehaviour
{
    [Header("INFORMATION")]
    public TMP_InputField question;
    public TMP_InputField ansA;
    public TMP_InputField ansB;
    public TMP_InputField ansC;
    public TMP_InputField ansD;
    public ToggleGroup toggleGroup;

    private GameObject currentObject;
    private int correctPos = 0;
    private EContent questionType = EContent.TEXT;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        currentObject = MRDataHolder.Instance.CurrentClickObject;
        //erase old data
        question.text = "";
        ansA.text = "";
        ansB.text = "";
        ansC.text = "";
        ansD.text = "";
        //recordButton.SetActive(true);
        //stopRecordButton.SetActive(false);
        //playButton.SetActive(false);
    }

    public void ChangeQuestionType(int type)
    {
        //this.questionType = (EContent)type;
        //if (type == (int)EContent.TEXT)
        //{
        //    quesText.SetActive(true);
        //    quesVoice.SetActive(false);
        //}
        //else
        //{
        //    quesText.SetActive(false);
        //    quesVoice.SetActive(true);
        //}
    }

    public void Save()
    {
        QuestionV2 com = ConvertContextUtils.AddComponent<QuestionV2>(currentObject);

        switch (questionType)
        {
            case EContent.TEXT:
                com.Question = new TextContent(question.text);
                break;
            case EContent.VOICE:
                {
                    AudioSource audioSource = ConvertContextUtils.AddComponent<AudioSource>(currentObject);
                    if (audioSource.clip != null)
                    {
                        float[] data = new float[audioSource.clip.samples * audioSource.clip.channels];
                        audioSource.clip.GetData(data, 0);
                        com.Question = new VoiceContent(data);
                    }
                    else
                    {
                        return;
                    }
                }
                break;
            default:
                return;
        }
        com.Choose = new List<Content>()
        {
            new TextContent(ansA.text),
            new TextContent(ansB.text),
            new TextContent(ansC.text),
            new TextContent(ansD.text)
        };
        foreach (Toggle tog in toggleGroup.ActiveToggles())
        {
            if (tog.isOn)
            {
                switch (tog.name)
                {
                    case "A":
                        correctPos = 0;
                        break;
                    case "B":
                        correctPos = 1;
                        break;
                    case "C":
                        correctPos = 2;
                        break;
                    case "D":
                        correctPos = 3;
                        break;
                }
                break;
            }
        }
        com.Answer = correctPos;
        Cancel();
    }

    public void PlayRecord()
    {
        AudioSource audioSource = ConvertContextUtils.AddComponent<AudioSource>(currentObject);
        audioSource.Play();
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
        SettingMenuPanel.Instance.OnController();
        //TagAlongManager.Instance.ControllerOn();
    }
}
