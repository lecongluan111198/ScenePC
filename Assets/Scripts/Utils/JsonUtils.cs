using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonUtils : MonoBehaviour
{
   //**Create and define**
    public class Object
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<double> position { get; set; }
        public List<double> rotation { get; set; }
        public List<double> scale { get; set; }
        //public List<object> components { get; set; }
        public Object (int id, string name, List<double>position, List<double> rotation, List<double> scale)
        {
            this.id = 0;
            this.name = name;
            this.position = position;
            this.rotation =rotation;
            this.scale = scale;
        }
    }

    public class Phrase
    {
        public int nObject { get; set; }
        public List<Object> Objects { get; set; }
    }

    public class RootObject
    {
        public int nPhrase { get; set; }
        public List<Phrase> Phrase { get; set; }
    }
    //**Save**
    //Convert vector3 and quaternion to list
    public List<double> vector3ToList(List<double> list, Vector3 vector3)
    {
        list = new List<double>()
            {
                vector3.x,
                vector3.y,
                vector3.z
            };
        return list;
    }
    public List<double> quaternionToList(List<double> list, Quaternion quaternion)
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
    public void addObjToList(List<Object> objects)
    {
        Object obje;
        List<double> position = new List<double>();
        List<double> rotation = new List<double>();
        List<double> scale = new List<double>();
        foreach (Transform child in transform)
        {
            position = vector3ToList(position, child.localPosition);
            rotation = quaternionToList(rotation, child.rotation);
            scale = vector3ToList(scale, child.localScale);
            obje = new Object(0, child.name, position, rotation, scale);
            objects.Add(obje);
        }
    }
    public void addDataToPhrase(List<Phrase> phrase, List<Object> objects)
    {
        Phrase phrs = new Phrase();
        phrs.nObject = transform.childCount;
        phrs.Objects = objects;
        phrase.Add(phrs);
    }
    public void addDataToRoot(RootObject rootObject, List<Phrase> phrase)
    {
        rootObject.nPhrase = 1;
        rootObject.Phrase = phrase;
    }
    
    public void saveJson()
    {
        //Create root
        RootObject rootObject = new RootObject();
        List<Phrase> phrase = new List<Phrase>();
        List<Object> objects = new List<Object>();
        //Get all child and add to object
        addObjToList(objects);
        //Get all object add to phrase
        addDataToPhrase(phrase, objects);
        //Get all phrase add to root
        addDataToRoot(rootObject, phrase);
        var path = Path.Combine(Application.dataPath, "data.json");
        File.WriteAllText(path, JsonConvert.SerializeObject(rootObject));
        string json = JsonConvert.SerializeObject(rootObject);
        Debug.Log(json);
    }
    //**Load**
    //Convert list to vector3 and quaternion 
    public Vector3 listToVector3(Vector3 vector3, List<double> list)
    {
        vector3.x = Convert.ToSingle(list[0]);
        vector3.y = Convert.ToSingle(list[1]);
        vector3.z = Convert.ToSingle(list[2]);
        return vector3;
    }
    public Quaternion listToQuaternion(Quaternion quaternion, List<double> list)
    {
        quaternion.x = (float)list[0];
        quaternion.y = (float)list[1];
        quaternion.z = (float)list[2];
        return quaternion;
    }
    //read data 
    public void readPhrase(RootObject rootObject, List<Phrase> listPhrase)
    {
        listPhrase = rootObject.Phrase;
        foreach (Phrase phrase in listPhrase)
        {
            List<Object> listObject = new List<Object>();
            readObject(phrase, listObject);
        }
    }
    public void readObject(Phrase phrase, List<Object> listObject)
    {
        GameObject parent = new GameObject();
        parent.name="listObject";
        listObject = phrase.Objects;
        foreach (Object obj in listObject)
        {
            loadGameObject(obj, parent);
        }
    }
    
    public void loadGameObject(Object obj, GameObject gob)
    {
        Vector3 position = new Vector3();
        Vector3 scale = new Vector3();
        Quaternion quaternion = new Quaternion();
        GameObject go = new GameObject();
        go.name = obj.name;
        go.transform.localPosition = listToVector3(position, obj.position);
        go.transform.localRotation = listToQuaternion(quaternion, obj.rotation);
        go.transform.localScale = listToVector3(scale, obj.position);
        go.transform.parent = gob.transform;
    }
    public void loadJson()
    {
        var path = Path.Combine(Application.dataPath, "data.json");
        var jsonDataRoot = File.ReadAllText(path);
        Debug.Log(jsonDataRoot);
        RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(jsonDataRoot);
        List<Phrase> listPhrase = new List<Phrase>();
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
