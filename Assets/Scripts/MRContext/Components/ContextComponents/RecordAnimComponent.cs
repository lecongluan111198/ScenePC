using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RecordAnimComponent : AbstractComponent
{
    private EAnimMode mode;
    private List<RecordTransform.ObjectStatus> listStatuses;

    public RecordAnimComponent(string name, EAnimMode mode, List<RecordTransform.ObjectStatus> listStatuses) : base((int) EComponent.RECORD_ANIMATION, name)
    {
        this.Mode = mode;
        this.ListStatuses = new List<RecordTransform.ObjectStatus>(listStatuses);
    }

    public EAnimMode Mode { get => mode; set => mode = value; }
    public List<RecordTransform.ObjectStatus> ListStatuses { get => listStatuses; set => listStatuses = value; }

    public override Type getType()
    {
        return typeof(RecordAnimation);
    }

    public override void updateInfomation(Component component)
    {
        try
        {
            RecordAnimation recordAnim = component as RecordAnimation;
            recordAnim.Mode = Mode;
            recordAnim.ListStatuses = ListStatuses;
        }catch(Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
}
