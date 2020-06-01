using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordAnimation : MonoBehaviour, IMixedRealityPointerHandler
{
    private EAnimMode mode;
    private long interval;
    private List<RecordTransform.ObjectStatus> listStatuses;
    private bool isLoop = false;

    public EAnimMode Mode { get => mode; set => mode = value; }
    public List<RecordTransform.ObjectStatus> ListStatuses { get => listStatuses; set => listStatuses = value; }
    public long Interval { get => interval; set => interval = value; }

    // Start is called before the first frame update
    void Start()
    {
        switch (mode)
        {
            case EAnimMode.CLICK:
            case EAnimMode.START:
                isLoop = false;
                break;
            case EAnimMode.CLICK_LOOP:
            case EAnimMode.START_LOOP:
                isLoop = true;
                break;
        }

        if (mode == EAnimMode.START || mode == EAnimMode.START_LOOP)
        {
            //TODO: start animation
            StartCoroutine("startAnimation");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        //TODO: start animation
        StartCoroutine("startAnimation");
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
    }

    IEnumerator startAnimation()
    {
        if(ListStatuses != null)
        {
            int index = 0;
            int maxIndex = ListStatuses.Count;
            float pre = interval * 1.0f / 1000;
            RecordTransform.ObjectStatus status;
            while(true)
            {
                index = 0;
                while (index < maxIndex)
                {
                    Debug.Log(index + " " + ListStatuses.Count);
                    status = ListStatuses[index];
                    transform.localPosition = ConvertTypeUtils.listToVector3(status.Position);
                    transform.localRotation = ConvertTypeUtils.listToQuaternion(status.Rotation);
                    transform.localScale = ConvertTypeUtils.listToVector3(status.Scale);
                    index++;
                    yield return new WaitForSeconds(pre);
                }
                if (!isLoop)
                {
                    break;
                }
            }
        }
        yield return null;
    }
}
