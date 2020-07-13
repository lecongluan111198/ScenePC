using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureController : MonoBehaviour
{

    private GameObject currentObject;
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
    }

    public void playAnimation()
    {
        CustAnimation anim = currentObject.GetComponent<CustAnimation>();
        if(anim != null)
        {
            anim.Play();
        }
        else
        {
            RecordAnimation record = currentObject.GetComponent<RecordAnimation>();
            if(record != null)
            {
                record.PlayAnimation();
            }
        }
    }

    public void showQuestion()
    {
        //Question question = currentObject.GetComponent<Question>();
        //if(question != null)
        //{
        //    question.showQuestion();
        //}
        QuestionV2 question = currentObject.GetComponent<QuestionV2>();
        if(question != null)
        {
            question.showQuestion();
        }
    }

    public void close()
    {
        TagAlongManager.Instance.ControllerIn();
    }
}
