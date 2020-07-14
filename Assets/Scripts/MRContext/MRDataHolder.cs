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
            Id = 3,
            AvatarId = 1,
            Content = defaultContent,
            CreateTime = 1587788312000,
            Description = "Description for contexts",
            Name = "context 3",
            TeacherId = 1,
            Author = "Luan Lee"
        };
        isEdit = true;
        IsRecord = false;
    }

    public void updateCurrentContext(Context currentContext, bool isEdit = true)
    {
        this.currentContext = currentContext;
        if(currentContext.Content != null && currentContext.Content != "" & currentContext.Content != "{}")
        {
            defaultContent = currentContext.Content;
        }

        this.isEdit = isEdit;
    }

    public Context CurrentContext { get => currentContext; set => currentContext = value; }
    public string DefaultContent { get => defaultContent; set => defaultContent = value; }
    public bool IsEdit { get => isEdit; set => isEdit = value; }
    public bool IsRecord { get => isRecord; set => isRecord = value; }
    public GameObject CurrentClickObject { get => currentClickObject; set => currentClickObject = value; }
}
