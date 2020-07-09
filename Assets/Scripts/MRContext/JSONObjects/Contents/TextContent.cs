﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TextContent : Content
{
    private string value;

    public TextContent(string value) : base(EContent.TEXT)
    {
        this.value = value;
    }

    public override object GetValue()
    {
        return value;
    }
}