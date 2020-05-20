using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertTypeUtils
{
    public static Vector3 listToVector3(List<double> list)
    {
        Vector3 vector3 = new Vector3();
        vector3.x = Convert.ToSingle(list[0]);
        vector3.y = Convert.ToSingle(list[1]);
        vector3.z = Convert.ToSingle(list[2]);
        return vector3;
    }
    public static Quaternion listToQuaternion(List<double> list)
    {
        Quaternion quaternion = new Quaternion();
        quaternion.x = (float)list[0];
        quaternion.y = (float)list[1];
        quaternion.z = (float)list[2];
        return quaternion;
    }

    public static List<double> vector3ToList(Vector3 vector3)
    {
        List<double> list = new List<double>()
            {
                vector3.x,
                vector3.y,
                vector3.z
            };
        return list;
    }
    public static List<double> quaternionToList(Quaternion quaternion)
    {
        List<double> list = new List<double>()
            {
                quaternion.x,
                quaternion.y,
                quaternion.z
            };
        return list;
    }
}
