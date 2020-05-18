using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditBackground : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject listRoom;
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showListRoom()
    {
        //foreach (Transform ts in abc.transform)
        //{
        //    bool active = GUILayout.Toggle(ts.gameObject.activeSelf, ts.gameObject.name);
        //    if (active != ts.gameObject.activeSelf)
        //        ts.gameObject.SetActive(active);
        //}
        listRoom.SetActive(!listRoom.active);
    }
}
