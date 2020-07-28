using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordAnimation : MonoBehaviour, IMixedRealityPointerHandler
{
    private EAnimMode mode;
    private List<RecordTransform.ObjectStatus> listStatuses;
    private bool isLoop = false;
    private bool isRewind = false;
    private Rigidbody rigid;

    public EAnimMode Mode { get => mode; set => mode = value; }
    public List<RecordTransform.ObjectStatus> ListStatuses { get => listStatuses; set => listStatuses = value; }

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
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
            StartRewind();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    int index = 0;
    private void FixedUpdate()
    {
        if (isRewind)
        {
            if (index < listStatuses.Count)
            {
                RecordTransform.ObjectStatus status = listStatuses[index];
                transform.localPosition = ConvertTypeUtils.ListToVector3(status.Position);
                transform.localRotation = Quaternion.Euler(ConvertTypeUtils.ListToVector3(status.Rotation));
                transform.localScale = ConvertTypeUtils.ListToVector3(status.Scale);
                index++;
            }
            else
            {
                index = 0;
                if (!isLoop)
                {
                    StopRewind();
                }
            }
        }
    }

    public void PlayAnimation()
    {
        isLoop = false;
        StartRewind();
    }

    private void StartRewind()
    {
        isRewind = true;
        if (rigid != null)
        {
            rigid.isKinematic = true;
        }
    }
    private void StopRewind()
    {
        isRewind = false;
        if (rigid != null)
        {
            rigid.isKinematic = false;
        }
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        //TODO: start animation
        if (mode == EAnimMode.CLICK || mode == EAnimMode.CLICK_LOOP)
        {
            if (!MRDataHolder.Instance.IsEdit)
            {
                StartRewind();
            }
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

    //IEnumerator startAnimation()
    //{
    //    if (ListStatuses != null)
    //    {
    //        int index = 0;
    //        int maxIndex = ListStatuses.Count;
    //        float pre = interval * 1.0f / 1000;
    //        RecordTransform.ObjectStatus status;
    //        while (true)
    //        {
    //            index = 0;
    //            while (index < maxIndex)
    //            {
    //                status = ListStatuses[index++];
    //                Vector3 destPos = ConvertTypeUtils.listToVector3(status.Position);
    //                Quaternion destRotation = ConvertTypeUtils.listToQuaternion(status.Rotation);
    //                Vector3 destScale = ConvertTypeUtils.listToVector3(status.Scale);
    //                var i = 0.0f;
    //                var rate = 1.0f / pre;
    //                while (i < pre)
    //                {
    //                    i += Time.deltaTime * rate;
    //                    if (i < 1.0f)
    //                    {
    //                        transform.localPosition = Vector3.Lerp(transform.localPosition, destPos, i);
    //                        transform.localRotation = Quaternion.Lerp(transform.localRotation, destRotation, i);
    //                        transform.localScale = Vector3.Lerp(transform.localScale, destScale, i);
    //                    }
    //                    yield return null;
    //                }
    //            }

    //            if (!isLoop)
    //            {
    //                break;
    //            }
    //        }
    //    }
    //}
    //IEnumerator startAnimationV2()
    //{
    //    if (ListStatuses != null)
    //    {
    //        if (rigid != null)
    //        {
    //            rigid.isKinematic = true;
    //        }
    //        int index = 0;
    //        int maxIndex = ListStatuses.Count;
    //        float pre = interval * 1.0f / 1000;
    //        RecordTransform.ObjectStatus status;
    //        while (true)
    //        {
    //            index = 0;
    //            while (index < maxIndex)
    //            {
    //                status = ListStatuses[index++];
    //                Vector3 destPos = ConvertTypeUtils.listToVector3(status.Position);
    //                Quaternion destRotation = ConvertTypeUtils.listToQuaternion(status.Rotation);
    //                Vector3 destScale = ConvertTypeUtils.listToVector3(status.Scale);
    //                float start = Time.time;
    //                float timeRange = 0f;
    //                while (timeRange <= pre)
    //                {
    //                    timeRange = Time.time - start;
    //                    transform.localPosition = Vector3.Lerp(transform.localPosition, destPos, timeRange / pre);
    //                    transform.localRotation = Quaternion.Lerp(transform.localRotation, destRotation, timeRange / pre);
    //                    transform.localScale = Vector3.Lerp(transform.localScale, destScale, timeRange / pre);
    //                    yield return null;
    //                }
    //            }

    //            if (!isLoop)
    //            {
    //                break;
    //            }
    //        }
    //        if (rigid != null)
    //        {
    //            rigid.isKinematic = false;
    //        }
    //    }
    //}
}
