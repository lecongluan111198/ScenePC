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
            PV.RPC("updateMRObjectComponent", RpcTarget.AllBuffered, bo.Value.name, GUI.name, ms.ToArray(), true);
        }
        foreach (KeyValuePair<ContextObject, GameObject> entry in info.GameObjs)
        {
            using (var ms = new MemoryStream())
            {
                formater.Serialize(ms, entry.Key);
                PV.RPC("updateMRObjectComponent", RpcTarget.AllBuffered, entry.Value.name, container.name, ms.ToArray(), false);
            }
        }
    }

    [PunRPC]
    private void updateMRObjectComponent(string objName, string containerName, byte[] data, bool isBackground)
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

        //update mesh to template object
        updateMesh(co.nameDownload, go);
        ConvertContextUtils.addComponent<ObjectSetting>(go);

        go.name = co.nameObj;

        if (container != null)
        {
            go.transform.SetParent(container.transform);
        }

        //MR
        if (!isBackground)
        {
            BoundingBox bbox = addComponent<BoundingBox>(go);
            bbox.Target = go.gameObject;
            bbox.BoundsOverride = go.GetComponent<BoxCollider>();
            bbox.HandleMaterial = handleMaterial;
            bbox.HandleGrabbedMaterial = handleGrabbedMaterial;
            bbox.ScaleHandlePrefab = scaleHandlePrefab;
            bbox.ScaleHandleSlatePrefab = scaleHandleSlatePrefab;
            bbox.RotationHandleSlatePrefab = rotationHandlePrefab;
            ManipulationHandler mHandler = addComponent<ManipulationHandler>(go);
            mHandler.HostTransform = go.transform;
            addComponent<NearInteractionGrabbable>(go);
        }
        else
        {
            Rigidbody rigid = go.GetComponent<Rigidbody>();
            if (rigid != null)
            {
                rigid.isKinematic = true;
            }
        }

        co.toGameObject(go);
    }

    private void updateMesh(string srcName, GameObject dest)
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
            getAndUpdateTransform(exGo.gameObject, go);
            //getAndUpdateComponent<MeshRenderer>(exGo.gameObject, go);
            getAndUpdateMeshRenderer(exGo.gameObject, go);
            getAndUpdateMeshFilter(exGo.gameObject, go);
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
            comDest = addComponent<T>(dest);
        }
        ConvertTypeUtils.GetCopyOf(comDest, comSrc);
        //FieldInfo[] fields = type.GetFields();
        //foreach (FieldInfo field in fields)
        //{
        //    field.SetValue(comDest, field.GetValue(comSrc));
        //}
    }


    private void getAndUpdateTransform(GameObject src, GameObject dest)
    {
        dest.transform.localPosition = src.transform.localPosition;
        dest.transform.localRotation = src.transform.localRotation;
        dest.transform.localScale = src.transform.localScale;
    }
    private void getAndUpdateMeshRenderer(GameObject src, GameObject dest)
    {
        MeshRenderer com = src.GetComponent<MeshRenderer>();
        if (com != null)
        {
            MeshRenderer newCom = addComponent<MeshRenderer>(dest);
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
    private void getAndUpdateMeshFilter(GameObject src, GameObject dest)
    {
        MeshFilter com = src.GetComponent<MeshFilter>();
        if (com != null)
        {
            MeshFilter newCom = dest.AddComponent<MeshFilter>();
            newCom.mesh = com.mesh;
        }
    }

    private T addComponent<T>(GameObject go) where T : Component
    {
        T com = go.GetComponent<T>();
        if (com == null)
        {
            com = go.AddComponent<T>();
        }
        return com;
    }

    public void Exit()
    {
        PhotonRoom.instance.LeaveRoom();
        LoadSceneManager.Instance.LoadScene(LoadSceneManager.SceneType.MAINBOARD, false);
    }
}
