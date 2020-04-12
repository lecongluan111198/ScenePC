using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using SimpleJSON;

public class EditContext : MonoBehaviour
{
    public Text textM;
    public Text userNameText;
    int i = 0;
//    public void Request()
//    {
//        i++;
//        userNameText.text = "" + i;
//        string jsonfile = "abcbsbdb"; //nhap string
//        WWW request = new WWW(jsonfile);
//        PlayerPrefs.SetInt("authComplete", 0);
//        textM.text = "Requested2";
//        StartCoroutine(OnResponse(request));
//    }
//    private IEnumerator OnResponse(WWW req)
//    {
//        yield return req;
//        textM.text = req.text; //Response Json
//        var rt = JsonConvert.DeserializeObject<RootObject>(req.text);
//        string nameObj = rt.jsonData.Scene.name;
//        userNameText.text = nameObj + ":" + i;
//        string linkObj = rt.jsonData.Scene.link;
//        StartCoroutine(loadObject(linkObj));
//    }  
//    IEnumerator loadObject(string url)
//    {
//        if (Application.internetReachability == NetworkReachability.NotReachable)
//        {
//            yield return null;
//        }
//        var www = new WWW(url);
//        Debug.Log("Download obj on progress");
//        yield return www;
//        if (string.IsNullOrEmpty(www.text))
//            Debug.Log("Download Failed");
//        else
//        {
//            //code download obj
//            Debug.Log("Download Success");
//        }
//    }
    public void GetJsonData()
    {
        StartCoroutine(rRequestWebService());
    }
    IEnumerator rRequestWebService()
    {
        string getDataUrl = "https://drive.google.com/uc?export=download&id=13GU3xtSNds0t4UcneXxoA5RnREkoZkjL"; //duong dan
        print(getDataUrl);
        using(UnityWebRequest webData = UnityWebRequest.Get(getDataUrl))
        {
            yield return webData.SendWebRequest();
            if (webData.isNetworkError || webData.isHttpError)
            {
                print("Error");
                print(webData.error);
            }
            else
            {
                if (webData.isDone)
                {
                    JSONNode jsonData = JSON.Parse(System.Text.Encoding.UTF8.GetString(webData.downloadHandler.data));
                    if (jsonData == null)
                    {
                        print("No Data");
                    }
                    else
                    {
                        print("Json Data");
                        print("Count:" + jsonData.Count);
                    }
                }
            }
        }
    }
}
[Serializable]
public class Question
{
    public string question { get; set; }
    public string choiceA { get; set; }
    public string choiceB { get; set; }
    public string choiceC { get; set; }
    public string correct { get; set; }
}
[Serializable]
public class Component
{
    public List<Question> questions { get; set; }
}
[Serializable]
public class Scene
{
    public string id { get; set; }
    public string link { get; set; }
    public string name { get; set; }
    public List<int> position { get; set; }
    public List<int> rotation { get; set; }
    public Component component { get; set; }
}
[Serializable]
public class JsonData
{
    public Scene Scene { get; set; }
}
[Serializable]
public class RootObject
{
    public JsonData jsonData { get; set; }
}
