using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootObject
{
    private int phaseCount;
    private List<Phase> phases;
    public int PhaseCount { get => phaseCount; set => phaseCount = value; }
    public List<Phase> Phases { get => phases; set => phases = value; }

    public RootObject()
    {
    }

    public RootObject(List<Phase> phases)
    {
        this.PhaseCount = phases.Count;
        this.Phases = phases;
    }

    public static RootObject toRootObject(List<Phase> phases)
    {
        RootObject root = null;
        if(phases != null)
        {
            root = new RootObject(phases);
        }
        return root;
    }
}
