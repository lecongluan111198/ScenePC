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
    void Save()
    {
        //info
        JSONObject playerJson = new JSONObject();
        playerJson.Add("Id", Id);
        playerJson.Add("Name", Name);
        //Position
        JSONArray position = new JSONArray();
        position.Add(transform.position.x);
        position.Add(transform.position.y);
        position.Add(transform.position.z);
        playerJson.Add("Position", position);
        //Rotation
        JSONArray rotation = new JSONArray();
        rotation.Add(transform.rotation.x);
        rotation.Add(transform.rotation.y);
        rotation.Add(transform.rotation.z);
        playerJson.Add("Rotation", rotation);
        //Scale
        JSONArray scale = new JSONArray();
        scale.Add(transform.localScale.x);
        scale.Add(transform.localScale.y);
        scale.Add(transform.localScale.z);
        playerJson.Add("Scale", scale);
        //Save json to laptop
        string path = Application.persistentDataPath + "/data.json";
        Debug.Log(path);
        File.WriteAllText(path, playerJson.ToString());
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) Save();
        if (Input.GetKeyDown(KeyCode.L)) Load();
    }
}
