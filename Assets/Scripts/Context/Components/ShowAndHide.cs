﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAndHide : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject go;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetActiveGameobject()
    {
        go.SetActive(!go.active);
    }
}
