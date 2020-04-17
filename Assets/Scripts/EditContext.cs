using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using System.IO;
using SimpleJSON;
using Newtonsoft.Json.Linq;

public class EditContext : MonoBehaviour
{
    List<GameObject> listGO = new List<GameObject>();
    void Start()
    {
        // json();
        //loadJson();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            json();
        if (Input.GetKeyDown(KeyCode.L))
            loadJson();
    }
    public void makeChild()
    {
        
    }
    public class JsonObject
    {
        public int id;
        public string name;
        public List<double> position;
        public List<double> scale;
        public List<double> rotation;
        public List<Dictionary<string, object>> components;

        public JsonObject(int id, string name, List<double> position, List<double> rotation, List<double> scale, List<Dictionary<string, object>> components)
        {
            this.id = id;
            this.name = name;
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
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
            dic.Add("rotation", rotation);
            dic.Add("scale", scale);
            dic.Add("components", components);
            return dic;
        }

        public void toObject(Dictionary<string, object> dict)
        {
            this.id = Convert.ToInt32(dict["id"]);
            this.name = Convert.ToString(dict["name"]);
            this.position = ((JArray)dict["position"]).Value<JArray>().ToObject<List<double>>();
            Debug.Log(this.position);
            this.rotation = ((JArray)dict["rotation"]).Value<JArray>().ToObject<List<double>>();
            this.scale = ((JArray)dict["scale"]).Value<JArray>().ToObject<List<double>>();
            // tmp.components = (List<Dictionary<string, object>>)dict["components"];
        }
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
            jsonObject = new JsonObject(0, child.name, new List<double>() { child.localPosition.x, child.localPosition.y, child.localPosition.z }, new List<double>() { child.position.x, child.position.y, child.position.z }, new List<double>() { child.localScale.x, child.localScale.y, child.localScale.y }, new List<Dictionary<string, object>>());
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
        GameObject gob = new GameObject();
        gob.name = "ListObject";
        listGO.Add(gob);
        var path = Path.Combine(Application.dataPath, "data.json");
        var jsonData = File.ReadAllText(path);
        Debug.Log(JsonConvert.DeserializeObject(jsonData));
        Dictionary<string, object> listScenes = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);
        Debug.Log(listScenes["Phrase"]);
        for (int i = 0; i < Convert.ToInt32(listScenes["nPhrase"]); i++) {
            List<Dictionary<string, object>> scenes = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(listScenes["Phrase"].ToString());
            Debug.Log(scenes[0]["Objects"]);
            List<Dictionary<string, object>> objects = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(scenes[0]["Objects"].ToString());
            foreach (Dictionary<string, object> element in objects)
            {
                JsonObject jsonObject = new JsonObject();
                jsonObject.toObject(element);
                GameObject go = new GameObject();
                go.name = jsonObject.name;
                go.transform.localPosition = new Vector3(Convert.ToSingle(jsonObject.position[0]), Convert.ToSingle(jsonObject.position[1]), Convert.ToSingle(jsonObject.position[2]));
                go.transform.rotation = new Quaternion((float)jsonObject.position[0], (float)jsonObject.position[1], (float)jsonObject.position[2], 0);
                go.transform.localScale = new Vector3((float)jsonObject.position[0], (float)jsonObject.position[1], (float)jsonObject.position[2]);
                go.transform.parent = gob.transform;
                go = Resources.Load("Prefab/" + go.name) as GameObject;
                var gobj = Instantiate(go, go.transform.localPosition, go.transform.rotation);
               
                listGO.Add(go);
            }
        }
    }
}