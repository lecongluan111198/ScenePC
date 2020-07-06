using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipObject : MonoBehaviour
{
    public GameObject pivot;
    public GameObject anchor;

    private MRTooltip targetObject;
    private int currentIndex;

    public int CurrentIndex { get => currentIndex; set => currentIndex = value; }
    public MRTooltip TargetObject { get => targetObject; set => targetObject = value; }

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
        //if(MRDataHolder.Instance.CurrentClickObject != null)
        //{
        //    TargetObject = MRDataHolder.Instance.CurrentClickObject.GetComponent<MRTooltip>();
        //}
        if (MRDataHolder.Instance.IsEdit)
        {
            anchor.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
        else
        {
            anchor.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }
    }

    private void FixedUpdate()
    {
        if (MRDataHolder.Instance.IsEdit)
        {
            if(TargetObject != null)
            {
                TargetObject.UpdatePos(currentIndex, anchor.transform.position, pivot.transform.position);
            }
            else
            {
                
            }
        }
    }

    public void SetPosition(Vector3 anchorPos, Vector3 pivotPos)
    {
        anchor.transform.position = anchorPos;
        pivot.transform.position = pivotPos;
    }

}
