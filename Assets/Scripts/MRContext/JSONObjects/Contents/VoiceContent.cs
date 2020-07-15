using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class VoiceContent : Content
{
    float[] data;

    public VoiceContent(float[] data) : base(EContent.VOICE)
    {
        this.Data = data;
    }

    public float[] Data { get => data; set => data = value; }

    public override object GetValue()
    {
        return Data;
    }
}
