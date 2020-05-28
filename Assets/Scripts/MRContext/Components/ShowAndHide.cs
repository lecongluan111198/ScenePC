using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAndHide : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject listBG;
    public GameObject content;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    public void SetActiveGameobject()
    {
        listBG.SetActive(!listBG.active);
    }
}
