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
        ab.Add(new BoxColliderComponent("BXC", false, new List<double>() { 1, 2, 3 }, new List<double>() { 1, 2, 3 }));
        ab.Add(new BoxColliderComponent("BXC1", false, new List<double>() { 4, 5, 6 }, new List<double>() { 4, 5, 6 }));
        string json = JSONUtils.toJSONString(ab);
        Debug.Log(json);
        List<AbstractComponent> ab2 = JSONUtils.toObject<List<AbstractComponent>>(json);
        Debug.Log(ab2[0].Name);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
