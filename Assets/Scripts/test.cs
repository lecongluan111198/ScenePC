using Newtonsoft.Json;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string str = "{\"uid\":1,\"image\":null,\"role\":\"ADMIN\",\"error\":0,\"message\":\"Success\",\"sessKey\":\"eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiIxIiwiZXhwIjoxNTg5NjM5ODc0LCJpYXQiOjE1ODcwNDc4NzR9.sLhyaOfB07CV1m5FCxZ9LxeaDNjn7ZeukHGhtvWS-ROPXfbtlcCoCWkACuTK6w1yDigTcbhRJ30zbGdBvhME2g\",\"username\":\"LuanLee\"}";
        JSONNode data = JSON.Parse(str);
        Debug.Log(data["role"]);
        Dictionary<string, object>  mapData = JsonConvert.DeserializeObject<Dictionary<string, object>>(str);
        Debug.Log(mapData["role"]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
