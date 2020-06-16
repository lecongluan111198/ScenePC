using Dummiesman;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConvertContextUtils
{
    public class ContextInfo
    {
        KeyValuePair<ContextObject, GameObject> bo;
        List<KeyValuePair<ContextObject, GameObject>> gameObjs;

        public KeyValuePair<ContextObject, GameObject> Bo { get => bo; set => bo = value; }
        public List<KeyValuePair<ContextObject, GameObject>> GameObjs { get => gameObjs; set => gameObjs = value; }
    }

    public static List<ContextInfo> toGameObjects(string json)
    {
        List<ContextInfo> ret = new List<ContextInfo>();
        RootObject root = JSONUtils.toObject<RootObject>(json);
        foreach (Phase phase in root.Phases)
        {
            ContextInfo info = toContextInfo(phase);
            ret.Add(info);
        }

        return ret;
    }

    private static ContextInfo toContextInfo(Phase phase)
    {
        ContextInfo info = new ContextInfo();
        //BackgroundObject bo = phase.backgroundObject;
        KeyValuePair<ContextObject, GameObject> bo = new KeyValuePair<ContextObject, GameObject>(phase.backgroundObject, toGameObject(phase.backgroundObject));
        List<KeyValuePair<ContextObject, GameObject>> goes = toListGameObject(phase.Objects);
        info.Bo = bo;
        info.GameObjs = goes;
        return info;
    }

    private static List<KeyValuePair<ContextObject, GameObject>> toListGameObject(List<ContextObject> objects)
    {
        List<KeyValuePair<ContextObject, GameObject>> goes = new List<KeyValuePair<ContextObject, GameObject>>();
        foreach (ContextObject co in objects)
        {

            KeyValuePair<ContextObject, GameObject> kvp = new KeyValuePair<ContextObject, GameObject>(co, toGameObject(co));
            goes.Add(kvp);
        }

        return goes;
    }

    private static GameObject toGameObject(ContextObject obj)
    {
        GameObject loadedObj = null;
        bool isFail = false;
        if (obj.nameDownload != null)
        {
            if (obj.fromServer)
            {
                FileModel.Instance._DownloadObject(obj.nameDownload + ".obj", obj.nameDownload + ".mtl", (file) =>
                {
                    if (file != null)
                    {
                        loadedObj = new OBJLoader().Load(new MemoryStream(file[0]), new MemoryStream(file[1]));
                    }

                    if (file == null || loadedObj == null)
                    {
                        isFail = true;
                    }
                });
                while (loadedObj == null && !isFail)
                {

                }
            }
            else
            {
                //GameObject loadedObj = Instantiate(Resources.Load(ResourceManager.MRPrefab + obj.nameDownload) as GameObject);
                try
                {
                    //loadedObj = GameObject.Instantiate(Resources.Load(ResourceManager.MRPrefab + obj.nameDownload) as GameObject);
                    loadedObj = PhotonNetwork.Instantiate(Path.Combine(ResourceManager.MRPrefab, "Templates/Template"), Vector3.zero, Quaternion.identity, 0);
                }
                catch (Exception ex)
                {
                    loadedObj = GameObject.Instantiate(Resources.Load(ResourceManager.MRPrefab + obj.nameDownload) as GameObject);
                }

            }

            //if(loadedObj != null)
            //{
            //    loadedObj.name = obj.nameObj;
            //}
        }
        return loadedObj;
    }
}
