using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    private bool isMove = false;
    private bool isScale = false;
    private bool isRotate = false;
    public bool IsMove { get => isMove; set => isMove = value; }
    public bool IsScale { get => isScale; set => isScale = value; }
    public bool IsRotate { get => isRotate; set => isRotate = value; }

    public void Move()
    {
        IsScale = false;
        IsRotate = false;
        IsMove = !IsMove;
    }
    public void Scale()
    {
        IsMove = false;
        IsRotate = false;
        IsScale = !IsScale;
    }
    public void Rotate()
    {
        IsMove = false;
        IsScale = false;
        IsRotate = !IsRotate;
    }
}
