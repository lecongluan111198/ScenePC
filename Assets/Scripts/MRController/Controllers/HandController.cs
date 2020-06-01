using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : AbstractController
{
    protected override void doOnDisable()
    {
        //Debug.Log("doOnDisable");
    }

    protected override void doOnEnable()
    {
        //Debug.Log("doOnEnable");
    }

    protected override void doOnStart()
    {
        this.Type = ControllerType.HAND;
        //Debug.Log("doOnStart");
    }

    protected override void doOnUpdate()
    {
        //Debug.Log("doOnUpdate");
    }
}