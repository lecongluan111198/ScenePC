﻿using System.Collections;
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
    //public void SaveScene() {
    //    var mno = new Scenes {
    //        scenes = new Dictionary<string, Scene>
    //        {
    //            {"scene1",listScene }
    //        }
    //    };
    //}
    public void writeToJson() { 
        var setting = new JsonSerializerSettings();
        setting.Formatting = Formatting.Indented;
        setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

        // write
        // var accountsFromCode = new List<Account> { accountJames, accountOnion };
        // var accountsFromCode = new List<List<List<List<Obj>>>> {data};
        var scenesFromCode = new List<List<Obj>> { listObj };
        var json = JsonConvert.SerializeObject(scenesFromCode, setting);
        var path = Path.Combine(Application.dataPath, "hiiii.json");
        Debug.Log(path);
        File.WriteAllText(path, json);
    }
    public void readFromJson()
    {
        var setting = new JsonSerializerSettings();
        setting.Formatting = Formatting.Indented;
        setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        var path = Path.Combine(Application.dataPath, "hiiii.json");
        var fileContent = File.ReadAllText(path);
        var objFromFile = JsonConvert.DeserializeObject<List<List<Obj>>>(fileContent);
        var reSerializedJson = JsonConvert.SerializeObject(objFromFile, setting);
        print(reSerializedJson);
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
        WithForeachLoop();
    }
    void WithForeachLoop()
    {
        foreach (Transform child in transform)
        {
            print("Object con: " + child);
            addListObj(child);
        }
    }
}

