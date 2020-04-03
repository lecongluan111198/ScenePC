using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class API 
{
    public static string DOMAIN = "http://localhost:8080/";

    public static string USER_LOGIN;
    public static string USER_LOGOUT;
    public static string USER_SINGUP;
    public static string USER_AUTHENTICATION;
    public static string USER_UPDATE_PROFILE;
    public static string USER_UPDATE_IMAGE_PROFILE;
    public static string USER_LOAD_PROFILE_IMAGE;


    public static string CREATE_CONTEXT = DOMAIN + "context/create";
    public static string LOAD_CONTEXT = DOMAIN + "context";
    public static string LOAD_OWN_CONTEXT = DOMAIN + "context/own";
    public static string LOAD_COURSE_CONTEXT = DOMAIN + "context/course";
    public static string UPDATE_CONTEXT = DOMAIN + "context/update";
    public static string DELETE_CONTEXT = DOMAIN + "context/delete";

    public static string CREATE_COURSE = DOMAIN + "context/create";
    public static string LOAD_COURSE = DOMAIN + "context";
    public static string LOAD_OWN_COURSE = DOMAIN + "context/own";
    public static string LOAD_ALL_ACCESS_COURSE = DOMAIN + "context/access/all";
    public static string LOAD_ACCESS_COURSE = DOMAIN + "context/access";
    public static string UPDATE_COURSE = DOMAIN + "context/update";
    public static string DELETE_COURSE = DOMAIN + "context/delete";

    public static string DOWNLOAD_FILE = DOMAIN + "model/";
    public static string UPLOAD_FILE = DOMAIN + "model/upload";
}
