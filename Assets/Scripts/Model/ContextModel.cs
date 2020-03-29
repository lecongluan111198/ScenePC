using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

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
                _instance = new ContextModel();
            }
            return _instance;
        }
    }

    public void loadContext(int contextId, Action<JSONNode> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.LOAD_CONTEXT);
        string uri = reqBuilder.AddParam("contextId", contextId).build();
        StartCoroutine(APIRequest.Instance.doGet(uri, (data) =>
        {
            if (data.error >= 0)
            {
                callBack(data.result);
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
            }
        }));
    }

    public void loadOwnContext(Action<JSONNode> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.LOAD_OWN_CONTEXT);
        string uri = reqBuilder.build();
        StartCoroutine(APIRequest.Instance.doGet(uri, (data) =>
        {
            if (data.error >= 0)
            {
                callBack(data.result);
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
            }
        }));
    }

    public void loadCourseContext(int courseId, Action<JSONNode> callBack)
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
            }
        }));
    }

    public void createContext(Context context, Action<JSONNode> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.LOAD_COURSE_CONTEXT);
        string uri = reqBuilder.AddParam("teacherId", context.TeacherId)
                .AddParam("name", context.Name)
                .AddParam("description", context.Description)
                .AddParam("content", context.Content)
                .build();
        StartCoroutine(APIRequest.Instance.doGet(uri, (data) =>
        {
            if (data.error >= 0)
            {
                callBack(data.result);
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
            }
        }));
    }

    public void updateContext(Context context, Action<JSONNode> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.LOAD_COURSE_CONTEXT);
        string uri = reqBuilder.AddParam("id", context.Id)
                .AddParam("teacherId", context.TeacherId)
                .AddParam("name", context.Name)
                .AddParam("description", context.Description)
                .AddParam("content", context.Content)
                .build();
        StartCoroutine(APIRequest.Instance.doGet(uri, (data) =>
        {
            if (data.error >= 0)
            {
                callBack(data.result);
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
            }
        }));
    }

    public void deleteContext(int contextId, Action<JSONNode> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.LOAD_COURSE_CONTEXT);
        string uri = reqBuilder.AddParam("contextId", contextId)
                .build();
        StartCoroutine(APIRequest.Instance.doGet(uri, (data) =>
        {
            if (data.error >= 0)
            {
                callBack(data.result);
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
            }
        }));
    }
}
