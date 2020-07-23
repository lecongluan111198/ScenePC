using NAudio.Lame;
using NAudio.Wave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class SpeechToTextModel : MonoBehaviour
{
    private static SpeechToTextModel _instance = null;
    private static GameObject go;
    public static SpeechToTextModel Instance
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
                _instance = go.AddComponent<SpeechToTextModel>();
            }
            return _instance;
        }
    }

    private string IBM_API_KEY = Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes("apikey:8qcqzy1NoIIIWPDSkSYm7QR0eKRII6GgoRG1O4Pzr9sa"));

    public void SpeechToTextWatson(byte[] data, Action<string> callBack)
    {
        WWWForm form = new WWWForm();
        form.AddBinaryData("speech", ConvertWavToMp3(data));
        Dictionary<string, string> header = new Dictionary<string, string>();
        header.Add("Authorization", "Basic " + IBM_API_KEY);
        header.Add("Content-Type", "audio/mp3");
        StartCoroutine(APIRequest.Instance.doPost(API.SPEECH_TO_TEXT_API, form, header, (response)=> {
            if(response.result != null)
            {
                List<Dictionary<string, object>>  result = JSONUtils.toObject<List<Dictionary<string, object>>>(response.getStringParam("results", "[]"));
                if(result.Count != 0)
                {
                    var alternatives = JSONUtils.toObject<List<Dictionary<string, object>>>(result[0]["alternatives"].ToString());
                    if (alternatives != null && alternatives.Count != 0)
                    {
                        string transcript = alternatives[0]["transcript"] as string;
                        callBack(transcript);
                        return;
                    }
                }
            }
            callBack(null);
        }));
    }

    public void SpeechToTextWatson(string filePath, Action<string> callBack)
    {
        byte[] data = File.ReadAllBytes(filePath);
        SpeechToTextWatson(data, callBack);
    }

    private static byte[] ConvertWavToMp3(byte[] wavFile)
    {
        var retMs = new MemoryStream();
        var ms = new MemoryStream(wavFile);
        var rdr = new WaveFileReader(ms);
        var wtr = new LameMP3FileWriter(retMs, rdr.WaveFormat, 128);
        rdr.CopyTo(wtr);
        return retMs.ToArray();
    }
}
