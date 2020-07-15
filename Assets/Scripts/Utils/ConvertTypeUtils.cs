using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class ConvertTypeUtils
{
    public static Vector3 ListToVector3(List<double> list)
    {
        Vector3 vector3 = new Vector3();
        vector3.x = Convert.ToSingle(list[0]);
        vector3.y = Convert.ToSingle(list[1]);
        vector3.z = Convert.ToSingle(list[2]);
        return vector3;
    }
    public static Quaternion ListToQuaternion(List<double> list)
    {
        Quaternion quaternion = new Quaternion();
        quaternion.x = (float)list[0];
        quaternion.y = (float)list[1];
        quaternion.z = (float)list[2];
        return quaternion;
    }

    public static List<double> Vector3ToList(Vector3 vector3)
    {
        List<double> list = new List<double>()
            {
                vector3.x,
                vector3.y,
                vector3.z
            };
        return list;
    }
    public static List<double> QuaternionToList(Quaternion quaternion)
    {
        List<double> list = new List<double>()
            {
                quaternion.x,
                quaternion.y,
                quaternion.z
            };
        return list;
    }

    public static T GetCopyOf<T>(this Component comp, T other) where T : Component
    {
        Type type = comp.GetType();
        if (type != other.GetType()) return null; // type mis-match
        BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
        PropertyInfo[] pinfos = type.GetProperties(flags);
        foreach (var pinfo in pinfos)
        {
            if (pinfo.CanWrite)
            {
                try
                {
                    pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                }
                catch { } // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anything specific.
            }
        }
        FieldInfo[] finfos = type.GetFields(flags);
        foreach (var finfo in finfos)
        {
            finfo.SetValue(comp, finfo.GetValue(other));
        }
        return comp as T;
    }
}
