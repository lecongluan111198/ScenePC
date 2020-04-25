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
    public string result { get; set; }
    private Dictionary<string, object> response = new Dictionary<string, object>();

    public static APIResponse textToReponse(string responseJson)
    {
        var jsonDB = JSON.Parse(responseJson);
        Dictionary<string, object> mapData = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseJson);
        object resultObj;
        mapData.TryGetValue("result", out resultObj);
        return new APIResponse
        {
            response = mapData,
            error = jsonDB["error"].AsInt,
            message = jsonDB["message"],
            result = Convert.ToString(resultObj)
        };
    }

    //public T getParam<T>(string name, T defaultValue)
    //{
    //    try
    //    {
    //        return (T)response[name];
    //    }catch(Exception e)
    //    {
    //        Debug.Log(e.Message);
    //        return defaultValue;
    //    }
    //}

    public int getIntParam(string name, int defaultValue)
    {
        try
        {
            return Convert.ToInt32(response[name]);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return defaultValue;
        }
    }

    public double getDoubleParam(string name, double defaultValue)
    {
        try
        {
            return Convert.ToDouble(response[name]);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return defaultValue;
        }
    }

    public string getStringParam(string name, string defaultValue)
    {
        try
        {
            return Convert.ToString(response[name]);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return defaultValue;
        }
    }
}
