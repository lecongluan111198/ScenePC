using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonUtils : MonoBehaviour
{
   
    public class Object
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<double> position { get; set; }
        public List<double> rotation { get; set; }
        public List<double> scale { get; set; }
        //public List<object> components { get; set; }
        public List<double> vector3ToList(List<double> list,Vector3 vector3)
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
        public Object(Transform tsf)
        {
            id = 0;
            name = tsf.name;
            position = vector3ToList(position, tsf.localPosition);
            rotation = quaternionToList(rotation, tsf.rotation);
            scale = vector3ToList(scale, tsf.localScale);
        }
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
    public void addObjToList(List<Object> objects)
    {
        Object obje, a;
        foreach(Transform child in transform)
        {
            obje = new Object(child);
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
        RootObject rootObject = new RootObject();
        List<Phrase> phrase = new List<Phrase>();
        List<Object> objects = new List<Object>();
        addObjToList(objects);
        addDataToPhrase(phrase, objects);
        addDataToRoot(rootObject, phrase);
        var path = Path.Combine(Application.dataPath, "data.json");
        File.WriteAllText(path, JsonConvert.SerializeObject(rootObject));
        string json = JsonConvert.SerializeObject(rootObject);
        Debug.Log(json);
    }
    public void loadJson()
    {
        
        //List<Phrase> phrase = new List<Phrase>();
        //List<Object> objects = new List<Object>();
        var path = Path.Combine(Application.dataPath, "data.json");
        var jsonData = File.ReadAllText(path);
        Debug.Log(jsonData);
        //RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(jsonData);
        //Debug.Log(rootObject.nPhrase);
        //Debug.Log(rootObject.Phrase);
    }
    // Start is called before the first frame update
    void Start()
    {
        //saveJson();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            saveJson();
        if (Input.GetKeyDown(KeyCode.L))
            loadJson();
    }
}
