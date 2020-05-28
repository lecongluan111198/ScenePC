using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase
{
    public int nObject { get; set; }
    public BackgroundObject backgroundObject { get; set; }
    public List<ContextObject> Objects { get; set; }

    public static Phase toPhase(BackgroundObject bo, List<ContextObject> objects)
    {
        Phase phase = null;
        if (bo != null && objects != null)
        {
            phase = new Phase();
            phase.nObject = objects.Count;
            phase.Objects = objects;
            phase.backgroundObject = bo;
        }
        return phase;
    }
}
