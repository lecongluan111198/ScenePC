using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObject
{
    public string nameBackground { get; set; }
    public List<double> position { get; set; }
    public List<double> rotation { get; set; }
    public List<double> scale { get; set; }
    public BackgroundObject()
    {
    }
    public BackgroundObject(string nameObj, List<double> position, List<double> rotation, List<double> scale)
    {
        this.nameBackground = nameObj;
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
    }

    public static BackgroundObject toBackGroundObject(GameObject go)
    {
        BackgroundObject bo = new BackgroundObject();
        Transform tf = go.transform;
        bo.nameBackground = tf.transform.name.Replace("(Clone)", "");
        bo.position = ConvertTypeUtils.vector3ToList(tf.transform.localPosition);
        bo.rotation = ConvertTypeUtils.quaternionToList(tf.transform.localRotation);
        bo.scale = ConvertTypeUtils.vector3ToList(tf.transform.localScale);
        return bo;
    }
}
