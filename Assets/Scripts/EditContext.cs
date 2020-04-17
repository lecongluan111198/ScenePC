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
        json();
        //loadJson();
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
    }

    public void json()
    {
        Dictionary<string, object> jsonData = new Dictionary<string, object>();
        List<Dictionary<string, object>> listScenes = new List<Dictionary<string, object>>();
        jsonData.Add("nPhrase", 1);
        jsonData.Add("Phrase", listScenes);
        Dictionary<string, object> scene = new Dictionary<string, object>();
        listScenes.Add(scene);

        scene.Add("nObject", transform.childCount);

        List<Dictionary<string, object>> objects = new List<Dictionary<string, object>>();
        JsonObject jsonObject;
      
        foreach (Transform child in transform)
        {
            print("Object con: " + child);
            jsonObject = new JsonObject(0, child.name, child.localPosition, child.localScale, new List<Dictionary<string, object>>());
            //Debug.Log(JsonConvert.SerializeObject(jsonObject));
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
        var path = Path.Combine(Application.dataPath, "data.json");
        var fileContent = File.ReadAllText(path);
        Debug.Log(JsonConvert.DeserializeObject(fileContent));
        Dictionary<string, object> jsonData = JsonConvert.DeserializeObject<Dictionary<string, object>>(fileContent);
        Debug.Log(jsonData["nPhrase"]);
        Debug.Log(jsonData["Phrase"]);

        List<Dictionary<string, object>> listScenes = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>((jsonData["Phrase"]).ToString());
        Debug.Log(listScenes[0]);
        Debug.Log(string.Join(",",listScenes));
        //Debug.Log(JsonConvert.DeserializeObject<Dictionary<string, object>>((listScenes.ToArray()).ToString()));
        //Debug.Log(string.Join(",", listScenes.ToArray()));
        //new List<Dictionary<string, object>>();
        //jsonData.Add("nPhrase", 1);
        //jsonData.Add("Phrase", listScenes);
        //Debug.Log(JsonConvert.DeserializeObject < Dictionary<string, object>>(string.Join(",", listScenes)));
        //Dictionary<string, object> scene = JsonConvert.DeserializeObject<Dictionary<string, object>>((listScenes[0]).ToString());
        //listScenes.Add(scene);
        //Debug.Log(scene["nObject"]);
        //scene.Add("nObject", transform.childCount);

        //List<Dictionary<string, object>> objects = new List<Dictionary<string, object>>();
        //JsonObject jsonObject;

        //foreach (Transform child in transform)
        //{
        //    print("Object con: " + child);
        //    jsonObject = new JsonObject(0, child.name, child.localPosition, child.localScale, new List<Dictionary<string, object>>());
        //    objects.Add(jsonObject.toJson());
        //}
        //scene.Add("Objects", objects);
    }
}

