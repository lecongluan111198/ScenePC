using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralTemplate : AbstractTemplate
{
    public GameObject child;

    protected override GameObject UpdateOtherComponents(GameObject src, GameObject dest, ContextObject co)
    {
        Rigidbody rigid = dest.GetComponent<Rigidbody>();
        if (rigid != null)
        {
            rigid.isKinematic = true;
        }

        Animator anim = dest.GetComponent<Animator>();
        if (anim != null)
        {
            anim.applyRootMotion = true;
        }
        return dest;
    }
}
