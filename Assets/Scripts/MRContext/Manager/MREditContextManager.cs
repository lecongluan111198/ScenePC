using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MREditContextManager : MonoBehaviour
{
    [Header("PANEL")]
    public GameObject menuModel;

    [Header("CONTAINER")]
    public GameObject container;
    public GameObject GUI;

    public static MREditContextManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    private Context currentContext;

    void Start()
    {
        MRDataHolder.Instance.IsEdit = true;
        LoadEditContext();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            SaveEditContext();
        }
    }

    public void LoadEditContext()
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
        List<ConvertContextUtils.ContextInfo> contextInfos = ConvertContextUtils.ToGameObjects(json, false);
        if (contextInfos.Count == 0)
        {
            Debug.Log("Convert to ContextInfos error!");
            return;
        }
        //load phase 1
        ConvertContextUtils.ContextInfo info = contextInfos[0];
        KeyValuePair<ContextObject, GameObject> bo = info.Bo;
        //load background
        UpdateBackground(bo.Key, bo.Value);
        foreach (KeyValuePair<ContextObject, GameObject> entry in info.GameObjs)
        {
            GameObject go = entry.Value;
            //if not stroke
            if (!entry.Key.nameDownload.Equals("BrushThinStroke"))
            {
                UpdateModel(go);
            }
            else
            {
                go.transform.parent = container.transform;
            }

            entry.Key.ToGameObject(go);
        }
    }

    private void UpdateBackground(ContextObject bo, GameObject go)
    {
        if (go == null)
            return;
        go.transform.SetParent(GUI.transform);
        ObjBasicInfo bInfo = ConvertContextUtils.AddComponent<ObjBasicInfo>(go);
        bInfo.Id = 1;
        bInfo.DownloadName = bo.nameDownload;
        bInfo.FromServer = false;
        bo.ToGameObject(go);
    }

    public void SaveEditContext()
    {
        Context context = MRDataHolder.Instance.CurrentContext;
        RootObject rootObject = ExportRootObject();
        string json = JSONUtils.toJSONString(rootObject);
        if (json != null && json != "")
        {
            context.Content = StringCompressor.CompressString(json);
            File.WriteAllText(@"Assets/saveJSON.txt", json);
            Debug.Log(json);
            ContextModel.Instance.updateContext(context, (data) =>
            {
                if (!data.Key)
                {
                    Debug.Log(data.Value);
                }
                else
                {
                    Debug.Log(data.Value);
                }
            });
        }
        else
        {
            Debug.Log("Wrong format JSON :" + json);
        }
    }

    private RootObject ExportRootObject()
    {
        List<ContextObject> objects = ToListObj();
        ContextObject bo = ToBackgroundObject();
        List<Phase> phases = ToPhase(bo, objects);
        RootObject rootObject = ToRoot(phases);
        return rootObject;
    }

    private List<ContextObject> ToListObj()
    {
        List<ContextObject> objects = new List<ContextObject>();
        foreach (Transform child in container.transform)
        {
            ObjBasicInfo bInfo = child.GetComponent<ObjBasicInfo>();
            if (bInfo != null)
            {
                ContextObject contextObj = ContextObject.ToContextObject(child.gameObject);
                if (contextObj != null)
                {
                    objects.Add(contextObj);
                }
            }
        }
        return objects;
    }
    private ContextObject ToBackgroundObject()
    {
        ContextObject bo = null;
        foreach (Transform child in GUI.transform)
        {
            ObjBasicInfo bInfo = child.GetComponent<ObjBasicInfo>();
            if (bInfo != null)
            {
                bo = ContextObject.ToContextObject(child.gameObject);
                break;
            }
        }
        if(bo == null)
        {
            bo = new ContextObject(1, "", "", false, new List<double>() { 0, 0, 0 }, new List<double>() { 0, 0, 0 }, new List<double>() { 1, 1, 1 }, new List<AbstractComponent>());
        }
        return bo;
    }
    private List<Phase> ToPhase(ContextObject bo, List<ContextObject> objects)
    {
        List<Phase> phases = new List<Phase>();
        Phase phrs = Phase.toPhase(bo, objects);
        phases.Add(phrs);
        return phases;
    }
    private RootObject ToRoot(List<Phase> phases)
    {
        RootObject rootObject = RootObject.toRootObject(phases);
        return rootObject;
    }



    public void Exit()
    {
        LoadSceneManager.Instance.LoadScene(LoadSceneManager.SceneType.MAINBOARD, false);
        //MRDataHolder.Instance.IsEdit = false;
    }

    public void ShowMenuAddModel()
    {
        menuModel.SetActive(true);
        //TagAlongManager.Instance.ControllerOff();
    }

    public void UpdateModel(GameObject go)
    {
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
    }
}
