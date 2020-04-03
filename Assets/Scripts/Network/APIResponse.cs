using Newtonsoft.Json;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APIResponse
{
    public int error { get; set; }
    public string message { get; set; }
    public JSONNode result { get; set; }
    private Dictionary<string, object> response = new Dictionary<string, object>();

    public static APIResponse textToReponse(string responseJson)
    {
        var jsonDB = JSON.Parse(responseJson);
        return new APIResponse
        {
            response = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseJson),
            error = jsonDB["error"].AsInt,
            message = jsonDB["message"],
            result = jsonDB["result"]
        };
    }

    public T getParam<T>(string name, T defautValue)
    {
        try
        {
            return (T)response[name];
        }catch(Exception e)
        {
            return defautValue;
        }
    }
}
