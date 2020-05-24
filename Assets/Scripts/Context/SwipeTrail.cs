using RTEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTrail : MonoBehaviour
{
    public GameObject trailPrefab;
    private GameObject thisTrail;
    private Vector3 startPos;
    private Plane objPlane;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);
            Debug.Log("start");
            //objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);
            //thisTrail = new GameObject();
            //thisTrail.transform.position = this.transform.position;
            //thisTrail.transform.rotation = Quaternion.identity;

            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;

            if (objPlane.Raycast(mRay, out rayDistance))
            {
                startPos = mRay.GetPoint(rayDistance);
                thisTrail = (GameObject)Instantiate(trailPrefab, startPos, Quaternion.identity);
                Debug.Log(thisTrail);
                //this.transform.position = mRay.GetPoint(rayDistance);
            }
        }
        else if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0)))
        {
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            float rayDistance;

            if (objPlane.Raycast(mRay, out rayDistance))
            {
                thisTrail.transform.position = mRay.GetPoint(rayDistance);
            }
        }
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
        {
            if (Vector3.Distance(thisTrail.transform.position, startPos) < 0.1)
            {
                Destroy(thisTrail);
            }
        }
    }
}
