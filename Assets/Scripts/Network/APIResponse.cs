using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APIResponse
{
    public int error { get; set; }
    public string message { get; set; }
    public JSONNode result { get; set; }

    public static APIResponse textToReponse(string responseJson)
    {
        var jsonDB = JSON.Parse(responseJson);
        return new APIResponse
        {
            error = jsonDB["error"].AsInt,
            message = jsonDB["message"],
            result = jsonDB["result"]
        };
    }
}
