﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObjectScript : MonoBehaviour
{
    [Header("CONTAINER")]
    public GameObject container;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addObjbect()
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = "brown_cube";
        cube.transform.parent = container.transform;
    }
}
