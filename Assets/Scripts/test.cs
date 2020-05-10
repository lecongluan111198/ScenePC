using Newtonsoft.Json;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        KeyValuePair<bool, string> k = new KeyValuePair<bool, string>(false, "Fail");

        Debug.Log(k.Key);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
