using Newtonsoft.Json;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = ConvertContextUtils.addComponent<AudioSource>(gameObject);
        //List<AbstractComponent> ab = new List<AbstractComponent>();
        //ab.Add(new BoxColliderComponent("BXC", false, new List<double>() { 1, 2, 3 }, new List<double>() { 1, 2, 3 }));
        //ab.Add(new BoxColliderComponent("BXC1", false, new List<double>() { 4, 5, 6 }, new List<double>() { 4, 5, 6 }));
        //string json = JSONUtils.toJSONString(ab);
        //Debug.Log(json);
        //List<AbstractComponent> ab2 = JSONUtils.toObject<List<AbstractComponent>>(json);
        //Debug.Log(ab2[0].Name);
        Debug.Log(Microphone.devices.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            string deviceName = Microphone.devices[0].ToString();
            Debug.Log(deviceName);
            if (Microphone.IsRecording(deviceName))
                return;
            audioSource.clip = Microphone.Start(deviceName, true, 5, 44100);
        }
        else if (Input.GetKey(KeyCode.P))
        {
            Debug.Log(audioSource.clip.length);
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

}
