using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserManual : MonoBehaviour
{
    public Text txt;
    public void txtMove(GameObject obj)
    {
        string moveText = "Ấn vào mũi tên để di chuyển " + obj.transform.name;
        txt.text = moveText;
    }
    public void txtRotation(GameObject obj)
    {
        string moveText = "Ấn vào mũi tên để xoay " + obj.transform.name;
        txt.text = moveText;
    }
    public void txtScale(GameObject obj)
    {
        string moveText = "Ấn vào mũi tên để thay đổi kích thước đối tượng " + obj.transform.name;
        txt.text = moveText;
    }
    public void txtDefault(GameObject obj)
    {
        string moveText = "Ấn vào công cụ để thực hiện thao tác trên đối tượng" + obj.transform.name;
        txt.text = moveText;
    }
}
