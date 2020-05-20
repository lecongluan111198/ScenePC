using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
        if (Input.GetKeyDown(KeyCode.K))
        {
            showListRoom();
        }
    }

    public void showListRoom()
    {
        var pathButton = "Assets/Prefabs/UI/Buttons/Room.prefab";
        var path = "Assets/Prefabs/UI/Room/";
        var absolutePaths = System.IO.Directory.GetFiles(path, "*.prefab", System.IO.SearchOption.AllDirectories);
        Debug.Log("absolute: " + absolutePaths);
        foreach(var a in absolutePaths)
        {
            var textButton = a.Substring(a.LastIndexOf("/") + 1);
            textButton = textButton.Substring(0, textButton.Length - 7);
            GameObject button = (GameObject)Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(pathButton, typeof(GameObject)),listRoom.transform.position,listRoom.transform.rotation,listRoom.transform);
            Debug.Log("Create: " + textButton);
            button.GetComponentInChildren<Text>().text = textButton;
            Debug.Log(button.GetComponentInChildren<Text>().text);
        }
    }
}
