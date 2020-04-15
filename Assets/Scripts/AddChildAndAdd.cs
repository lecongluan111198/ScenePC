using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddChildAndAdd : MonoBehaviour
{
    void Start()
    {
        WithForeachLoop();
       // WithForLoop();
    }

    void WithForeachLoop()
    {
        foreach (Transform child in transform)
            print("Foreach loop: " + child);
    }

    //void WithForLoop()
    //{
    //    int children = transform.childCount;
    //    for (int i = 0; i < children; ++i)
    //        print("For loop: " + transform.GetChild(i));
    //}
}
