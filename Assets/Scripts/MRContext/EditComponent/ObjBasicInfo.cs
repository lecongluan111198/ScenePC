using Microsoft.MixedReality.Toolkit.Input;
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

    private BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (RightController.Instance != null && RightController.Instance.enabled)
        {
            //Debug.Log("enabled");
            if(RightController.Instance.CurrController.Type == AbstractController.ControllerType.BRUSH)
            {
                //Debug.Log("BRUSH");
                if (boxCollider != null && boxCollider.enabled)
                {
                    //Debug.Log("false");
                    boxCollider.enabled = false;
                }
            }
            else
            {
                if (boxCollider != null && !boxCollider.enabled)
                {
                    boxCollider.enabled = true;
                }
            }
        }
    }
}
