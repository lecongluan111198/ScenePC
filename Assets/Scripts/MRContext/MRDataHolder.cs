using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MRDataHolder
{
    private static MRDataHolder instance = null;
    private string defaultContent;
    private Context currentContext;
    private bool isEdit;
    private bool isRecord;
    private GameObject currentClickObject;
    private float speed = 50;
    //private List<string> objectWhitelist = new List<string>() {"Sun", "Earth", "Jupiter", "Mars", "Mercury", "Neptune", "Pluto", "Saturn", "Uranus", "Venus", "Gorilla", "Leopard", "Description" };
    private List<string> objectWhitelist = new List<string>() {"Gorilla", "Leopard", "Description" };


    public static MRDataHolder Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MRDataHolder();
            }
            return instance;
        }
    }

    public MRDataHolder()
    {
        var path = Path.Combine(Application.dataPath, "defaultJSON.json");
        defaultContent = File.ReadAllText(path);
        currentContext = new Context()
        {
            Id = 5,
            AvatarId = 3,
            Content = defaultContent,
            CreateTime = 1587788312000,
            Description = "Description for contexts",
            Name = "context 5",
            TeacherId = 1,
            Author = "Luan Lee"
        };
        isEdit = false;
        IsRecord = false;
    }

    public void updateCurrentContext(Context currentContext, bool isEdit = true)
    {
        this.currentContext = Context.CloneContext(currentContext);
        this.currentContext.Content = StringCompressor.DecompressString(currentContext.Content);
        if (currentContext.Content == null || currentContext.Content.Equals("") || currentContext.Content.Equals("{}"))
        {
            currentContext.Content = defaultContent;
        }

        this.isEdit = isEdit;
    }

    public Context CurrentContext {
        get { return currentContext; }
        set {
            currentContext = value;
            currentContext.Content = StringCompressor.DecompressString(value.Content);
        }
    }
    public string DefaultContent { get => defaultContent; set => defaultContent = value; }
    public bool IsEdit { get => isEdit; set => isEdit = value; }
    public bool IsRecord { get => isRecord; set => isRecord = value; }
    public GameObject CurrentClickObject { get => currentClickObject; set => currentClickObject = value; }
    public float Speed { get => speed; set => speed = value; }
    public List<string> ObjectWhitelist { get => objectWhitelist; set => objectWhitelist = value; }
}
