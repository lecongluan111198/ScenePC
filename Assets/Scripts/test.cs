using Newtonsoft.Json;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class test : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        var path = Path.Combine(Application.dataPath, "defaultJSON.json");
        string defaultContent = File.ReadAllText(path);
        Debug.Log(defaultContent.Length);
        string data = StringCompressor.CompressString(defaultContent);
        Debug.Log(data.Length);
        defaultContent = StringCompressor.DecompressString(data);
        Debug.Log(defaultContent.Length);

        audioSource = ConvertContextUtils.addComponent<AudioSource>(gameObject);
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
            audioSource.clip = Microphone.Start(deviceName, true, 20, 44100);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            Microphone.End(deviceName);
        }
        else if (Input.GetKey(KeyCode.P))
        {
            Debug.Log(audioSource.clip.length);
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

}
