using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public ContextObject(int id, string nameObj, string nameDownload, bool fromServer, List<double> position, List<double> rotation, List<double> scale)
    {
        this.id = id;
        this.nameObj = nameObj;
        this.nameDownload = nameDownload;
        this.fromServer = fromServer;
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
        this.components = new List<AbstractComponent>();
    }

    public ContextObject(int id, string nameObj, string nameDownload, bool fromServer, List<double> position, List<double> rotation, List<double> scale, List<AbstractComponent> components)
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

    public GameObject toGameObject()
    {
        GameObject go = new GameObject();
        go.transform.localPosition = ConvertTypeUtils.listToVector3(position);
        go.name = nameObj;
        go.transform.localScale = ConvertTypeUtils.listToVector3(scale);
        go.transform.localRotation = ConvertTypeUtils.listToQuaternion(rotation);
        ObjBasicInfo bInfo = go.AddComponent<ObjBasicInfo>();
        bInfo.DownloadName = nameDownload;
        bInfo.FromServer = fromServer;
        bInfo.Id = id;

        foreach (AbstractComponent ab in components)
        {
            Component component = go.AddComponent(ab.getType());
            ab.updateInfomation(component);
        }

        return go;
    }

    public static List<AbstractComponent> toComponents(GameObject go)
    {
        List<AbstractComponent> abComponents = new List<AbstractComponent>();
        BoxCollider bxc = go.GetComponent<BoxCollider>();
        if (bxc != null)
        {
            abComponents.Add(new BoxColliderComponent("BXC", bxc.isTrigger, ConvertTypeUtils.vector3ToList(bxc.center), ConvertTypeUtils.vector3ToList(bxc.size)));
        }
        //expand for another components

        return abComponents;
    }

    public static ContextObject toContextObject(GameObject go)
    {
        ContextObject ret = null;
        Transform tf = go.transform;
        ret = new ContextObject();
        ret.position = ConvertTypeUtils.vector3ToList(tf.localPosition);
        ret.rotation = ConvertTypeUtils.quaternionToList(tf.rotation);
        ret.scale = ConvertTypeUtils.vector3ToList(tf.localScale);
        ret.components = toComponents(go);
        return ret;
    }
}
