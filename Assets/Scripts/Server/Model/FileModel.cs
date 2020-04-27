using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileModel : MonoBehaviour
{
    private static FileModel _instance = null;
    private static GameObject go;
    public static FileModel Instance
    {
        get
        {
            if (_instance == null)
            {
                go = new GameObject();
                _instance = go.AddComponent<FileModel>();
            }
            return _instance;
        }
    }

    public void _DownloadFile(string name, Action<byte[]> callBack)
    {
        string url = API.DOWNLOAD_FILE + name;
        StartCoroutine(APIRequest.Instance.downloadFile(name, (data) =>
        {
            callBack(data);
        }));
    }

    public void _DownloadObject(string objPath, string mtlPath, Action<List<byte[]>> callBack)
    {
        Debug.Log(objPath + " " + mtlPath);
        StartCoroutine(APIRequest.Instance.downloadFile(objPath, (obj) =>
        {
            Debug.Log("downloading obj");
            if(obj != null)
            {
                StartCoroutine(APIRequest.Instance.downloadFile(mtlPath, (mtl) =>
                {
                    if (mtl != null)
                    {
                        List<byte[]> file = new List<byte[]>();
                        file.Add(obj);
                        file.Add(mtl);
                        callBack(file);
                    }
                    else
                    {
                        callBack(null);
                    }
                }));
            }
            else
            {
                callBack(null);
            }
        }));
    }
}
