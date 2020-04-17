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
    void Start()
    {
        //json();
        loadJson();
    }

    public class JsonObject
    {
        int id;
        string name;
        float[] position;
        float[] scale;
        List<Dictionary<string, object>> components;
        
        public float[] vector3ToFloat(Vector3 vt)
        {
            float[] tmp = new float[3];
            tmp[0] = vt.x;
            tmp[1] = vt.y;
            tmp[2] = vt.z;
            return tmp;
        }

        public JsonObject(int id, string name, Vector3 position, Vector3 scale, List<Dictionary<string, object>> components)
        {
            this.id = id;
            this.name = name;
            this.position = vector3ToFloat(position);
            this.scale = vector3ToFloat(scale);
            this.components = components;
        }

        public JsonObject()
        {

        }

        public Dictionary<string, object> toJson()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("id", id);
            dic.Add("name", name);
            dic.Add("position", position);
            dic.Add("scale", scale);
            dic.Add("components", components);
            return dic;
        }
        //Hàm gán giá trị lại cho obj
    }

    public void json()
    {
        Dictionary<string, object> jsonData = new Dictionary<string, object>();
        List<Dictionary<string, object>> listScenes = new List<Dictionary<string, object>>();
        Dictionary<string, object> scene = new Dictionary<string, object>();
        jsonData.Add("nPhrase", 1);
        jsonData.Add("Phrase", listScenes);
        listScenes.Add(scene);
        scene.Add("nObject", transform.childCount);

        List<Dictionary<string, object>> objects = new List<Dictionary<string, object>>();
        JsonObject jsonObject;
      
        foreach (Transform child in transform)
        {
            print("Object con: " + child);
            jsonObject = new JsonObject(0, child.name, child.localPosition, child.localScale, new List<Dictionary<string, object>>());
            Debug.Log(JsonConvert.SerializeObject(jsonObject));
            objects.Add(jsonObject.toJson());
        }
        scene.Add("Objects", objects);
        Debug.Log(JsonConvert.SerializeObject(jsonData));
        var path = Path.Combine(Application.dataPath, "data.json");
        Debug.Log(path);
        File.WriteAllText(path, JsonConvert.SerializeObject(jsonData));
    }
    public void loadJson()
    {
        var path = Path.Combine(Application.dataPath, "a.json");
        var jsonData = File.ReadAllText(path);
        Debug.Log(JsonConvert.DeserializeObject(jsonData));
        Dictionary<string, object> listScenes = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);
        Debug.Log(listScenes["Phrase"]);
        //chạy vòng for (i=0;i<nPhrase;i++)
        List<Dictionary<string, object>> scenes = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(listScenes["Phrase"].ToString());
        Debug.Log(scenes[0]["Objects"]);
        Debug.Log(scenes[1]["Objects"]);
        //Chạy vòng for(j=0;j<nObject;i++)
        Dictionary<string, object> objects = JsonConvert.DeserializeObject<Dictionary<string, object>>(scenes[0]["Objects"].ToString());
        //load đối tượng và gán giá trị
    }
}

