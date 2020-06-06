using Microsoft.MixedReality.Toolkit.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordTransform : MonoBehaviour, IMixedRealityPointerHandler
{
    private bool isStart;
    private long intervalTime;
    private List<ObjectStatus> listStatuses;

    public class ObjectStatus
    {
        List<double> position;
        List<double> rotation;
        List<double> scale;

        public ObjectStatus()
        {
        }

        public ObjectStatus(List<double> position, List<double> rotation, List<double> scale)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.Scale = scale;
        }

        public ObjectStatus(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            this.Position = ConvertTypeUtils.vector3ToList(position);
            this.Rotation = ConvertTypeUtils.quaternionToList(rotation);
            this.Scale = ConvertTypeUtils.vector3ToList(scale);
        }

        public List<double> Position { get => position; set => position = value; }
        public List<double> Rotation { get => rotation; set => rotation = value; }
        public List<double> Scale { get => scale; set => scale = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        isStart = false;
        intervalTime = MRDataHolder.Instance.RecordInterval;
        listStatuses = new List<ObjectStatus>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnableStartRecord()
    {
        this.isStart = true;
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        if (isStart)
        {
            //TODO: start thread for recording its transform
            MRDataHolder.Instance.IsRecord = true;
            StartCoroutine("recordingTransform");
        }
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        //Debug.Log("OnPointerDragged");
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        if (isStart)
        {
            //TODO: stop thread (record)
            isStart = false;
            MRDataHolder.Instance.IsRecord = false;
            //TODO: save transform, create RecordAnimation
            RecordAnimation recordAnim = gameObject.GetComponent<RecordAnimation>();
            if (recordAnim == null)
            {
                recordAnim = gameObject.AddComponent<RecordAnimation>();
            }
            recordAnim.Mode = EAnimMode.CLICK;
            recordAnim.ListStatuses = listStatuses;
            recordAnim.Interval = intervalTime;

            //TODO: active record option
            MRContextManager.Instance.ShowRecordOption();
        }
    }

    IEnumerator recordingTransform()
    {
        long startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        long current = 0;
        while (isStart)
        {
            current = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if(current - startTime >= intervalTime)
            {
                startTime = current;
                StartCoroutine("saveTransform");
            }
            yield return null;
        }
    }

    IEnumerator saveTransform()
    {
        listStatuses.Add(new ObjectStatus(transform.localPosition, transform.localRotation, transform.localScale));
        yield return null;
    }

}
