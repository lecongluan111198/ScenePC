using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowOrbit : MonoBehaviour
{

    public float orbitSpeed;

    public int numberOfpositions;
    private LineRenderer lineRenderer;

    public int earthOffset = 1;

    public Vector3[] localOrbitPositions;
    public GameObject orbitToFollow;

    public GameObject OrbitToFollow { get => orbitToFollow; set => orbitToFollow = value; }

    // Use this for initialization
    void Start()
    {
        if (MRDataHolder.Instance.IsEdit)
        {
            orbitToFollow = transform.parent.Find("Orbit").gameObject;
        }
        else
        {
            string orbitName = transform.name.Replace("(Clone)", "") + "Orbit";
            orbitToFollow = GameObject.Find("ListObject").transform.Find(orbitName).gameObject;
        }
        lineRenderer = orbitToFollow.GetComponent<LineRenderer>();
        numberOfpositions = lineRenderer.positionCount;
        if (earthOffset >= numberOfpositions)
        {
            //earthOffset = 1;
            orbitToFollow.GetComponent<EllipseLineRenderer>().Init();
            numberOfpositions = lineRenderer.positionCount;
        }
        transform.localPosition = orbitToFollow.transform.TransformPoint(lineRenderer.GetPosition(numberOfpositions - earthOffset));

        // save obrbit local positions for reference, so we do not have to calculate global position on coroutine
        localOrbitPositions = new Vector3[lineRenderer.positionCount];
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            localOrbitPositions[i] = orbitToFollow.transform.TransformPoint(lineRenderer.GetPosition(i));
            localOrbitPositions[i].x = (float)Math.Round(localOrbitPositions[i].x, 3);
            localOrbitPositions[i].y = (float)Math.Round(localOrbitPositions[i].y, 3);
            localOrbitPositions[i].z = (float)Math.Round(localOrbitPositions[i].z, 3);
        }
        if (!MRDataHolder.Instance.IsEdit)
        {
            StartCoroutine(ConstantValues.FollowOrbitCoroutine);
        }
    }

    private IEnumerator MoveOnOrbit()
    {
        Vector3 parent = transform.parent.localPosition;
        // move on orbit if orbit speed for planet has been set up
        while (Mathf.Abs(orbitSpeed) > 0)
        {
            Vector3 pos = transform.localPosition + parent;
            pos.x = (float)Math.Round(pos.x, 3);
            pos.y = (float)Math.Round(pos.y, 3);
            pos.z = (float)Math.Round(pos.z, 3);
           
            if (pos == localOrbitPositions[numberOfpositions - earthOffset])
            {
                earthOffset += 4;
            }
            else
            {
                //transform.position = Vector3.MoveTowards(transform.position, localOrbitPositions[numberOfpositions- earthOffset], Time.deltaTime * orbitSpeed * ConfigManager.instance.orbitSpeedInDaysPerSecond);
                //transform.position = Vector3.MoveTowards(transform.position, localOrbitPositions[numberOfpositions - earthOffset], Time.deltaTime * orbitSpeed * 85.1f);
                transform.position = Vector3.MoveTowards(transform.position, localOrbitPositions[numberOfpositions - earthOffset], Time.deltaTime * orbitSpeed * MRDataHolder.Instance.Speed);
            }

            if (earthOffset >= numberOfpositions)
            {
                earthOffset = 1;
            }
            yield return null;
        }
    }
}
