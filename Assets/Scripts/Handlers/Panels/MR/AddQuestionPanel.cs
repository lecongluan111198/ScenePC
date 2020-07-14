using System.Collections;
using System.Collections.Generic;
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
    [Header("QUESTION")]
    public GameObject quesText;
    public GameObject quesVoice;
    public GameObject recordButton;
    public GameObject stopRecordButton;
    public GameObject playButton;

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
        recordButton.SetActive(true);
        stopRecordButton.SetActive(false);
        playButton.SetActive(false);
    }

    public void ChangeQuestionType(int type)
    {
        this.questionType = (EContent)type;
        if (type == (int)EContent.TEXT)
        {
            quesText.SetActive(true);
            quesVoice.SetActive(false);
        }
        else
        {
            quesText.SetActive(false);
            quesVoice.SetActive(true);
        }
    }

    public void Save()
    {
        QuestionV2 com = ConvertContextUtils.addComponent<QuestionV2>(currentObject);

        switch (questionType)
        {
            case EContent.TEXT:
                com.Question = new TextContent(question.text);
                break;
            case EContent.VOICE:
                {
                    AudioSource audioSource = ConvertContextUtils.addComponent<AudioSource>(currentObject);
                    if (audioSource.clip != null)
                    {
                        float[] data = new float[audioSource.clip.channels * audioSource.clip.frequency];
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

    AudioSource currAudioSource;
    string deviceName;
    public void StartRecord()
    {
        if (Microphone.devices.Length > 0)
        {
            currAudioSource = ConvertContextUtils.addComponent<AudioSource>(currentObject);
            deviceName = Microphone.devices[0].ToString();
            if (Microphone.IsRecording(deviceName))
            {
                Debug.Log(deviceName + " is recording!");
                return;
            }
            currAudioSource.clip = Microphone.Start(deviceName, false, 60, 44100);
            recordButton.SetActive(false);
            stopRecordButton.SetActive(true);
        }
        else
        {
            Debug.Log("Cannot find any devices!");
        }
    }

    public void StopRecord()
    {
        recordButton.SetActive(true);
        stopRecordButton.SetActive(false);
        playButton.SetActive(true);
        Microphone.End(deviceName);
    }

    public void PlayRecord()
    {
        AudioSource audioSource = ConvertContextUtils.addComponent<AudioSource>(currentObject);
        audioSource.Play();
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
        TagAlongManager.Instance.ControllerOn();
    }
}
