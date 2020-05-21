using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderComponent : AbstractComponent
{
    private bool isTrigger;
    private List<double> center;
    private List<double> size;

    public BoxColliderComponent(bool isTrigger, List<double> center, List<double> size) : base(2, "box collider")
    {
        this.IsTrigger = isTrigger;
        this.Center = center;
        this.Size = size;
    }

    public bool IsTrigger { get => isTrigger; set => isTrigger = value; }
    public List<double> Center { get => center; set => center = value; }
    public List<double> Size { get => size; set => size = value; }

    public override Component toComponent()
    {
        BoxCollider box = new BoxCollider();
        box.isTrigger = isTrigger;
        box.center = ConvertTypeUtils.listToVector3(center);
        box.size = ConvertTypeUtils.listToVector3(size);
        return box;
    }
}
