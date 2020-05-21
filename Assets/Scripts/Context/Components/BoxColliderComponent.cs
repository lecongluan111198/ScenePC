using RTEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderComponent : AbstractComponent
{
    private bool isTrigger;
    private List<double> center;
    private List<double> size;

    public BoxColliderComponent(string name, bool isTrigger, List<double> center, List<double> size) : base((int)EComponent.BOX_COLLIDER, name)
    {
        this.IsTrigger = isTrigger;
        this.Center = center;
        this.Size = size;
    }

    public bool IsTrigger { get => isTrigger; set => isTrigger = value; }
    public List<double> Center { get => center; set => center = value; }
    public List<double> Size { get => size; set => size = value; }

    public override void updateInfomation(Component component)
    {
        ((BoxCollider)component).isTrigger = isTrigger;
        ((BoxCollider)component).center = ConvertTypeUtils.listToVector3(center);
        ((BoxCollider)component).size = ConvertTypeUtils.listToVector3(size);
    }

    public override Type getType()
    {
        return typeof(BoxCollider);
    }
}
