using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UserModel : MonoBehaviour
{
    private static UserModel _instance = null;
    private static GameObject go;
    public static UserModel Instance
    {
        get
        {
            if (_instance == null)
            {
                go = new GameObject();
                _instance = go.AddComponent<UserModel>();
            }
            return _instance;
        }
    }

    public void Login(string email, string password, Action<bool> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.USER_LOGIN)
            .AddParam("email", email)
            .AddParam("password", password);
        Debug.Log(reqBuilder.build());
        StartCoroutine(APIRequest.Instance.doPost(reqBuilder.build(), "{}", (data) =>
        {
            if (data.error >= 0)
            {
                AccountInfo.Instance.Email = email;
                AccountInfo.Instance.Username = data.getStringParam("username", "");
                AccountInfo.Instance.UID = data.getIntParam("uid", -1);
                AccountInfo.Instance.Session = data.getStringParam("sessKey", "");
                AccountInfo.Instance.Role = data.getStringParam("role", "");
                if (AccountInfo.Instance.Session == "" || AccountInfo.Instance.UID == -1 || AccountInfo.Instance.Role == "")
                {
                    Debug.Log("Session or Uid or role are empty");
                    Debug.Log(AccountInfo.Instance.Session);
                    Debug.Log(AccountInfo.Instance.UID);
                    Debug.Log(AccountInfo.Instance.Role);
                    callBack(false);
                }
                else
                {
                    callBack(true);
                }
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
                callBack(false);
            }
        }));
    }

    public void logout(Action<bool> callBack)
    {
        StartCoroutine(APIRequest.Instance.doPost(API.USER_LOGOUT, "{}", (data) =>
        {
            if (data.error >= 0)
            {
                AccountInfo.Instance.reset();
                callBack(true);
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
                callBack(false);
            }
        }));
    }

    public void register(string email, string username, string password, string role, Action<bool> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.USER_SINGUP)
            .AddParam("email", email)
            .AddParam("username", username)
            .AddParam("password", password)
            .AddParam("role", role);

        StartCoroutine(APIRequest.Instance.doPost(reqBuilder.build(), "{}", (data) =>
        {
            if (data.error >= 0)
            {
                AccountInfo.Instance.Email = email;
                AccountInfo.Instance.Username = username;
                AccountInfo.Instance.UID = data.getIntParam("uid", -1);
                AccountInfo.Instance.Role = role;
                if (AccountInfo.Instance.UID == -1)
                {
                    Debug.Log("Uid are empty");
                    callBack(false);
                }
                else
                {
                    callBack(true);
                }
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
                callBack(false);
            }
        }));
    }

    public void authentication(string verifyCode, int uid, Action<bool> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.USER_AUTHENTICATION)
            .AddParam("uid", uid)
            .AddParam("verifyCode", verifyCode);

        StartCoroutine(APIRequest.Instance.doPost(reqBuilder.build(), "{}", (data) =>
        {
            if (data.error >= 0)
            {
                AccountInfo.Instance.Session = data.getStringParam("sessKey", "");
                if (AccountInfo.Instance.Session == "")
                {
                    Debug.Log("Uid are empty");
                    callBack(false);
                }
                else
                {
                    callBack(true);
                }
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
                callBack(false);
            }
        }));
    }

    public void updateProfile(string username, Action<bool> callBack)
    {
        ReqParamBuilder reqBuilder = new ReqParamBuilder(API.USER_UPDATE_PROFILE)
            .AddParam("username", username);

        StartCoroutine(APIRequest.Instance.doPost(reqBuilder.build(), "{}", (data) =>
        {
            if (data.error >= 0)
            {
                AccountInfo.Instance.Username = username;
                callBack(true);
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
                callBack(false);
            }
        }));
    }

    public void updateProfileImage(string pathFile, Action<bool> callBack)
    {
        StartCoroutine(APIRequest.Instance.uploadFile(API.USER_UPDATE_IMAGE_PROFILE, pathFile, (data) =>
        {
            if (data.error >= 0)
            {
                AccountInfo.Instance.Image = File.ReadAllBytes(pathFile);
                callBack(true);
            }
            else
            {
                Debug.Log(data.error + ": " + data.message);
                callBack(false);
            }
        }));
    }

    public void loadProfileImage(Action<bool> callback)
    {
        StartCoroutine(APIRequest.Instance.downloadFile(API.USER_LOAD_PROFILE_IMAGE, (data) =>
        {
            if (data != null)
            {
                AccountInfo.Instance.Image = data;
                callback(true);
            }
            else
            {
                callback(false);
            }

        }));
    }
}
