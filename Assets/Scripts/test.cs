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
        //string str = "{\"$type\":\"QuestionComponentV2, Assembly-CSharp\",\"Question\":{\"$type\":\"TextContent, Assembly-CSharp\",\"Value\":\"How many ribs are there in a human skeleton?\",\"Type\":1},\"Choose\":{\"$type\":\"System.Collections.Generic.List`1[[Content, Assembly-CSharp]], mscorlib\",\"$values\":[{\"$type\":\"TextContent, Assembly-CSharp\",\"Value\":\"20\",\"Type\":1},{\"$type\":\"TextContent, Assembly-CSharp\",\"Value\":\"30\",\"Type\":1},{\"$type\":\"TextContent, Assembly-CSharp\",\"Value\":\"24\",\"Type\":1},{\"$type\":\"TextContent, Assembly-CSharp\",\"Value\":\"40\",\"Type\":1}]},\"Answer\":2,\"Id\":6,\"Name\":\"QS\"}";
        //QuestionComponentV2 ques = JSONUtils.toObject<QuestionComponentV2>(str);
        //Debug.Log(ques.Question.GetValue());
        //QuestionV2 q2 = gameObject.AddComponent(ques.getType()) as QuestionV2;
        //ques.updateInfomation(q2);
        //Debug.Log(q2.Question.GetValue());
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
