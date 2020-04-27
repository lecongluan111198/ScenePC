using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    private bool isMove = false;
    private bool isScale = false;
    public bool IsMove { get => isMove; set => isMove = value; }
    public bool IsScale { get => isScale; set => isScale = value; }

    public void Move()
    {
        IsScale = false;
        IsMove = !IsMove;
    }
    public void Scale()
    {
        IsMove = false;
        IsScale = !IsScale;
    }
}
