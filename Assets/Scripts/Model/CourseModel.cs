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
                _instance = new CourseModel();
            }
            return _instance;
        }
    }

    public static Course parseCourse(JSONNode node)
    {
        try
        {
            Course course = new Course();
            course.Id = node["id"];
            course.Name = node["name"];
            course.Status = node["status"];
            course.TeacherId = node["teacherId"];
            JSONArray contextNodes = node["contexts"] as JSONArray;
            List<Context> contexts = new List<Context>();
            foreach (JSONNode contextNode in contextNodes)
            {
                Context con = ContextModel.parseContext(contextNode);
                if (con != null)
                {
                    contexts.Add(con);
                }
            }
            return course;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return null;
        }

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

    public void loadOwnCourse(Action<List<Course>> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.LOAD_OWN_COURSE);
        string uri = reqBuilder.build();
        StartCoroutine(APIRequest.Instance.doGet(uri, (data) =>
        {
            if (data.error >= 0)
            {
                List<Course> courses = new List<Course>();
                JSONArray nodes = data.result as JSONArray;
                foreach (JSONNode node in nodes)
                {
                    Course course = parseCourse(data.result);
                    if (course != null)
                    {
                        courses.Add(course);
                    }
                }
                if (courses.Count != 0)
                    callBack(courses);
                else
                    callBack(null);
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
                callBack(null);
            }
        }));
    }

    public void loadAccessCourse(Action<List<Course>> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.LOAD_ALL_ACCESS_COURSE);
        string uri = reqBuilder.build();
        StartCoroutine(APIRequest.Instance.doGet(uri, (data) =>
        {
            if (data.error >= 0)
            {
                List<Course> courses = new List<Course>();
                JSONArray nodes = data.result as JSONArray;
                foreach (JSONNode node in nodes)
                {
                    Course course = parseCourse(data.result);
                    if (course != null)
                    {
                        courses.Add(course);
                    }
                }
                if (courses.Count != 0)
                    callBack(courses);
                else
                    callBack(null);
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
                JSONArray nodes = data.result as JSONArray;
                Course course = parseCourse(data.result);
                if (course != null)
                {
                    callBack(course);
                }
                else
                {
                    callBack(null);
                }
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
                callBack(null);
            }
        }));
    }

    public void createCourse(Course course, Action<Course> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.CREATE_COURSE);
        string uri = reqBuilder
            .AddParam("teacherId", course.TeacherId)
            .AddParam("name", course.Name)
            .AddParam("status", course.Status)
            .AddParam("discription", course.Status)
            .AddParam("contexts", course.Contexts)
            .build();
        StartCoroutine(APIRequest.Instance.doGet(uri, (data) =>
        {
            if (data.error >= 0)
            {
                JSONArray nodes = data.result as JSONArray;
                Course newCourse = parseCourse(data.result);
                callBack(newCourse);
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
                callBack(null);
            }
        }));
    }

    public void updateCourse(Course course, Action<Course> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.UPDATE_COURSE);
        string uri = reqBuilder.AddParam("id", course.Id)
            .AddParam("teacherId", course.TeacherId)
            .AddParam("name", course.Name)
            .AddParam("status", course.Status)
            .AddParam("discription", course.Status)
            .AddParam("contexts", course.Contexts)
            .build();
        StartCoroutine(APIRequest.Instance.doGet(uri, (data) =>
        {
            if (data.error >= 0)
            {
                JSONArray nodes = data.result as JSONArray;
                Course newCourse = parseCourse(data.result);
                callBack(newCourse);
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
                callBack(null);
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
                JSONArray nodes = data.result as JSONArray;
                Course newCourse = parseCourse(data.result);
                callBack(newCourse);
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
                callBack(null);
            }
        }));
    }
}
