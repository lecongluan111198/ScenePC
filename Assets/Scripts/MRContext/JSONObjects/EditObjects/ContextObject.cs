using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class ContextObject
{
    public int id { get; set; }
    public string nameObj { get; set; }
    public string nameDownload { get; set; }
    public bool fromServer { get; set; }
    public List<double> position { get; set; }
    public List<double> rotation { get; set; }
    public List<double> scale { get; set; }
    public List<AbstractComponent> components { get; set; }

    public ContextObject()
    {
    }

    public ContextObject(int id, string nameObj, string nameDownload, bool fromServer, List<double> position, List<double> rotation, List<double> scale,  List<AbstractComponent> components)
    {
        this.id = id;
        this.nameObj = nameObj;
        this.nameDownload = nameDownload;
        this.fromServer = fromServer;
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
        this.components = components;
    }

    public GameObject ToGameObject(GameObject go)
    {
        go.transform.localPosition = ConvertTypeUtils.ListToVector3(position);
        go.name = nameObj;
        go.transform.localScale = ConvertTypeUtils.ListToVector3(scale);
        //go.transform.rotation = ConvertTypeUtils.listToQuaternion(rotation);
        //go.transform.rotation.eulerAngles.x = 0;
        Vector3 angle = ConvertTypeUtils.ListToVector3(rotation);
        //go.transform.Rotate(angle.x, angle.y, angle.z);
        go.transform.rotation = Quaternion.Euler(angle);

        //Debug.Log(nameObj + " " + angle.y + " " + go.transform.rotation.eulerAngles.y);

        ObjBasicInfo bInfo = ConvertContextUtils.AddComponent<ObjBasicInfo>(go);
        bInfo.DownloadName = nameDownload;
        bInfo.FromServer = fromServer;
        bInfo.Id = id;

        foreach (AbstractComponent ab in components)
        {
            Component component = null;
            if (go.GetComponent<PlanetTemplate>() && ab.getType() == typeof(MRTooltip))
            {
                go.transform.Find(go.name);
                component = ConvertContextUtils.AddComponent<MRTooltip>(go.transform.Find(go.name).gameObject);
            }
            else
            {
                component = go.GetComponent(ab.getType());
                if (component == null)
                {
                    component = go.AddComponent(ab.getType());
                }
            }
            ab.updateInfomation(component);
        }

        return go;
    }

    public static List<AbstractComponent> ToComponents(GameObject go)
    {
        List<AbstractComponent> abComponents = new List<AbstractComponent>();
        BoxCollider bxc = go.GetComponent<BoxCollider>();
        if (bxc != null)
        {
            abComponents.Add(new BoxColliderComponent("BXC", bxc.isTrigger, ConvertTypeUtils.Vector3ToList(bxc.center), ConvertTypeUtils.Vector3ToList(bxc.size)));
        }
        //expand for another components
        Question qs = go.GetComponent<Question>();
        if(qs != null)
        {
            abComponents.Add(new QuestionComponent("QS", qs.QuestionText, qs.Choose, qs.Answer));
        }

        QuestionV2 qs2 = go.GetComponent<QuestionV2>();
        if (qs2 != null)
        {
            abComponents.Add(new QuestionComponentV2("QS", qs2.Question, qs2.Choose, qs2.Answer));
        }

        CustAnimation anim = go.GetComponent<CustAnimation>();
        if(anim != null)
        {
            abComponents.Add(new AnimationComponent("AIMATION", anim.Mode, anim.ControllerName, anim.ClipName));
        }

        RecordAnimation recordAnim = go.GetComponent<RecordAnimation>();
        if(recordAnim != null)
        {
            abComponents.Add(new RecordAnimComponent("RECORD", recordAnim.Mode, recordAnim.ListStatuses));
        }

        MRTooltip tooltip = go.GetComponent<MRTooltip>();
        if (tooltip != null && tooltip.MapTooltipDetails.Count != 0)
        {
            abComponents.Add(new TooltipComponent("TOOLTIP", new HashSet<TooltipComponent.ToolTipDetail>(tooltip.MapTooltipDetails.Values)));
        }

        Stroke stroke = go.GetComponent<Stroke>();
        if(stroke != null && stroke.Positions.Count != 0)
        {
            abComponents.Add(new StrokeComponent("STROKE", stroke.Positions));
        }
        return abComponents;
    }

    public static ContextObject ToContextObject(GameObject go)
    {
        ObjBasicInfo bInfo = go.GetComponent<ObjBasicInfo>();
        ContextObject ret = null;
        Transform tf = go.transform;
        if(bInfo == null)
        {
            return ret;
        }
        ret = new ContextObject();
        ret.nameObj = go.name;
        ret.nameDownload = bInfo.DownloadName;
        ret.fromServer = bInfo.FromServer;
        ret.id = bInfo.Id;
        ret.position = ConvertTypeUtils.Vector3ToList(tf.localPosition);
        ret.rotation = ConvertTypeUtils.Vector3ToList(tf.localRotation.eulerAngles);
        ret.scale = ConvertTypeUtils.Vector3ToList(tf.localScale);
        ret.components = ToComponents(go);
        return ret;
    }
}
