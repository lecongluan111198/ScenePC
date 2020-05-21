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
        List<AbstractComponent> ab = new List<AbstractComponent>();
        ab.Add(new BoxColliderComponent(false, new List<double>() { 1, 2, 3 }, new List<double>() { 1, 2, 3 }));
        ab.Add(new BoxColliderComponent(false, new List<double>() { 4, 5, 6 }, new List<double>() { 4, 5, 6 }));
        Debug.Log(JsonConvert.SerializeObject(ab));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
