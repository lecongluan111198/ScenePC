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

        audioSource = ConvertContextUtils.AddComponent<AudioSource>(gameObject);
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
