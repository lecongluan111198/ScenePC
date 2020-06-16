using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MRGamePlayManager : MonoBehaviour
{
    [Header("CONTAINER")]
    public GameObject container;
    public GameObject GUI;

    private PhotonView PV;
    private Context currentContext;

    public static MRGamePlayManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
        PV = gameObject.GetComponent<PhotonView>();
        Debug.Log(PV);

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadPlayContext()
    {
        currentContext = MRDataHolder.Instance.CurrentContext;
        string json = currentContext.Content;
        if (json == null || "".Equals(json) || "{}".Equals(json))
        {
            json = MRDataHolder.Instance.DefaultContent;
        }
        BinaryFormatter formater = new BinaryFormatter();
        //list phase
        List<ConvertContextUtils.ContextInfo> contextInfos = ConvertContextUtils.toGameObjects(json);
        if (contextInfos.Count == 0)
        {
            Debug.Log("Convert to ContextInfos error!");
            return;
        }
        //load phase 1
        ConvertContextUtils.ContextInfo info = contextInfos[0];
        KeyValuePair<ContextObject, GameObject> bo = info.Bo;
        using (var ms = new MemoryStream())
        {
            formater.Serialize(ms, bo.Key);
            PV.RPC("updateMRObjectComponent", RpcTarget.AllBuffered, GUI.name, ms.ToArray(), true);
        }
        foreach (KeyValuePair<ContextObject, GameObject> entry in info.GameObjs)
        {
            using (var ms = new MemoryStream())
            {
                formater.Serialize(ms, entry.Key);
                PV.RPC("updateMRObjectComponent", RpcTarget.AllBuffered, container.name, ms.ToArray(), false);
            }
        }
    }

    [PunRPC]
    private void updateMRObjectComponent(string containerName, byte[] data, bool isBackground)
    {
        ContextObject co = null;
        using (var ms = new MemoryStream(data))
        {
            co = (ContextObject)(new BinaryFormatter()).Deserialize(ms);
        }
        if (co == null)
        {
            Debug.Log("ContextObject is null");
        }
        Debug.Log(co.nameDownload);

        GameObject container = GameObject.Find(containerName);
        GameObject go = GameObject.Find(co.nameObj);
        if (go == null)
        {
            return;
        }
        if (container != null)
        {
            go.transform.SetParent(container.transform);
        }

        //MR
        if (!isBackground)
        {
            BoundingBox bbox = go.AddComponent<BoundingBox>();
            bbox.Target = go.gameObject;
            bbox.BoundsOverride = go.GetComponent<BoxCollider>();
            ManipulationHandler mHandler = go.AddComponent<ManipulationHandler>();
            mHandler.HostTransform = go.transform;
            go.AddComponent<NearInteractionGrabbable>();
        }
        //Photon
        //TODO: add necessary components for multiplayer mode
        //PhotonView pv = go.AddComponent<PhotonView>();
        //pv.ViewID = currentPVId++;
        //PhotonTransformView ptv = go.AddComponent<PhotonTransformView>();
        //ptv.m_SynchronizePosition = true;
        //ptv.m_SynchronizeRotation = true;
        //ptv.m_SynchronizeScale = true;
        //PhotonAnimatorView pav = go.AddComponent<PhotonAnimatorView>();
        //List<PhotonAnimatorView.SynchronizedParameter> listParam = pav.GetSynchronizedParameters();
        //foreach (PhotonAnimatorView.SynchronizedParameter param in listParam)
        //{
        //    param.SynchronizeType = PhotonAnimatorView.SynchronizeType.Discrete;
        //}
        //pv.ObservedComponents = new List<Component>();
        //pv.ObservedComponents.Add(ptv);
        //pv.ObservedComponents.Add(pav);
        //go.AddComponent<SynchronizeEvent>();

        co.toGameObject(go);
    }
}
