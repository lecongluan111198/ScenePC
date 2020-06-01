using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComponent : AbstractComponent
{

    private EAnimMode mode;
    private string controllerName;
    private string clipName;

    public EAnimMode Mode { get => mode; set => mode = value; }
    public string ControllerName { get => controllerName; set => controllerName = value; }
    public string ClipName { get => clipName; set => clipName = value; }

    public AnimationComponent(string name, EAnimMode mode, string controllerName, string clipName) : base((int)EComponent.ANIMTION, name)
    {
        this.Mode = mode;
        this.ClipName = clipName;
        this.ControllerName = controllerName;
    }

    public override Type getType()
    {
        return typeof(CustAnimation);
    }

    public override void updateInfomation(Component component)
    {
        try
        {
            CustAnimation anim = component as CustAnimation;
            anim.ClipName = this.ClipName;
            anim.Mode = this.Mode;
            anim.ControllerName = this.ControllerName;
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
}
