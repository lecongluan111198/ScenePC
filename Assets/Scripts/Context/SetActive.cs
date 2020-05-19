using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject room;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void show()
    {
        room.SetActive(true);
    }
    public void hide()
    {
        room.SetActive(false);
    }
}
