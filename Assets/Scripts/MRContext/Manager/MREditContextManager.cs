using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MREditContextManager : MonoBehaviour
{
    [Header("CONTAINER")]
    public GameObject container;
    public GameObject GUI;

    private Context currentContext;

    void Start()
    {
        loadEditContext();
    }

    public void loadEditContext()
    {
        if (!MRDataHolder.Instance.IsEdit)
        {
            Debug.Log("mode edit is not enable");
            return;
        }
        currentContext = MRDataHolder.Instance.CurrentContext;
        string json = currentContext.Content;
        if (json == null || "".Equals(json) || "{}".Equals(json))
        {
            json = MRDataHolder.Instance.DefaultContent;
        }
        //list phase
        List<ConvertContextUtils.ContextInfo> contextInfos = ConvertContextUtils.toGameObjects(json, false);
        if (contextInfos.Count == 0)
        {
            Debug.Log("Convert to ContextInfos error!");
            return;
        }
        //load phase 1
        ConvertContextUtils.ContextInfo info = contextInfos[0];
        KeyValuePair<ContextObject, GameObject> bo = info.Bo;
        //load background
        updateBackground(bo.Key, bo.Value);
        foreach (KeyValuePair<ContextObject, GameObject> entry in info.GameObjs)
        {
            GameObject go = entry.Value;
            go.transform.parent = container.transform;

            BoundingBox bbox = go.AddComponent<BoundingBox>();
            bbox.Target = go.gameObject;
            bbox.BoundsOverride = go.GetComponent<BoxCollider>();
            ManipulationHandler mHandler = go.AddComponent<ManipulationHandler>();
            mHandler.HostTransform = go.transform;
            go.AddComponent<NearInteractionGrabbable>();
            //add record MR
            go.AddComponent<RecordTransform>();
            go.AddComponent<ObjectSetting>();

            entry.Key.toGameObject(go);
        }
    }

    private void updateBackground(ContextObject bo, GameObject go)
    {
        go.transform.SetParent(GUI.transform);
        ObjBasicInfo bInfo = ConvertContextUtils.addComponent<ObjBasicInfo>(go);
        bInfo.Id = 1;
        bInfo.DownloadName = bo.nameDownload;
        bInfo.FromServer = false;
        bo.toGameObject(go);
    }

    private void updateContextObject(ContextObject co, GameObject go)
    {
        go.transform.parent = container.transform;
        BoundingBox bbox = ConvertContextUtils.addComponent<BoundingBox>(go);
        bbox.Target = go.gameObject;
        bbox.BoundsOverride = ConvertContextUtils.addComponent<BoxCollider>(go);
        ManipulationHandler mHandler = ConvertContextUtils.addComponent<ManipulationHandler>(go);
        mHandler.HostTransform = go.transform;
        ConvertContextUtils.addComponent<NearInteractionGrabbable>(go);
        //add feature to use in edit mode
        ConvertContextUtils.addComponent<RecordTransform>(go);
        ConvertContextUtils.addComponent<ObjectSetting>(go);
        ObjBasicInfo info = ConvertContextUtils.addComponent<ObjBasicInfo>(go);
        info.Id = 1;
        info.DownloadName = co.nameDownload;
        info.FromServer = co.fromServer;
        co.toGameObject(go);
    }

    public void saveEditContext()
    {
        Context context = MRDataHolder.Instance.CurrentContext;
        RootObject rootObject = exportRootObject();
        string json = JSONUtils.toJSONString(rootObject);
        if (json != null && json != "")
        {
            context.Content = json;
            File.WriteAllText(@"Assets/saveJSON.txt", json);
            Debug.Log(json);
            ContextModel.Instance.updateContext(context, (data) =>
            {
                if (data == null)
                {
                    Debug.Log("Cannot save");
                }
                else
                {
                    Debug.Log("Success");
                }
            });
        }
        else
        {
            Debug.Log("Wrong format JSON :" + json);
        }
    }

    private List<ContextObject> toListObj()
    {
        List<ContextObject> objects = new List<ContextObject>();
        foreach (Transform child in container.transform)
        {
            ObjBasicInfo bInfo = child.GetComponent<ObjBasicInfo>();
            if (bInfo != null)
            {
                ContextObject contextObj = ContextObject.toContextObject(child.gameObject);
                if (contextObj != null)
                {
                    objects.Add(contextObj);
                }
            }
        }
        return objects;
    }
    private ContextObject toBackgroundObject()
    {
        ContextObject bo = null;
        foreach (Transform child in GUI.transform)
        {
            ObjBasicInfo bInfo = child.GetComponent<ObjBasicInfo>();
            if (bInfo != null)
            {
                bo = ContextObject.toContextObject(child.gameObject);
                break;
            }
        }
        return bo;
    }
    private List<Phase> toPhase(ContextObject bo, List<ContextObject> objects)
    {
        List<Phase> phases = new List<Phase>();
        Phase phrs = Phase.toPhase(bo, objects);
        phases.Add(phrs);
        return phases;
    }
    private RootObject toRoot(List<Phase> phases)
    {
        RootObject rootObject = RootObject.toRootObject(phases);
        return rootObject;
    }
    private RootObject exportRootObject()
    {
        List<ContextObject> objects = toListObj();
        //Debug.Log(objects.Count);
        ContextObject bo = toBackgroundObject();
        List<Phase> phases = toPhase(bo, objects);
        RootObject rootObject = toRoot(phases);
        return rootObject;
    }

}
