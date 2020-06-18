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

    public void loadEditContext()
    {
        currentContext = MRDataHolder.Instance.CurrentContext;
        string json = currentContext.Content;
        if (json == null || "".Equals(json) || "{}".Equals(json))
        {
            json = MRDataHolder.Instance.DefaultContent;
        }
        //list phase
        List<ConvertContextUtils.ContextInfo> contextInfos = ConvertContextUtils.toGameObjects(json);
        if (contextInfos.Count == 0)
        {
            Debug.Log("Convert to ContextInfos error!");
            return;
        }
        //load phase 1
        ConvertContextUtils.ContextInfo info = contextInfos[0];
        foreach (KeyValuePair<ContextObject, GameObject> entry in info.GameObjs)
        {
            GameObject go = entry.Value;
            go.transform.parent = container.transform;
            if (MRDataHolder.Instance.IsMR)
            {
                BoundingBox bbox = go.AddComponent<BoundingBox>();
                bbox.Target = go.gameObject;
                bbox.BoundsOverride = go.GetComponent<BoxCollider>();
                ManipulationHandler mHandler = go.AddComponent<ManipulationHandler>();
                mHandler.HostTransform = go.transform;
                go.AddComponent<NearInteractionGrabbable>();
                //add record MR
                go.AddComponent<RecordTransform>();
                go.AddComponent<ObjectSetting>();
            }
            entry.Key.toGameObject(go);
        }
    }


    private void updateBackground(ContextObject bo, GameObject go)
    {
        go.transform.SetParent(GUI.transform);
        ObjBasicInfo bInfo = go.GetComponent<ObjBasicInfo>();
        if (bInfo == null)
        {
            bInfo = go.AddComponent<ObjBasicInfo>();
        }
        bInfo.Id = 1;
        bInfo.DownloadName = bo.nameDownload;
        bInfo.FromServer = false;
    }

    private void loadBackground(BackgroundObject bo)
    {
        GameObject go = PhotonNetwork.Instantiate(Path.Combine(ResourceManager.MRPrefab, bo.nameBackground), ConvertTypeUtils.listToVector3(bo.position), ConvertTypeUtils.listToQuaternion(bo.rotation), 0);
        //GameObject go = Instantiate(Resources.Load(ResourceManager.MRPrefab + bo.nameBackground) as GameObject, ConvertTypeUtils.listToVector3(bo.position), ConvertTypeUtils.listToQuaternion(bo.rotation), GUI.transform);
        go.transform.SetParent(GUI.transform);
        ObjBasicInfo bInfo = go.GetComponent<ObjBasicInfo>();
        if (bInfo == null)
        {
            bInfo = go.AddComponent<ObjBasicInfo>();
        }
        bInfo.Id = 1;
        bInfo.DownloadName = bo.nameBackground;
        bInfo.FromServer = false;

    }
}
