﻿using System.Collections;
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
    }

    public void ChangeQuestionType(int type)
    {
        this.questionType = (EContent) type;
        if(type == (int)EContent.TEXT)
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
        //Question com = ConvertContextUtils.addComponent<Question>(currentObject);
        //com.QuestionText = question.text;
        //com.Choose = new List<string>() { ansA.text, ansB.text, ansC.text, ansD.text };
        //foreach (Toggle tog in toggleGroup.ActiveToggles())
        //{
        //    if (tog.isOn)
        //    {
        //        switch (tog.name)
        //        {
        //            case "A":
        //                correctPos = 0;
        //                break;
        //            case "B":
        //                correctPos = 1;
        //                break;
        //            case "C":
        //                correctPos = 2;
        //                break;
        //            case "D":
        //                correctPos = 3;
        //                break;
        //        }
        //        break;
        //    }
        //}
        //com.Answer = correctPos;
        //Cancel();

        QuestionV2 com = ConvertContextUtils.addComponent<QuestionV2>(currentObject);

        switch (questionType)
        {
            case EContent.TEXT:
                com.Question = new TextContent(question.text);
                com.Choose = new List<Content>()
                {
                    new TextContent(ansA.text),
                    new TextContent(ansB.text),
                    new TextContent(ansC.text),
                    new TextContent(ansD.text)
                };
                break;
            case EContent.VOICE:
                break;
        }
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

    public void StartRecord()
    {
        if(Microphone.devices.Length > 0)
        {
            AudioSource audioSource = ConvertContextUtils.addComponent<AudioSource>(currentObject);
            string deviceName = Microphone.devices[0].ToString();
            Debug.Log(deviceName);
            audioSource.clip = Microphone.Start(deviceName, true, 10, 44100);
        }
        else
        {
            Debug.Log("Cannot find any devices!");
        }
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
