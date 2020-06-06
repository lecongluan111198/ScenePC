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

        if ((mode == EAnimMode.START || mode == EAnimMode.START_LOOP) && !MRDataHolder.Instance.IsEdit)
        {
            //TODO: start animation
            StartCoroutine("startAnimation");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayAnimation()
    {
        isLoop = false;
        StartCoroutine("startAnimation");
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        //TODO: start animation
        if (!MRDataHolder.Instance.IsEdit)
        {
            StartCoroutine("startAnimation");
        }
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
        if (ListStatuses != null)
        {
            int index = 0;
            int maxIndex = ListStatuses.Count;
            float pre = interval * 1.0f / 1000;
            RecordTransform.ObjectStatus status;
            while (true)
            {
                index = 0;
                while (index < maxIndex)
                {
                    status = ListStatuses[index++];
                    Vector3 destPos = ConvertTypeUtils.listToVector3(status.Position);
                    Quaternion destRotation = ConvertTypeUtils.listToQuaternion(status.Rotation);
                    Vector3 destScale = ConvertTypeUtils.listToVector3(status.Scale);
                    var i = 0.0f;
                    var rate = 1.0f / pre;
                    while (i < 1.0)
                    {
                        i += Time.deltaTime * rate;
                        if(i < 1.0f)
                        {
                            transform.localPosition = Vector3.Lerp(transform.localPosition, destPos, i);
                            transform.localRotation = Quaternion.Lerp(transform.localRotation, destRotation, i);
                            transform.localScale = Vector3.Lerp(transform.localScale, destScale, i);
                        }
                        yield return new WaitForEndOfFrame();
                    }
                }

                if (!isLoop)
                {
                    break;
                }
            }
        }
    }
}
