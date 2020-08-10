using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MRGamePlayManager : MonoBehaviour
{
    [Header("CONTAINER")]
    public GameObject container;
    public GameObject GUI;

    [Header("HOLOLEN2")]
    public Material handleMaterial;
    public Material handleGrabbedMaterial;
    public GameObject scaleHandlePrefab;
    public GameObject scaleHandleSlatePrefab;
    public GameObject rotationHandlePrefab;


    public PhotonView PV;
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
        //PV = gameObject.GetComponent<PhotonView>();
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

    public void LoadPlayContext()
    {
        currentContext = MRDataHolder.Instance.CurrentContext;
        string json = currentContext.Content;
        if (json == null || "".Equals(json) || "{}".Equals(json))
        {
            json = MRDataHolder.Instance.DefaultContent;
        }
        BinaryFormatter formater = new BinaryFormatter();
        //list phase
        List<ConvertContextUtils.ContextInfo> contextInfos = ConvertContextUtils.ToGameObjects(json);
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
            PV.RPC("UpdateMRObjectComponent", RpcTarget.AllBuffered, bo.Value.name, GUI.name, ms.ToArray(), true);
        }
        foreach (KeyValuePair<ContextObject, GameObject> entry in info.GameObjs)
        {
            using (var ms = new MemoryStream())
            {
                formater.Serialize(ms, entry.Key);
                PV.RPC("UpdateMRObjectComponent", RpcTarget.AllBuffered, entry.Value.name, container.name, ms.ToArray(), false);
            }
        }
    }

    [PunRPC]
    private void UpdateMRObjectComponent(string objName, string containerName, byte[] data, bool isBackground)
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

        GameObject container = GameObject.Find(containerName);
        GameObject go = GameObject.Find(objName);
        if (go == null)
        {
            Debug.Log(co.nameObj + " is null");
            return;
        }

        AbstractTemplate temp = go.GetComponent<AbstractTemplate>();
        if (temp == null)
        {
            if (co.nameDownload.Equals("Gorilla") || co.nameDownload.Equals("Description"))
            {
                //go.name = co.nameObj;
                co.ToGameObject(go);
            }
            else
            {
                Debug.Log(co.nameObj + " doesn't contain template script");
                return;
            }
        }

        if (container != null)
        {
            go.transform.SetParent(container.transform);
        }

        //MR
        //if (co.nameDownload.Equals("BrushThinStroke"))
        //{
        //    temp.UpdateMeshAndTransform(co.nameDownload, temp.gameObject);
        //    return;
        //}
        if (!isBackground && !co.nameDownload.Equals("BrushThinStroke"))
        {
            //BoundingBox bbox = ConvertContextUtils.AddComponent<BoundingBox>(go);
            //bbox.Target = go.gameObject;
            //bbox.BoundsOverride = go.GetComponent<BoxCollider>();
            //bbox.HandleMaterial = handleMaterial;
            //bbox.HandleGrabbedMaterial = handleGrabbedMaterial;
            //bbox.ScaleHandlePrefab = scaleHandlePrefab;
            //bbox.ScaleHandleSlatePrefab = scaleHandleSlatePrefab;
            //bbox.RotationHandleSlatePrefab = rotationHandlePrefab;
            ConvertContextUtils.AddComponent<BoxCollider>(go);
            ManipulationHandler mHandler = ConvertContextUtils.AddComponent<ManipulationHandler>(go);
            mHandler.HostTransform = go.transform;
            ConvertContextUtils.AddComponent<NearInteractionGrabbable>(go);
        }
        if(temp != null)
        {
            temp.UpdateInformation(co);
        }
        //co.toGameObject(go);
    }

    public void Exit()
    {
        PhotonRoom.instance.LeaveRoom();
        LoadSceneManager.Instance.LoadScene(LoadSceneManager.SceneType.MAINBOARD, false);
    }
}
