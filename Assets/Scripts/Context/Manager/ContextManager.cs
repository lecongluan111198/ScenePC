using Dummiesman;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ContextManager : MonoBehaviour
{
    [Header("CONTAINER")]
    public GameObject container;

    public GameObject GUI;
   //**Create and define**
    public class ContextObject
    {
        public int id { get; set; }
        public string nameObj { get; set; }
        public string nameDownload { get; set; }
        public List<double> position { get; set; }
        public List<double> rotation { get; set; }
        public List<double> scale { get; set; }
        public List<AbstractComponent> components { get; set; }

        public ContextObject (int id, string nameObj, string nameDownload , List<double>position, List<double> rotation, List<double> scale)
        {
            this.id = 0;
            this.nameObj = nameObj;
            this.nameDownload = nameDownload;
            this.position = position;
            this.rotation =rotation;
            this.scale = scale;
            this.components = new List<AbstractComponent>();
        }

        //public ContextObject(int id, string name, List<double> position, List<double> rotation, List<double> scale, List<AbstractComponent> components) : this(id, name, position, rotation, scale)
        //{
        //    this.components = components;
        //}
    }
    public class backgroundObject
    {
        public string nameBackground { get; set; }
        public List<double> position { get; set; }
        public List<double> rotation { get; set; }
        public List<double> scale { get; set; }
        public backgroundObject(string nameObj, List<double> position, List<double> rotation, List<double> scale)
        {
            this.nameBackground = nameObj;
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }
        public backgroundObject()
        {

        }
    }

    public class Phase
    {
        public int nObject { get; set; }
        public backgroundObject backgroundObject { get; set; }
        public List<ContextObject> Objects { get; set; }
    }

    public class RootObject
    {
        public int nPhrase { get; set; }
        public List<Phase> Phrase { get; set; }
    }
    //**Save**
    //Convert vector3 and quaternion to list
    private List<double> vector3ToList(List<double> list, Vector3 vector3)
    {
        list = new List<double>()
            {
                vector3.x,
                vector3.y,
                vector3.z
            };
        return list;
    }
    private List<double> quaternionToList(List<double> list, Quaternion quaternion)
    {
        list = new List<double>()
            {
                quaternion.x,
                quaternion.y,
                quaternion.z
            };
        return list;
    }
    //add object, data to list and save
    private void addObjToList(List<ContextObject> objects)
    {
        ContextObject obje;
        List<double> position = new List<double>();
        List<double> rotation = new List<double>();
        List<double> scale = new List<double>();
        foreach (Transform child in container.transform)
        {
            position = vector3ToList(position, child.localPosition);
            rotation = quaternionToList(rotation, child.rotation);
            scale = vector3ToList(scale, child.localScale);
            obje = new ContextObject(0, child.name, child.name, position, rotation, scale);
            objects.Add(obje);
        }
    }
    private void addBackgroundPhrase(backgroundObject bo)
    {
        List<double> position = new List<double>();
        List<double> rotation = new List<double>();
        List<double> scale = new List<double>();
        string nameBG = GUI.transform.GetChild(0).name;
        nameBG = nameBG.Substring(0, nameBG.Length - 7);
        Debug.Log("nameBG: " + nameBG);
        bo.nameBackground = nameBG;
        bo.position = vector3ToList(position, GUI.transform.GetChild(0).localPosition);
        bo.rotation = quaternionToList(rotation, GUI.transform.GetChild(0).localRotation);
        bo.scale = vector3ToList(scale, GUI.transform.GetChild(0).localScale);
    }
    private void addDataToPhrase(List<Phase> phrase, backgroundObject bo, List<ContextObject> objects)
    {
        Phase phrs = new Phase();
        phrs.nObject = transform.childCount;
        phrs.Objects = objects;
        phrs.backgroundObject = bo;
        phrase.Add(phrs);
    }
    private void addDataToRoot(RootObject rootObject, List<Phase> phrase)
    {
        rootObject.nPhrase = 1;
        rootObject.Phrase = phrase;
    }
    
    public void saveJson(/*Context context*/)
    {
        //Create root
        RootObject rootObject = new RootObject();
        List<Phase> phrase = new List<Phase>();
        List<ContextObject> objects = new List<ContextObject>();
        backgroundObject bo = new backgroundObject();
        //Get all child and add to object
        addObjToList(objects);
        //Get background
        addBackgroundPhrase(bo);
        //Get all object add to phrase
        addDataToPhrase(phrase,bo, objects);
        //Get all phrase add to root
        addDataToRoot(rootObject, phrase);
        //context.Content = JsonConvert.SerializeObject(rootObject);
        //ContextModel.Instance.updateContext(context, (data) => {
        //    //data = updated context

        //});
        var path = Path.Combine(Application.dataPath, "dataSave.json");
        File.WriteAllText(path, JsonConvert.SerializeObject(rootObject));
        string json = JsonConvert.SerializeObject(rootObject);
        Debug.Log(json);
    }
    //**Load**
    //Convert list to vector3 and quaternion 
    private Vector3 listToVector3(Vector3 vector3, List<double> list)
    {
        vector3.x = Convert.ToSingle(list[0]);
        vector3.y = Convert.ToSingle(list[1]);
        vector3.z = Convert.ToSingle(list[2]);
        return vector3;
    }
    private Quaternion listToQuaternion(Quaternion quaternion, List<double> list)
    {
        quaternion.x = (float)list[0];
        quaternion.y = (float)list[1];
        quaternion.z = (float)list[2];
        return quaternion;
    }
    //read data 
    private void readPhrase(RootObject rootObject, List<Phase> listPhrase)
    {
        listPhrase = rootObject.Phrase;
        foreach (Phase phrase in listPhrase)
        {
            backgroundObject bo = new backgroundObject();
            bo = phrase.backgroundObject;
            loadBackground(bo);
            List<ContextObject> listObject = new List<ContextObject>();
            readObject(phrase, listObject);
        }
    }
    private void readObject(Phase phrase, List<ContextObject> listObject)
    {
        listObject = phrase.Objects;
        foreach (ContextObject obj in listObject)
        {
            loadGameObject(obj);
        }
    }

    private void loadGameObject(ContextObject obj)
    {
        Vector3 position = new Vector3();
        Vector3 scale = new Vector3();
        Quaternion quaternion = new Quaternion();
        if (obj.nameDownload == null)
        {
            Debug.Log("source null " + obj.nameDownload);
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.localPosition = listToVector3(position, obj.position);
            go.transform.localScale = listToVector3(scale, obj.scale);
            go.transform.localRotation = listToQuaternion(quaternion, obj.rotation);
            go.transform.parent = container.transform;
        }
        else
        {
            Debug.Log("start download " + obj.nameDownload);
            
            FileModel.Instance._DownloadObject(obj.nameDownload + ".obj", obj.nameDownload + ".mtl", (file)=> {
                Debug.Log("file "+file[0]);
                if(file != null)
                {
                    GameObject loadedObj = new OBJLoader().Load(new MemoryStream(file[0]), new MemoryStream(file[1]));
                    loadedObj.transform.localPosition = listToVector3(position, obj.position);
                    loadedObj.name = obj.nameObj;
                    loadedObj.transform.localScale = listToVector3(scale, obj.scale);
                    loadedObj.transform.localRotation = listToQuaternion(quaternion, obj.rotation);
                    foreach (Transform child in loadedObj.transform)
                    {
                        _Prepare(child.gameObject);
                    }
                    loadedObj.transform.parent = container.transform;
                }
            } );
        }
    }
    private void loadBackground(backgroundObject bo)
    {
        Vector3 position = new Vector3();
        Quaternion quaternion = new Quaternion();
        string path = "Assets/Prefabs/UI/Room/" + bo.nameBackground + ".prefab";
        Debug.Log("path: " + path);
        GameObject myGameObject = (GameObject)Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)), listToVector3(position, bo.position), listToQuaternion(quaternion, bo.rotation), GUI.transform);
    }
    private void _Prepare(GameObject child)
    {
        Rigidbody rigid = child.AddComponent<Rigidbody>();
        rigid.mass = 1;
        rigid.useGravity = false;
        MeshCollider collider = child.AddComponent<MeshCollider>();
        collider.convex = true;
        collider.isTrigger = true;
    }

    public void loadJson(/*int contextId*/)
    {
        //ContextModel.Instance.loadContext(contextId, (data) =>
        //{
        //    Debug.Log(data);
        //    RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(data);
        //    List<Phase> listPhrase = new List<Phase>();
        //    readPhrase(rootObject, listPhrase);
        //});
        var path = Path.Combine(Application.dataPath, "dataLoad.json");
        var jsonDataRoot = File.ReadAllText(path);
        Debug.Log(jsonDataRoot);
        RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(jsonDataRoot);
        List<Phase> listPhrase = new List<Phase>();
        readPhrase(rootObject, listPhrase);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            saveJson();
        if (Input.GetKeyDown(KeyCode.L))
            loadJson();
    }
}
