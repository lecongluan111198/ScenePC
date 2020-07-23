using Newtonsoft.Json;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseModel : MonoBehaviour
{
    private static CourseModel _instance = null;
    private static GameObject go;
    public static CourseModel Instance
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
                _instance = go.AddComponent<CourseModel>();
            }
            return _instance;
        }
    }

    //public static Course parseCourse(JSONNode node)
    //{
    //    try
    //    {
    //        Course course = new Course();
    //        course.Id = node["id"];
    //        course.Name = node["name"];
    //        course.Status = node["status"];
    //        course.TeacherId = node["teacherId"];
    //        JSONArray contextNodes = node["contexts"] as JSONArray;
    //        List<Context> contexts = new List<Context>();
    //        foreach (JSONNode contextNode in contextNodes)
    //        {
    //            Context con = ContextModel.parseContext(contextNode);
    //            if (con != null)
    //            {
    //                contexts.Add(con);
    //            }
    //        }
    //        return course;
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.Log(e);
    //        return null;
    //    }

    //}

    private List<Course> parseListCourse(string json)
    {
        try
        {
            List<Course> courses = JsonConvert.DeserializeObject<List<Course>>(json);
            return courses;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        return null;
    }


    public Course parseCourse(string json)
    {
        try
        {
            Course courses = JsonConvert.DeserializeObject<Course>(json);
            return courses;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        return null;
    }

    public void loadCourse(int courseId, Action<Course> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.LOAD_COURSE);
        string uri = reqBuilder.AddParam("courseId", courseId).build();
        StartCoroutine(APIRequest.Instance.doGet(uri, (data) =>
        {
            if (data.error >= 0)
            {
                Course course = parseCourse(data.result);
                callBack(course);
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
                callBack(null);
            }
        }));
    }

    public void loadOwnCourse(int length, int offset, Action<List<Course>> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.LOAD_OWN_COURSE);
        string uri = reqBuilder
            .AddParam("length", length)
            .AddParam("offset", offset)
            .build();
        StartCoroutine(APIRequest.Instance.doGet(uri, (data) =>
        {
            if (data.error >= 0)
            {
                List<Course> courses = parseListCourse(data.result);
                callBack(courses);
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
                callBack(null);
            }
        }));
    }

    public void loadAccessCourse(int length, int offset, Action<List<Course>> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.LOAD_ALL_ACCESS_COURSE);
        string uri = reqBuilder
            .AddParam("length", length)
            .AddParam("offset", offset)
            .build();
        StartCoroutine(APIRequest.Instance.doGet(uri, (data) =>
        {
            if (data.error >= 0)
            {
                List<Course> courses = parseListCourse(data.result);
                callBack(courses);
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
                callBack(null);
            }
        }));
    }

    public void loadAccessCourse(int courseId, Action<Course> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.LOAD_ACCESS_COURSE);
        string uri = reqBuilder.AddParam("courseId", courseId).build();
        StartCoroutine(APIRequest.Instance.doGet(uri, (data) =>
        {
            if (data.error >= 0)
            {
                Course courses = parseCourse(data.result);
                callBack(courses);
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
                callBack(null);
            }
        }));
    }

    public void createCourse(Course course, Action<KeyValuePair<bool, string>> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.CREATE_COURSE);
        string uri = reqBuilder
            .AddParam("teacherId", course.TeacherId)
            .AddParam("name", course.Name)
            .AddParam("status", course.Status)
            .AddParam("description", course.Status)
            .AddParam("contexts", course.Contexts)
            .AddParam("type", course.Type)
            .AddParam("avatarId", course.AvatarId)
            .build();
        StartCoroutine(APIRequest.Instance.doPost(uri, "", (data) =>
        {
            if (data == null)
            {
                Debug.Log(uri);
                callBack(new KeyValuePair<bool, string>(false, "Data is empty"));
            }
            else
            {
                if (data.error >= 0)
                {
                    Course newCourse = parseCourse(data.result);
                    if (newCourse != null)
                    {
                        callBack(new KeyValuePair<bool, string>(true, "Create successfully"));
                    }
                    else
                    {
                        callBack(new KeyValuePair<bool, string>(false, "Create failly"));
                    }
                }
                else
                {
                    string mess = data.error + ": " + data.message;
                    Debug.Log(mess);
                    callBack(new KeyValuePair<bool, string>(false, data.message));
                }
            }
        }));
    }

    public void updateCourse(Course course, Action<KeyValuePair<bool, string>> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.UPDATE_COURSE);
        string uri = reqBuilder.AddParam("id", course.Id)
            .AddParam("teacherId", course.TeacherId)
            .AddParam("name", course.Name)
            .AddParam("status", course.Status)
            .AddParam("description", course.Status)
            .AddParam("contexts", course.Contexts)
            .AddParam("type", course.Type)
            .AddParam("avatarId", course.AvatarId)
            .build();
        StartCoroutine(APIRequest.Instance.doPost(uri, "", (data) =>
         {
             if (data == null)
             {
                 Debug.Log(uri);
                 callBack(new KeyValuePair<bool, string>(false, "Data is empty"));
             }
             else
             {
                 if (data.error >= 0)
                 {
                     Course newCourse = parseCourse(data.result);
                     if (newCourse != null)
                     {
                         callBack(new KeyValuePair<bool, string>(true, "Update successfully"));
                     }
                     else
                     {
                         callBack(new KeyValuePair<bool, string>(false, "Update failly"));
                     }
                 }
                 else
                 {
                     Debug.Log(data.error + ": " + data.message);
                     callBack(new KeyValuePair<bool, string>(false, data.message));
                 }
             }
         }));
    }

    public void deleteCourse(int courseId, Action<Course> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.DELETE_COURSE);
        string uri = reqBuilder.AddParam("courseId", courseId)
            .build();
        StartCoroutine(APIRequest.Instance.doGet(uri, (data) =>
        {
            if (data.error >= 0)
            {
                Course courses = parseCourse(data.result);
                callBack(courses);
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
                callBack(null);
            }
        }));
    }
}
