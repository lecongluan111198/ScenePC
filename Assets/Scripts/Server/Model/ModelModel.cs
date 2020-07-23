using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelModel : MonoBehaviour
{
    private static ModelModel _instance = null;
    private static GameObject go;
    public static ModelModel Instance
    {
        get
        {
            if (_instance == null)
            {
                go = GameObject.Find("Holder");
                if (go == null)
                {
                    go = new GameObject();
                }
                _instance = go.AddComponent<ModelModel>();
            }
            return _instance;
        }
    }

    public Model parseModel(string json)
    {
        try
        {
            Model courses = JsonConvert.DeserializeObject<Model>(json);
            return courses;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        return null;
    }

    public List<Model> parseListModel(string json)
    {
        try
        {
            List<Model> models = JsonConvert.DeserializeObject<List<Model>>(json);
            return models;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        return null;
    }

    public void loadAll(Action<List<Model>> callBack)
    {
        StartCoroutine(APIRequest.Instance.doGet(API.LOAD_ALL_MODLE, (data) =>
        {
            if (data.error >= 0)
            {
                callBack(parseListModel(data.result));
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
                callBack(null);
            }
        }));
    }
}
