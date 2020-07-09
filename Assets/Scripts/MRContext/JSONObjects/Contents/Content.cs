using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Content
{
    protected EContent type;

    protected Content(EContent type)
    {
        this.type = type;
    }

    public EContent Type { get => type; set => type = value; }

    public abstract object GetValue();
}
