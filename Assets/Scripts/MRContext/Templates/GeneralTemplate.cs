using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralTemplate : AbstractTemplate
{
    public GameObject child;

    public override void UpdateInformation(ContextObject co)
    {
        GameObject go = gameObject;
        //update mesh to template object
        UpdateMeshAndTransform(co.nameDownload, go);
        ConvertContextUtils.AddComponent<ObjectSetting>(go);

        go.name = co.nameObj;

        Rigidbody rigid = go.GetComponent<Rigidbody>();
        if (rigid != null)
        {
            rigid.isKinematic = true;
        }

        Animator anim = go.GetComponent<Animator>();
        if (anim != null)
        {
            anim.applyRootMotion = true;
        }

        co.ToGameObject(go);
    }

}
