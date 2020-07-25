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
            Debug.Log(co.nameObj + " doesn't contain template script");
            return;
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
            ManipulationHandler mHandler = ConvertContextUtils.AddComponent<ManipulationHandler>(go);
            mHandler.HostTransform = go.transform;
            ConvertContextUtils.AddComponent<NearInteractionGrabbable>(go);
        }
        temp.UpdateInformation(co);
        //co.toGameObject(go);
    }

    private void UpdateMesh(string srcName, GameObject dest)
    {
        GameObject src = Instantiate(Resources.Load(ResourceManager.MRPrefab + srcName) as GameObject);

        GameObject go = dest;
        Queue<GameObject> parent = new Queue<GameObject>();
        Queue<GameObject> queue = new Queue<GameObject>();
        parent.Enqueue(null);
        queue.Enqueue(src);
        while (queue.Count != 0)
        {
            GameObject exGo = queue.Dequeue();
            GameObject parentGo = parent.Dequeue();

            if (parentGo != null)
            {
                go = new GameObject();
                go.transform.SetParent(parentGo.transform);
            }
            else
            {
                go = dest;
            }
            UpdateTransform(exGo.gameObject, go);
            //getAndUpdateComponent<MeshRenderer>(exGo.gameObject, go);
            UpdateMeshRenderer(exGo.gameObject, go);
            UpdateSkinMeshRenderer(exGo.gameObject, go);
            UpdateMeshFilter(exGo.gameObject, go);
            //getAndUpdateComponent<MeshFilter>(exGo.gameObject, go);
            getAndUpdateComponent<MeshCollider>(exGo.gameObject, go);
            getAndUpdateComponent<BoxCollider>(exGo.gameObject, go);
            go.name = exGo.name;

            foreach (Transform child in exGo.transform)
            {
                queue.Enqueue(child.gameObject);
                parent.Enqueue(go);
            }
        }

        Destroy(src);
    }

    private void UpdateSkinMeshRenderer(GameObject src, GameObject dest)
    {
        SkinnedMeshRenderer com = src.GetComponent<SkinnedMeshRenderer>();
        if (com != null)
        {
            MeshUtils.Combine(src, dest);
            //SkinnedMeshRenderer newCom = addComponent<SkinnedMeshRenderer>(dest);
            //newCom.sharedMesh = com.sharedMesh;
            ////newCom.localBounds.center = com.localBounds.center;
            //newCom.localBounds = com.localBounds;
            //newCom.rootBone = com.rootBone;
            //newCom.bones = com.bones;

            //newCom.forceMatrixRecalculationPerRender = com.forceMatrixRecalculationPerRender;
            //newCom.materials = com.materials;
            //newCom.shadowCastingMode = com.shadowCastingMode;
            //newCom.receiveShadows = com.receiveShadows;

            //newCom.lightProbeUsage = com.lightProbeUsage;
            //newCom.reflectionProbeUsage = com.reflectionProbeUsage;
            //newCom.probeAnchor = com.probeAnchor;
            //newCom.motionVectorGenerationMode = com.motionVectorGenerationMode;
            //newCom.allowOcclusionWhenDynamic = com.allowOcclusionWhenDynamic;
        }
    }

    private void getAndUpdateComponent<T>(GameObject src, GameObject dest) where T : Component
    {
        T comSrc = src.GetComponent<T>();
        if (comSrc == null)
        {
            return;
        }
        T comDest = dest.GetComponent<T>();
        if (comDest == null)
        {
            comDest = ConvertContextUtils.AddComponent<T>(dest);
        }
        ConvertTypeUtils.GetCopyOf(comDest, comSrc);
        //FieldInfo[] fields = type.GetFields();
        //foreach (FieldInfo field in fields)
        //{
        //    field.SetValue(comDest, field.GetValue(comSrc));
        //}
    }


    private void UpdateTransform(GameObject src, GameObject dest)
    {
        dest.transform.localPosition = src.transform.localPosition;
        //dest.transform.localRotation = src.transform.localRotation;
        //List<double> ro = ConvertTypeUtils.vector3ToList(src.transform.localRotation.eulerAngles);
        dest.transform.Rotate(src.transform.localRotation.eulerAngles);
        //dest.transform.rotation = ConvertTypeUtils.listToQuaternion(ro);
        dest.transform.localScale = src.transform.localScale;
    }
    private void UpdateMeshRenderer(GameObject src, GameObject dest)
    {
        MeshRenderer com = src.GetComponent<MeshRenderer>();
        if (com != null)
        {
            MeshRenderer newCom = ConvertContextUtils.AddComponent<MeshRenderer>(dest);
            newCom.materials = com.materials;

            newCom.shadowCastingMode = com.shadowCastingMode;
            newCom.receiveShadows = com.receiveShadows;

            newCom.lightProbeUsage = com.lightProbeUsage;
            newCom.reflectionProbeUsage = com.reflectionProbeUsage;
            newCom.probeAnchor = com.probeAnchor;
            newCom.motionVectorGenerationMode = com.motionVectorGenerationMode;
            newCom.allowOcclusionWhenDynamic = com.allowOcclusionWhenDynamic;
        }
    }
    private void UpdateMeshFilter(GameObject src, GameObject dest)
    {
        MeshFilter com = src.GetComponent<MeshFilter>();
        if (com != null)
        {
            MeshFilter newCom = dest.AddComponent<MeshFilter>();
            newCom.mesh = com.mesh;
        }
    }

    //private T addComponent<T>(GameObject go) where T : Component
    //{
    //    T com = go.GetComponent<T>();
    //    if (com == null)
    //    {
    //        com = go.AddComponent<T>();
    //    }
    //    return com;
    //}

    public void Exit()
    {
        PhotonRoom.instance.LeaveRoom();
        LoadSceneManager.Instance.LoadScene(LoadSceneManager.SceneType.MAINBOARD, false);
    }
}
