using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class APIRequest
{
    private static APIRequest _instance = null;
    public static APIRequest Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new APIRequest();
            }
            return _instance;
        }
    }

    private APIRequest(){ }

    /// <summary>
    /// Send Request to server with jsonbody
    /// </summary>
    /// <param name="url">API link</param>
    /// <param name="bodyJson">json body</param>
    /// <returns></returns>
    public IEnumerator doPost(string url, string bodyJson, Action<APIResponse> callBack)
    {
        using (var request = new UnityWebRequest(url, "POST"))
        {
            request.SetRequestHeader("Authorization", AccountInfo.Instance.Session);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJson);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            request.SendWebRequest();
            while (!request.isDone)
            {
                yield return null;
            }
            if (request.responseCode == 200)
            {
                string reponseJson = request.downloadHandler.text;
                callBack(APIResponse.textToReponse(reponseJson));
            }
            else
            {
                //Debug.Log(API.ERROR_CONNECT);
                callBack(null);
            }
        }
    }



    /// <summary>
    /// send request to server with api link
    /// </summary>
    /// <param name="url">api url</param>
    /// <returns></returns>
    public IEnumerator doGet(string url, Action<APIResponse> callBack)
    {
        using (var request = UnityWebRequest.Get(url))
        {
            //Debug.Log(url);
            request.SetRequestHeader("Authorization", AccountInfo.Instance.Session);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SendWebRequest();

            while (!request.isDone)
            {
                yield return null;
            }

            if (request.responseCode == 200)
            {
                string reponseJson = request.downloadHandler.text;
                callBack(APIResponse.textToReponse(reponseJson));
            }
            else
            {
                //Debug.Log(API.ERROR_CONNECT);
            }
        }
    }


    /// <summary>
    /// Dowload file from server
    /// </summary>
    /// <param name="url">API link</param>
    /// <param name="path">path to save file</param>
    /// <returns></returns>
    public IEnumerator downloadFile(string url, Action<byte[]> callBack)
    {
        using (var request = UnityWebRequest.Get(url))
        {
            //request.SetRequestHeader("Authorization", "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiIxIiwiZXhwIjoxNTg2MTYyNzIwLCJpYXQiOjE1ODM1NzA3MjB9.cCb-jUHdLVlCtDzCAx1vGlimeHTSV52OIN3vSdT0SPZCdCoj4dmZOT2BjuVyRBrc_eqQgkHwzBN4tqgDe8ehIg");
            request.SetRequestHeader("Authorization", AccountInfo.Instance.Session);
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();
            
            if (request.isNetworkError)
            {
                Debug.Log("NetworkError");
                //callBack(null);
            }
            if (request.isHttpError)
            {
                Debug.Log("HttpError");
            }

            byte[] results = request.downloadHandler.data;
            callBack(results);
        }
    }

    public IEnumerator uploadFile(string url, string path, Action<bool> callBack)
    {
        WWWForm form = new WWWForm();

        UnityWebRequest file = new UnityWebRequest();
        file = UnityWebRequest.Get(path);
        yield return file.SendWebRequest();

        form.AddBinaryData("file", file.downloadHandler.data, Path.GetFileName(path));

        UnityWebRequest req = UnityWebRequest.Post(url, form);
        req.SetRequestHeader("Authorization", AccountInfo.Instance.Session);
        yield return req.SendWebRequest();

        if (req.isNetworkError)
        {
            Debug.Log(req.error);
            callBack(false);
        }
        else
        {
            Debug.Log(ECode.SUCCESS);
            callBack(true);
        }
    }
}
