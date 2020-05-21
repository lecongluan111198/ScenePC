using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EditContextHolder
{
    private static EditContextHolder instance = null;
    private string defaultContent;
    private Context currentContext;
    private bool isMR = false;

    public static EditContextHolder Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EditContextHolder();
            }
            return instance;
        }
    }

    public EditContextHolder()
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
}
