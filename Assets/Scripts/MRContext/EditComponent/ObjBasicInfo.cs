using Microsoft.MixedReality.Toolkit.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjBasicInfo : MonoBehaviour
{
    public TMP_InputField text;
    public Text contentText;
    [SerializeField]
    private int id;
    [SerializeField]
    private string downloadName;
    [SerializeField]
    private bool fromServer;
    [SerializeField]
    private string content;

    public int Id { get => id; set => id = value; }
    public string DownloadName { get => downloadName; set => downloadName = value; }
    public bool FromServer { get => fromServer; set => fromServer = value; }
    public string Content
    {
        get => content;
        set
        {
            content = value;
        }
    }

    private BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
        UpdateText();
    }

    private void Update()
    {
        if (RightController.Instance != null && RightController.Instance.enabled)
        {
            //Debug.Log("enabled");
            if (RightController.Instance.CurrController.Type == AbstractController.ControllerType.BRUSH)
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

    public void OnUpdateText()
    {
        content = text.text;
    }

    public void UpdateText()
    {
        Debug.Log("Set text " + gameObject.name + " " + (text != null) + " " + content);
        try
        {
            //text.gameObject.SetActive(false);
            //contentText.gameObject.SetActive(true);
            text.text = content;
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
