using Dummiesman;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using WebSocketSharp;

public class EditMR : MonoBehaviour
{
    [Header("CONTAINER")]
    public GameObject container;

    public GameObject GUI;

    private OBJLoader loader = new OBJLoader();

    //**Create and define**
    private List<AbstractComponent> getComponents()
    {

        return null;
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
    private BackgroundObject toBackgroundObject()
    {
        BackgroundObject bo = null;
        foreach (Transform child in GUI.transform)
        {
            ObjBasicInfo bInfo = child.GetComponent<ObjBasicInfo>();
            if (bInfo != null)
            {
                bo = BackgroundObject.toBackGroundObject(child.gameObject);
                break;
            }
        }
        return bo;
    }
    private List<Phase> toPhase(BackgroundObject bo, List<ContextObject> objects)
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
        BackgroundObject bo = toBackgroundObject();
        List<Phase> phases = toPhase(bo, objects);
        RootObject rootObject = toRoot(phases);
        return rootObject;
    }
    public void saveJson()
    {
        Context context = EditContextHolder.Instance.CurrentContext;
        RootObject rootObject = exportRootObject();
        string json = JSONUtils.toJSONString(rootObject);
        if (!json.IsNullOrEmpty())
        {
            context.Content = json;
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
    }
    //read data 
    private void loadPhase(RootObject rootObject, int phaseIndex)
    {
        List<Phase> listPhase = rootObject.Phases;
        //load only 1 phase
        if (listPhase != null && listPhase.Count > phaseIndex)
        {
            Phase phase = listPhase[phaseIndex];
            BackgroundObject bo = phase.backgroundObject;
            //load background
            loadBackground(bo);
            //load gameobject
            foreach (ContextObject obj in phase.Objects)
            {
                loadGameObject(obj);
            }
        }
        else
        {
            Debug.Log("Cannot load phase at " + phaseIndex);
        }
    }

    private void updateObject(GameObject go, ContextObject obj)
    {
        obj.toGameObject(go);
        go.transform.parent = container.transform;
        if (EditContextHolder.Instance.IsMR)
        {
            Debug.Log(obj.nameObj);
            go.AddComponent<NearInteractionGrabbable>();
            BoundingBox bbox = go.AddComponent<BoundingBox>();
            bbox.Target = go.gameObject;
            bbox.BoundsOverride = go.GetComponent<BoxCollider>();
            ManipulationHandler mHandler = go.AddComponent<ManipulationHandler>();
            mHandler.HostTransform = go.transform;
        }
    }

    private void loadGameObject(ContextObject obj)
    {
        if (obj.nameDownload == null)
        {
            Debug.Log("source null " + obj.nameDownload);
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.localPosition = ConvertTypeUtils.listToVector3(obj.position);
            go.transform.localScale = ConvertTypeUtils.listToVector3(obj.scale);
            go.transform.localRotation = ConvertTypeUtils.listToQuaternion(obj.rotation);
            go.transform.parent = container.transform;
        }
        else
        {
            if (obj.fromServer)
            {
                Debug.Log("start download " + obj.nameDownload);
                FileModel.Instance._DownloadObject(obj.nameDownload + ".obj", obj.nameDownload + ".mtl", (file) =>
                {
                    if (file != null)
                    {
                        GameObject loadedObj = new OBJLoader().Load(new MemoryStream(file[0]), new MemoryStream(file[1]));
                        updateObject(loadedObj, obj);
                    }
                });
            }
            else
            {
                GameObject loadedObj = Instantiate(Resources.Load(ResourceManager.MRPrefab + obj.nameDownload) as GameObject);
                updateObject(loadedObj, obj);
            }

        }
    }
    private void loadBackground(BackgroundObject bo)
    {
        Instantiate(Resources.Load(ResourceManager.MRPrefab + bo.nameBackground) as GameObject, ConvertTypeUtils.listToVector3(bo.position), ConvertTypeUtils.listToQuaternion(bo.rotation), GUI.transform);
    }

    public void loadJson()
    {
        Context context = EditContextHolder.Instance.CurrentContext;
        string json = context.Content;
        if (json.IsNullOrEmpty() || "{}".Equals(json))
        {
            json = EditContextHolder.Instance.DefaultContent;
        }
        Debug.Log(json);
        RootObject rootObject = JSONUtils.toObject<RootObject>(json);
        loadPhase(rootObject, 0);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            saveJson();
        if (Input.GetKeyDown(KeyCode.L))
            loadJson();
    }
}
