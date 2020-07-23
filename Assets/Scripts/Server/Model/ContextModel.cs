using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using Newtonsoft.Json;

public class ContextModel : MonoBehaviour
{
    private static ContextModel _instance = null;
    private static GameObject go;
    public static ContextModel Instance
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
                _instance = go.AddComponent<ContextModel>();
            }
            return _instance;
        }
    }

    //public static Context parseContext(JSONNode node)
    //{
    //    try
    //    {
    //        Context context = new Context();
    //        context.Id = node["id"];
    //        context.Name = node["name"];
    //        context.TeacherId = node["teacherId"];
    //        context.Description = node["description"];
    //        context.Content = node["content"];

    //        return context;
    //    }catch(Exception e)
    //    {
    //        Debug.Log(e);
    //    }
    //    return null;
    //}

    public static Context parseContext(string json)
    {
        try
        {
            Context context = JsonConvert.DeserializeObject<Context>(json);
            return context;
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        return null;
    }

    private List<Context> parseListContext(string json)
    {
        try
        {
            List<Context> courses = JsonConvert.DeserializeObject<List<Context>>(json);
            return courses;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        return null;
    }

    public void loadContext(int contextId, Action<Context> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.LOAD_CONTEXT);
        string uri = reqBuilder.AddParam("contextId", contextId).build();
        StartCoroutine(APIRequest.Instance.doGet(uri, (data) =>
        {
            if (data.error >= 0)
            {
                callBack(parseContext(data.result));
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
                callBack(null);
            }
        }));
    }

    public void loadOwnContext(int length, int offset, Action<List<Context>> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.LOAD_OWN_CONTEXT);
        string uri = reqBuilder.build();
        StartCoroutine(APIRequest.Instance.doGet(uri, (data) =>
        {
            if (data.error >= 0)
            {
                List<Context> contexts = JsonConvert.DeserializeObject<List<Context>>(data.result);
                callBack(contexts);
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
                callBack(null);
            }
        }));
    }

    public void loadCourseContext(int courseId, Action<string> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.LOAD_COURSE_CONTEXT);
        string uri = reqBuilder.AddParam("courseId", courseId).build();
        StartCoroutine(APIRequest.Instance.doGet(uri, (data) =>
        {
            if (data.error >= 0)
            {
                callBack(data.result);
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
                callBack(null);
            }
        }));
    }

    public void createContext(Context context, Action<Context> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.LOAD_COURSE_CONTEXT);
        string uri = reqBuilder.AddParam("teacherId", context.TeacherId)
                .AddParam("name", context.Name)
                .AddParam("description", context.Description)
                .AddParam("content", context.Content)
                .build();
        StartCoroutine(APIRequest.Instance.doPost(uri, "", (data) =>
        {
            if (data.error >= 0)
            {
                callBack(parseContext(data.result));
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
                callBack(null);
            }
        }));
    }

    public void updateContext(Context context, Action<Context> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.UPDATE_CONTEXT);
        string uri = reqBuilder.AddParam("id", context.Id)
                .AddParam("name", context.Name)
                .AddParam("description", context.Description)
                .AddParam("content", context.Content)
                .AddParam("avatarId", context.AvatarId)
                .AddParam("createTime", context.CreateTime)
                .AddParam("teacherId", context.TeacherId)
                .build();
        StartCoroutine(APIRequest.Instance.doPost(API.UPDATE_CONTEXT, reqBuilder.toMap(), (data) =>
        {
            if(data != null)
            {
                if (data.error >= 0)
                {
                    Context updatedContext = parseContext(data.result);
                    callBack(updatedContext);
                }
                else
                {
                    Debug.Log(data.error + ": " + data.message);
                    callBack(null);
                }
            }
            else
            {
                Debug.Log("data is null");
                callBack(null);
            }
        }));
    }

    public void deleteContext(int contextId, Action<bool> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.LOAD_COURSE_CONTEXT);
        string uri = reqBuilder.AddParam("contextId", contextId)
                .build();
        StartCoroutine(APIRequest.Instance.doPost(uri, "", (data) =>
        {
            if (data.error >= 0)
            {
                callBack(true);
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
                callBack(false);
            }
        }));
    }
}
