using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
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
    public IEnumerator doPost(string url, string param, Action<APIResponse> callBack)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection(param));

        //using (var request = new UnityWebRequest(url, "POST"))
        using (var request = UnityWebRequest.Post(url, formData))
        {
            request.SetRequestHeader("Authorization", AccountInfo.Instance.Session);
            //byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJson);
            //request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            while (!request.isDone)
            {
                yield return null;
            }
            Debug.Log(request.responseCode);
            Debug.Log(request.downloadHandler.text);
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

    public IEnumerator doPost(string url, Dictionary<string, object> mapData, Action<APIResponse> callBack)
    {
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<string, object> entry in mapData)
        {
            form.AddField(entry.Key, entry.Value.ToString());
        }
        Debug.Log(form.ToString());
        using (var request = UnityWebRequest.Post(url, form))
        {
            request.SetRequestHeader("Authorization", AccountInfo.Instance.Session);
            //byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJson);
            //request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            //request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            //request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            while (!request.isDone)
            {
                yield return null;
            }
            Debug.Log(request.responseCode);
            Debug.Log(request.downloadHandler.text);
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

    public IEnumerator doPost(string url, WWWForm form, Dictionary<string, string> header, Action<APIResponse> callBack)
    {
        //form = new WWWForm();
        //byte[] data = File.ReadAllBytes(@"C:\Users\User\AppData\LocalLow\DefaultCompany\ScenePC\recordings\200723-030104-431.wav");
        //byte[] data = File.ReadAllBytes(@"F:\Library\IT\Thesis\Unity\Resources\Unity\hello.wav");
        //byte[] data = File.ReadAllBytes(@"F:\Library\IT\Thesis\Unity\Resources\Unity\hello.mp3");
        //byte[] data = File.ReadAllBytes(@"F:\Library\IT\Thesis\Unity\Resources\Unity\audio-file.flac");
        //Debug.Log(data.Length);
        //form.AddBinaryData("hello.wav", data);
        
        //form.AddBinaryData("hello.mp3", data);

        using (var request = UnityWebRequest.Post(url, form))
        {
            foreach (KeyValuePair<string, string> entry in header)
            {
                Debug.Log(entry.Key + " " + entry.Value);
                request.SetRequestHeader(entry.Key, entry.Value);
            }
            //string base64 = Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes("apikey:8qcqzy1NoIIIWPDSkSYm7QR0eKRII6GgoRG1O4Pzr9sa"));
            //request.SetRequestHeader("Authorization", "Basic " + base64);
            //request.SetRequestHeader("Content-Type", "audio/mp3");
            yield return request.SendWebRequest();

            while (!request.isDone)
            {
                yield return null;
            }
            Debug.Log(request.responseCode);
            Debug.Log(request.downloadHandler.text);
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
            Debug.Log(request.responseCode);
            if (request.responseCode == 200)
            {
                string reponseJson = request.downloadHandler.text;
                callBack(APIResponse.textToReponse(reponseJson));
            }
            else
            {
                callBack(null);
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

    public IEnumerator uploadFile(string url, string path, Action<APIResponse> callBack)
    {
        WWWForm form = new WWWForm();

        UnityWebRequest file = new UnityWebRequest();
        file = UnityWebRequest.Get(path);
        yield return file.SendWebRequest();

        form.AddBinaryData("file", file.downloadHandler.data, Path.GetFileName(path));

        UnityWebRequest req = UnityWebRequest.Post(url, form);
        req.SetRequestHeader("Authorization", AccountInfo.Instance.Session);
        yield return req.SendWebRequest();

        if (req.responseCode == 200)
        {
            string reponseJson = req.downloadHandler.text;
            callBack(APIResponse.textToReponse(reponseJson));
        }
        else
        {
            Debug.Log(ECode.SUCCESS);
            callBack(null);
        }
    }
}
