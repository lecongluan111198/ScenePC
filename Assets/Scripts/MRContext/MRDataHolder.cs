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
    private bool isMR = false;
    private bool isEdit = false;

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
        defaultContent = File.ReadAllText(path); ;
        currentContext = new Context()
        {
            Id = 1,
            AvatarId = 1,
            Content = "{}",
            CreateTime = 1587788312000,
            Description = "Description for contexts",
            Name = "context 1",
            TeacherId = 1,
            Author = "Luan Lee"
        };
        isMR = true;
    }

    public Context CurrentContext { get => currentContext; set => currentContext = value; }
    public string DefaultContent { get => defaultContent; set => defaultContent = value; }
    public bool IsMR { get => isMR; set => isMR = value; }
    public bool IsEdit { get => isEdit; set => isEdit = value; }
}
