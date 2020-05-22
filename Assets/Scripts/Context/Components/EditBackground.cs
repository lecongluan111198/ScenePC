using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EditBackground : MonoBehaviour
{
    public GameObject listRoom;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
        
        }
    }
    private bool checkLoad()
    {
        int load = 0;
        foreach (Transform ts in listRoom.transform)
        {
            load++;
        }
        if (load == 0)
            return true;
        return false;
    }
    public void showListRoom()
    {
        if (checkLoad()==true)
        {
            loadListRoom();
        }
    }
    private void loadListRoom()
    {
        var pathButton = "Assets/Prefabs/UI/Buttons/RoomButton.prefab";
        var pathImage = "Assets/Resources/Images/Backgrounds/EditTheme/SurgeryRoom.jpg";
        var path = "Assets/Prefabs/UI/Room/";
        var absolutePaths = System.IO.Directory.GetFiles(path, "*.prefab", System.IO.SearchOption.AllDirectories);
        foreach(var a in absolutePaths)
        {
            var textButton = a.Substring(a.LastIndexOf("/") + 1);
            textButton = textButton.Substring(0, textButton.Length - 7);
            GameObject button = (GameObject)Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(pathButton, typeof(GameObject)),listRoom.transform.position,listRoom.transform.rotation,listRoom.transform);
            button.name = textButton + " button";
            button.GetComponentInChildren<TextMeshProUGUI>().text = textButton;
        }
    }

}
