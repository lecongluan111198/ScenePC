using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
public class JsonSave : MonoBehaviour
{
    // Start is called before the first frame update
    public int Id;
    public string Name;
    int m = 0;
    List<JSONObject> jsonObj = new List<JSONObject>();
    void Save(Transform tmp)
    {
        //info
        JSONObject playerJson = new JSONObject();
        playerJson.Add("Id", Id);
        playerJson.Add("Name", Name);
        //Position
        JSONArray position = new JSONArray();
        position.Add(tmp.position.x);
        position.Add(tmp.position.y);
        position.Add(tmp.position.z);
        playerJson.Add("Position", position);
        //Rotation
        JSONArray rotation = new JSONArray();
        rotation.Add(tmp.rotation.x);
        rotation.Add(tmp.rotation.y);
        rotation.Add(tmp.rotation.z);
        playerJson.Add("Rotation", rotation);
        //Scale
        JSONArray scale = new JSONArray();
        scale.Add(tmp.localScale.x);
        scale.Add(tmp.localScale.y);
        scale.Add(tmp.localScale.z);
        playerJson.Add("Scale", scale);
        //Save json to laptop
        //string path = Application.persistentDataPath + "/data"+m+".json";
       // Debug.Log(path);
        jsonObj.Add(playerJson);
        //File.WriteAllText(path, playerJson.ToString());
        //m++;
    }
    void Load()
    {
        //Read from file and load
        string path = Application.persistentDataPath + "/data.json";
        string jsonString = File.ReadAllText(path);
        JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);
        //Set value
        Name = playerJson["Name"];
        Id = playerJson["Id"];
        //Position
        transform.position = new Vector3(
            playerJson["Position"].AsArray[0],
            playerJson["Position"].AsArray[1],
            playerJson["Position"].AsArray[2]
            );
    }
    void Start()
    {
        WithForeachLoop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save(transform);
            
            foreach (JSONObject js in jsonObj)
            {
                string path = Application.persistentDataPath + "/data" + m + ".json";
                File.WriteAllText(path, js.ToString());
                Debug.Log(path);
                m++;
            }
        }
        if (Input.GetKeyDown(KeyCode.L)) Load();
    }
    void WithForeachLoop()
    {
        foreach (Transform child in transform)
        {
            print("Object con: " + child);
            Save(child);
        }
    }
}
