using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
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
    }

    // Start is called before the first frame update
    void Start()
    {
        PV = gameObject.GetComponent<PhotonView>();
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
        PV.RPC("updateMRObjectComponent", RpcTarget.AllBuffered, GUI.name, bo.Key);
        foreach (KeyValuePair<ContextObject, GameObject> entry in info.GameObjs)
        {
            PV.RPC("updateMRObjectComponent", RpcTarget.AllBuffered, container.name, entry.Key);
        }
    }

    [PunRPC]
    private void updateMRObjectComponent(string containerName, ContextObject co)
    {
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
        BoundingBox bbox = go.AddComponent<BoundingBox>();
        bbox.Target = go.gameObject;
        bbox.BoundsOverride = go.GetComponent<BoxCollider>();
        ManipulationHandler mHandler = go.AddComponent<ManipulationHandler>();
        mHandler.HostTransform = go.transform;
        go.AddComponent<NearInteractionGrabbable>();

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
