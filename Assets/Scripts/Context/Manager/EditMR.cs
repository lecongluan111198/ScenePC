using Dummiesman;
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

    private Context context;

    //**Create and define**
    public class ContextObject
    {
        public int id { get; set; }
        public string nameObj { get; set; }
        public string nameDownload { get; set; }
        public bool fromServer { get; set; }
        public List<double> position { get; set; }
        public List<double> rotation { get; set; }
        public List<double> scale { get; set; }
        public List<AbstractComponent> components { get; set; }

        public ContextObject()
        {
        }

        public ContextObject(int id, string nameObj, string nameDownload, bool fromServer, List<double> position, List<double> rotation, List<double> scale)
        {
            this.id = id;
            this.nameObj = nameObj;
            this.nameDownload = nameDownload;
            this.fromServer = fromServer;
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
            this.components = new List<AbstractComponent>();
        }

        public ContextObject(int id, string nameObj, string nameDownload, bool fromServer, List<double> position, List<double> rotation, List<double> scale, List<AbstractComponent> components)
        {
            this.id = id;
            this.nameObj = nameObj;
            this.nameDownload = nameDownload;
            this.fromServer = fromServer;
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
            this.components = components;
        }
    }
    public class BackgroundObject
    {
        public string nameBackground { get; set; }
        public List<double> position { get; set; }
        public List<double> rotation { get; set; }
        public List<double> scale { get; set; }
        public BackgroundObject()
        {
        }
        public BackgroundObject(string nameObj, List<double> position, List<double> rotation, List<double> scale)
        {
            this.nameBackground = nameObj;
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }
    }

    public class Phase
    {
        public int nObject { get; set; }
        public BackgroundObject backgroundObject { get; set; }
        public List<ContextObject> Objects { get; set; }
    }

    public class RootObject
    {
        public int nPhase { get; set; }
        public List<Phase> Phase { get; set; }
    }
    //add object, data to list and save
    private void addObjToList(List<ContextObject> objects)
    {
        ContextObject obje;
        List<double> position, rotation, scale;
        foreach (Transform child in container.transform)
        {
            BasicInformation bInfo = child.GetComponent<BasicInformation>();
            if (bInfo != null)
            {
                position = ConvertTypeUtils.vector3ToList(child.localPosition);
                rotation = ConvertTypeUtils.quaternionToList(child.rotation);
                scale = ConvertTypeUtils.vector3ToList(child.localScale);
                //id,nameObj, nameDownload, fromServer, position, rotation, scale, component
                obje = new ContextObject(bInfo.Id, child.name, bInfo.DownloadName, bInfo.FromServer, position, rotation, scale);
                objects.Add(obje);
            }
        }
    }
    private void addBackgroundPhase(BackgroundObject bo)
    {
        foreach (Transform ts in GUI.transform)
        {
            bo.nameBackground = ts.transform.name.Replace("(Clone)","");
            bo.position = ConvertTypeUtils.vector3ToList(ts.transform.localPosition);
            bo.rotation = ConvertTypeUtils.quaternionToList(ts.transform.localRotation);
            bo.scale = ConvertTypeUtils.vector3ToList(ts.transform.localScale);
            break;
        }
    }
    private void addDataToPhase(List<Phase> phase, BackgroundObject bo, List<ContextObject> objects)
    {
        Phase phrs = new Phase();
        phrs.nObject = objects.Count;
        phrs.Objects = objects;
        phrs.backgroundObject = bo;
        phase.Add(phrs);
    }
    private void addDataToRoot(RootObject rootObject, List<Phase> phases)
    {
        rootObject.nPhase = phases.Count;
        rootObject.Phase = phases;
    }

    private RootObject exportRootObject()
    {
        RootObject rootObject = new RootObject();
        List<Phase> phases = new List<Phase>();
        BackgroundObject bo = new BackgroundObject();
        List<ContextObject> objects = new List<ContextObject>();

        //Get all children and add to objects
        addObjToList(objects);
        //Get background
        addBackgroundPhase(bo);
        //Get all object add to phase
        addDataToPhase(phases, bo, objects);
        //Get all phase add to root
        addDataToRoot(rootObject, phases);
        return rootObject;
    }
    public void saveJson()
    {
        Context context = EditContextHolder.Instance.CurrentContext;
        RootObject rootObject = exportRootObject();
        string json = JsonConvert.SerializeObject(rootObject);
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
        List<Phase> listPhase = rootObject.Phase;
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
                    Debug.Log("file " + file[0]);
                    if (file != null)
                    {
                        GameObject loadedObj = new OBJLoader().Load(new MemoryStream(file[0]), new MemoryStream(file[1]));
                        loadedObj.transform.localPosition = ConvertTypeUtils.listToVector3(obj.position);
                        loadedObj.name = obj.nameObj;
                        loadedObj.transform.localScale = ConvertTypeUtils.listToVector3(obj.scale);
                        loadedObj.transform.localRotation = ConvertTypeUtils.listToQuaternion(obj.rotation);
                        loadedObj.transform.parent = container.transform;
                        BasicInformation bInfo = loadedObj.AddComponent<BasicInformation>();
                        bInfo.DownloadName = obj.nameDownload;
                        bInfo.FromServer = true;
                        bInfo.Id = obj.id;
                    }
                });
            }
            else
            {
                GameObject loadedObj = Instantiate(Resources.Load("Prefabs/MR/" + obj.nameDownload) as GameObject, ConvertTypeUtils.listToVector3(obj.position), ConvertTypeUtils.listToQuaternion(obj.rotation), container.transform);
                loadedObj.transform.localScale = ConvertTypeUtils.listToVector3(obj.scale);
                BasicInformation bInfo = loadedObj.AddComponent<BasicInformation>();
                bInfo.DownloadName = obj.nameDownload;
                bInfo.FromServer = false;
                bInfo.Id = obj.id;
            }

        }
    }
    private void loadBackground(BackgroundObject bo)
    {
        Instantiate(Resources.Load(@"Prefabs/MR/" + bo.nameBackground) as GameObject, ConvertTypeUtils.listToVector3(bo.position), ConvertTypeUtils.listToQuaternion(bo.rotation), GUI.transform);
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
        RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(json);
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
