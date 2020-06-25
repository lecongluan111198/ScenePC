using Microsoft.MixedReality.Toolkit.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordTransform : MonoBehaviour, IMixedRealityPointerHandler
{
    private bool isStart;
    private List<ObjectStatus> listStatuses;

    [Serializable]
    public class ObjectStatus
    {
        List<double> position;
        List<double> rotation;
        List<double> scale;
        float timeRange;

        public ObjectStatus()
        {
        }

        public ObjectStatus(List<double> position, List<double> rotation, List<double> scale, float timeRange)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.Scale = scale;
            this.TimeRange = timeRange;
        }

        public ObjectStatus(Vector3 position, Quaternion rotation, Vector3 scale, float timeRange)
        {
            this.Position = ConvertTypeUtils.vector3ToList(position);
            this.Rotation = ConvertTypeUtils.quaternionToList(rotation);
            this.Scale = ConvertTypeUtils.vector3ToList(scale);
            this.TimeRange = timeRange;
        }

        public List<double> Position { get => position; set => position = value; }
        public List<double> Rotation { get => rotation; set => rotation = value; }
        public List<double> Scale { get => scale; set => scale = value; }
        public float TimeRange { get => timeRange; set => timeRange = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        isStart = false;
        listStatuses = new List<ObjectStatus>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (MRDataHolder.Instance.IsRecord)
        {
            //record
            if (listStatuses.Count > Math.Round(5f / Time.fixedDeltaTime))
            {
                listStatuses.RemoveAt(0);
            }
            listStatuses.Add(new ObjectStatus(transform.localPosition, transform.localRotation, transform.localScale, 0f));
        }
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
            //StartCoroutine("recordingTransform");
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

            //TODO: active record option
            //MRContextManager.Instance.ShowRecordOption();
            SettingMenuPanel.Instance.ShowRecordOption();
        }
    }

}
