using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionComponent : AbstractComponent
{
    private string question;
    private List<string> choose;
    private string answer;

    public string Question { get => question; set => question = value; }
    public List<string> Choose { get => choose; set => choose = value; }
    public string Answer { get => answer; set => answer = value; }

    public QuestionComponent(string name, string question, List<string> choose, string answer) : base((int)EComponent.QUESTION, name)
    {
        this.question = question;
        this.choose = choose;
        this.answer = answer;
    }
    public QuestionComponent(string name) : base((int)EComponent.QUESTION,name)
    {

    }

    private void loadQuestion()
    {

    }

    public override void updateInfomation(Component component)
    {
        switch (id)
        {
            case 1:
                loadQuestion();
                break;
            case 2:

                break;
            default:
                Debug.Log("ID Component not found");
                break;
        }
    }

    public override Type getType()
    {
        throw new NotImplementedException();
    }
}

