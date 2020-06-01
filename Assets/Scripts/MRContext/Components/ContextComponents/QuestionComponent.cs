using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionComponent : AbstractComponent
{
    private string questionText;
    private List<string> choose;
    private int answer;

    public string QuestionText { get => questionText; set => questionText = value; }
    public List<string> Choose { get => choose; set => choose = value; }
    public int Answer { get => answer; set => answer = value; }

    public QuestionComponent(string name, string question, List<string> choose, int answer) : base((int)EComponent.QUESTION, name)
    {
        this.questionText = question;
        this.choose = choose;
        this.answer = answer;
    }

    private void loadQuestion()
    {

    }

    public override void updateInfomation(Component component)
    {
        try
        {
            Question qs = component as Question;
            //qs.QuestionText = this.QuestionText;
            //qs.Choose = Choose;
            //qs.Answer = Answer;
            qs.Ques = this.QuestionText;
            qs.listChoose = Choose;
            qs.ans = Answer;
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }

    }

    public override Type getType()
    {
        return typeof(Question);
    }
}

