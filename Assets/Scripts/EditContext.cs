using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using System.IO;
using SimpleJSON;

public class EditContext : MonoBehaviour
{
    public int Id;
    public string name = "abc";
    List<Obj> listObj = new List<Obj>();
    Scene listScene = new Scene();
    public void addListObj(Transform transform)
    {   
        var objtmp = new Obj();
        objtmp.ID = Id;
        objtmp.Name = transform.name;
        objtmp.Position = transform.position;
        objtmp.Scale = transform.localScale;
        listObj.Add(objtmp);
        Debug.Log("listObj: " + listObj);

        //var xyz = new Scene {
        //    scene = new Dictionary<string, Obj>
        //    {
        //        {"Obj", objtmp }
        //    }
        //};

        //listScene.scene = new Dictionary<string, Obj>
        //{
        //    {"Obj", objtmp }
        //};
    }
    public void SaveScene()
    {
        var mno = new Scenes
        {
            scenes = new Dictionary<string, Scene>
            {
                {"scene1",listScene }
            }
        };
    }
    public void writeToJson()
    {
        //var setting = new JsonSerializerSettings();
        //setting.Formatting = Formatting.Indented;
        //setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

        //// write
        //// var accountsFromCode = new List<Account> { accountJames, accountOnion };
        //// var accountsFromCode = new List<List<List<List<Obj>>>> {data};
        //var scenesFromCode = new List<List<Obj>> { listObj };
        //var json = JsonConvert.SerializeObject(scenesFromCode, setting);
        //var path = Path.Combine(Application.dataPath, "hiiii.json");
        //Debug.Log(path);
        //File.WriteAllText(path, json);
    }
    public void readFromJson()
    {
        //var setting = new JsonSerializerSettings();
        //setting.Formatting = Formatting.Indented;
        //setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        //var path = Path.Combine(Application.dataPath, "hiiii.json");
        //var fileContent = File.ReadAllText(path);
        //var objFromFile = JsonConvert.DeserializeObject<List<List<Obj>>>(fileContent);
        //var reSerializedJson = JsonConvert.SerializeObject(objFromFile, setting);
        //print(reSerializedJson);
    }


    public class Obj
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }
        public IList<string> Component { get; set; }
    }
    public class Scene
    {
        public Dictionary<string, Obj> scene { get; set; }
    }
    public class Scenes
    {
        public Dictionary<string, Scene> scenes { get; set; }
    }
    //public class Data
    //{
    //    int phrase;
    //    public IList<Scenes> data { get; set; }
    //}
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            writeToJson();
        if (Input.GetKeyDown(KeyCode.L))
            readFromJson();
    }

    void Start()
    {
        json();
        //WithForeachLoop();
    }
    void WithForeachLoop()
    {
        foreach (Transform child in transform)
        {
            print("Object con: " + child);
            addListObj(child);
        }
    }

    public class JsonObject
    {
        int id;
        string name;
        float[] position;
        float[] rotation;
        List<Dictionary<string, object>> components;

        public JsonObject(int id, string name, float[] position, float[] rotation, List<Dictionary<string, object>> components)
        {
            this.id = id;
            this.name = name;
            this.position = position;
            this.rotation = rotation;
            this.components = components;
        }

        public Dictionary<string, object> toJson()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("id", id);
            dic.Add("name", name);
            dic.Add("position", position);
            dic.Add("rotation", rotation);
            dic.Add("components", components);
            return dic;
        }
    }

    public void json()
    {
        Dictionary<string, object> jsonData = new Dictionary<string, object>();
        List<Dictionary<string, object>> listScenes = new List<Dictionary<string, object>>();
        jsonData.Add("phase", 5);
        jsonData.Add("Scenes", listScenes);
        Dictionary<string, object> scene = new Dictionary<string, object>();
        listScenes.Add(scene);

        scene.Add("nObject", 2);

        List<Dictionary<string, object>> objects = new List<Dictionary<string, object>>();
        JsonObject jsonObject;
        for (int i = 0; i < 2; i++)
        {
            jsonObject = new JsonObject(i, "tham", new float[] { 1, 2, 3 }, new float[] { 1, 2, 3 }, new List<Dictionary<string, object>>());
            Debug.Log(JsonConvert.SerializeObject(jsonObject));
            objects.Add(jsonObject.toJson());
        }
        scene.Add("Objects", objects);

        Debug.Log(JsonConvert.SerializeObject(jsonData));

    }
}

