using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Phase
{
    public int nObject { get; set; }
    public ContextObject backgroundObject { get; set; }
    public List<ContextObject> Objects { get; set; }

    public static Phase toPhase(ContextObject bo, List<ContextObject> objects)
    {
        Phase phase = null;
        if (bo != null && objects != null)
        {
            phase = new Phase();
            phase.nObject = objects.Count;
            phase.Objects = objects;
            phase.backgroundObject = bo;
        }
        else
        {
            Debug.Log("Bacground and objects cannot be null");
        }
        return phase;
    }
}
