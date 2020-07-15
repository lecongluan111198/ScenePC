using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stroke : MonoBehaviour
{
    private List<Vector3> positions = new List<Vector3>();
    private LineRenderer line;

    public List<Vector3> Positions { get => positions; set => positions = value; }

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        if (line == null)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateInformation(List<List<double>> data)
    {
        line = GetComponent<LineRenderer>();
        if (line == null)
        {
            Destroy(gameObject);
        }
        line.positionCount = 0;
        Vector3 exPos;
        foreach (List<double> pos in data)
        {
            line.positionCount += 1;
            exPos = ConvertTypeUtils.ListToVector3(pos);
            line.SetPosition(line.positionCount - 1, exPos);
            Positions.Add(exPos);
        }
    }

    public void AddPosition(Vector3 pos)
    {
        Positions.Add(pos);
    }
}
