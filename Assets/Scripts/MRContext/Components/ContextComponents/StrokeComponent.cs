using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StrokeComponent : AbstractComponent
{
    private List<List<double>> positions = new List<List<double>>();

    public List<List<double>> Positions { get => positions; set => positions = value; }

    [JsonConstructor]
    public StrokeComponent(string name, List<List<double>> positions) : base((int) EComponent.STROKE, name)
    {
        this.Positions = positions;
    }

    public StrokeComponent(string name, List<Vector3> position1s) : base((int)EComponent.STROKE, name)
    {
        foreach (Vector3 pos in position1s)
        {
            this.Positions.Add(ConvertTypeUtils.Vector3ToList(pos));
        }
    }

    public override Type getType()
    {
        return typeof(Stroke);
    }

    public override void updateInfomation(Component component)
    {
        Stroke stroke = component as Stroke;
        stroke.UpdateInformation(Positions);
    }
}
