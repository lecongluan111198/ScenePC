using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceContent : Content
{
    float[] data;

    public VoiceContent(float[] data) : base(EContent.VOICE)
    {
        this.data = data;
    }

    public override object GetValue()
    {
        return data;
    }
}
