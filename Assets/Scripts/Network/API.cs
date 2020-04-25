using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class API 
{
    public static string DOMAIN = "https://hcmus-server.appspot.com/";
    //public static string DOMAIN = "http://localhost:8080/";

    public static string USER_LOGIN = DOMAIN + "login";
    public static string USER_LOGOUT = DOMAIN + "logout";
    public static string USER_SINGUP = DOMAIN + "register";
    public static string USER_AUTHENTICATION = DOMAIN + "authenVerifyCode";
    public static string USER_AUTHENTICATION_RESEND = DOMAIN + "register/resendVerifyCode";
    public static string USER_UPDATE_PROFILE = DOMAIN + "profile";
    public static string USER_UPDATE_IMAGE_PROFILE = DOMAIN + "profile/image";
    public static string USER_LOAD_PROFILE_IMAGE;


    public static string CREATE_CONTEXT = DOMAIN + "context/create";
    public static string LOAD_CONTEXT = DOMAIN + "context";
    public static string LOAD_OWN_CONTEXT = DOMAIN + "context/own";
    public static string LOAD_COURSE_CONTEXT = DOMAIN + "context/course";
    public static string UPDATE_CONTEXT = DOMAIN + "context/update";
    public static string DELETE_CONTEXT = DOMAIN + "context/delete";

    public static string CREATE_COURSE = DOMAIN + "course/create";
    public static string LOAD_COURSE = DOMAIN + "course";
    public static string LOAD_OWN_COURSE = DOMAIN + "course/own";
    public static string LOAD_ALL_ACCESS_COURSE = DOMAIN + "course/access/all";
    public static string LOAD_ACCESS_COURSE = DOMAIN + "course/access";
    public static string ADD_ACCESS_COURSE = DOMAIN + "/course/access/add";
    public static string UPDATE_COURSE = DOMAIN + "course/update";
    public static string DELETE_COURSE = DOMAIN + "course/delete";

    public static string DOWNLOAD_FILE = DOMAIN + "model/";
    public static string UPLOAD_FILE = DOMAIN + "model/upload";
}
