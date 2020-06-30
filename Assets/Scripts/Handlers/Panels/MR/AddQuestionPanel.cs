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

    private GameObject currentObject;
    public int correctPos = 0;
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

    public void Save()
    {
        Question com = ConvertContextUtils.addComponent<Question>(currentObject);
        com.QuestionText = question.text;
        com.Choose = new List<string>() { ansA.text, ansB.text, ansC.text, ansD.text};
        foreach(Toggle tog in toggleGroup.ActiveToggles())
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

    public void Cancel()
    {
        gameObject.SetActive(false);
        TagAlongManager.Instance.ControllerOn();
    }
}
