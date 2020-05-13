﻿using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ContextManager : MonoBehaviour
{
    [Header("CONTAINER")]
    public GameObject container;

   //**Create and define**
    public class ContextObject
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<double> position { get; set; }
        public List<double> rotation { get; set; }
        public List<double> scale { get; set; }
        public List<AbstractComponent> components { get; set; }

        public ContextObject (int id, string name, List<double>position, List<double> rotation, List<double> scale)
        {
            this.id = 0;
            this.name = name;
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

    public class Phase
    {
        public int nObject { get; set; }
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
            obje = new ContextObject(0, child.name, position, rotation, scale);
            objects.Add(obje);
        }
    }
    private void addDataToPhrase(List<Phase> phrase, List<ContextObject> objects)
    {
        Phase phrs = new Phase();
        phrs.nObject = transform.childCount;
        phrs.Objects = objects;
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
        //Get all child and add to object
        addObjToList(objects);
        //Get all object add to phrase
        addDataToPhrase(phrase, objects);
        //Get all phrase add to root
        addDataToRoot(rootObject, phrase);
        //context.Content = JsonConvert.SerializeObject(rootObject);
        //ContextModel.Instance.updateContext(context, (data) => {
        //    //data = updated context

        //});
        var path = Path.Combine(Application.dataPath, "datanew.json");
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
            List<ContextObject> listObject = new List<ContextObject>();
            readObject(phrase, listObject);
        }
    }
    private void readObject(Phase phrase, List<ContextObject> listObject)
    {
        //GameObject parent = new GameObject();
        //parent.name="listObject";
        listObject = phrase.Objects;
        foreach (ContextObject obj in listObject)
        {
            //loadGameObject(obj, parent);
            GameObject go = loadGameObject(obj);
            go.transform.SetParent(container.transform);
        }
    }

    private GameObject loadGameObject(ContextObject obj)
    {
        Vector3 position = new Vector3();
        Vector3 scale = new Vector3();
        Quaternion quaternion = new Quaternion();
        string source = checkModel(obj);
        GameObject go = new GameObject();
        if (source == null)
        {
            go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
        else
        {
            
            go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        }
        go.name = obj.name;
        go.transform.localPosition = listToVector3(position, obj.position);
        go.transform.localRotation = listToQuaternion(quaternion, obj.rotation);
        go.transform.localScale = listToVector3(scale, obj.position);
        //go.transform.parent = container.transform;
        return go;
    }
    private string checkModel(ContextObject obj)
    {
        ModelManager mm = new ModelManager();
        ModelManager.managerSource ms = new ModelManager.managerSource();
        mm.loadObjectFromSource(obj.id, ms);
        if (ms == null)
        {
            Debug.Log("Can not find source object");
        }
        else
        {
            return ms.name;
        }
        return null;
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
        var path = Path.Combine(Application.dataPath, "datanew.json");
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
