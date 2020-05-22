using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjBasicInfo : MonoBehaviour
{
    [SerializeField]
    private int id;
    [SerializeField]
    private string downloadName;
    [SerializeField]
    private bool fromServer;

    public int Id { get => id; set => id = value; }
    public string DownloadName { get => downloadName; set => downloadName = value; }
    public bool FromServer { get => fromServer; set => fromServer = value; }
}
