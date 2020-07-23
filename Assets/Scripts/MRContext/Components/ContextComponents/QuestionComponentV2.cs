using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestionComponentV2 : AbstractComponent
{
    private Content question;
    private List<Content> choose;
    private int answer;


    public QuestionComponentV2(string name, Content question, List<Content> choose, int answer) : base((int)EComponent.AUDIO_QUESTION, name)
    {
        this.Question = question;
        this.Choose = choose;
        this.Answer = answer;
    }

    public Content Question { get => question; set => question = value; }
    public List<Content> Choose { get => choose; set => choose = value; }
    public int Answer { get => answer; set => answer = value; }

    public override Type getType()
    {
        return typeof(QuestionV2);
    }

    public override void updateInfomation(Component component)
    {
        QuestionV2 ques = component as QuestionV2;
        ques.Question = Question;
        ques.Answer = Answer;
        ques.Choose = Choose;
    }
}
