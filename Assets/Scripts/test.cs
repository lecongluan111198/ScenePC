using Newtonsoft.Json;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class test : MonoBehaviour
{
    public AudioSource second;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        //string json = StringCompressor.DecompressString(data);
        //Debug.Log(json.Contains("VoiceContent12"));
        //File.WriteAllText(@"Assets/saveJSON1.txt", json);
        //SendingEmailUtils.SendMail();

        //var defaultContent = File.ReadAllText(@"Assets/saveJSON1.txt");
        //QuestionComponentV2 ques = JSONUtils.toObject<QuestionComponentV2>(defaultContent);
        //Debug.Log(ques.Question.Type);
        string data = "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";
        string json = StringCompressor.DecompressString(data);
        Debug.Log(json);
        File.WriteAllText(@"Assets/saveJSON1.txt", json);
        //audioSource = ConvertContextUtils.AddComponent<AudioSource>(gameObject);
    }

    string deviceName;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            deviceName = Microphone.devices[0].ToString();
            Debug.Log(deviceName);
            if (Microphone.IsRecording(deviceName))
                return;
            audioSource.clip = null;
            audioSource.clip = Microphone.Start(deviceName, true, 60, 44100);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            Microphone.End(deviceName);
            //SavWav.Save(@"Assets/clip", audioSource.clip);
        }
        else if (Input.GetKey(KeyCode.P))
        {
            Debug.Log(audioSource.clip.samples + " " + audioSource.clip.channels);
            float[] data = new float[audioSource.clip.samples * audioSource.clip.channels];
            audioSource.clip.GetData(data, 0);
            AudioClip clip = AudioClip.Create("QuesVoice", data.Length, audioSource.clip.channels, audioSource.clip.frequency, false);
            clip.SetData(data, 0);

            second.clip = clip;
            second.Play();
            Debug.Log(data.Length);
            var com = StringCompressor.CompressString(JSONUtils.toJSONString(data));
            Debug.Log(com.Length);
            com = StringCompressor.CompressString(com);
            Debug.Log(com.Length);
            //audioSource.Play();
        }
    }

}
