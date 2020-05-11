using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTool : MonoBehaviour
{
    private GameObject obj;
    public GameObject X;
    public GameObject Y;
    public GameObject Z;

    float rotationY = 0.0f;
    float rotationX = 0.0f;
    float rotationZ = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.name == X.name)
                    moveX();
                else if (hit.transform.name == Y.name)
                    moveY();
                else if (hit.transform.name == Z.name)
                    moveZ();
                else if (hit.transform.name == obj.name)
                    Debug.Log(obj.name);
            }
        }
    }
    private void moveX()
    {
        if (Input.GetMouseButton(0) && GUIUtility.hotControl == 0)
        {
            //Debug.Log(Time.deltaTime);
            rotationX += Input.GetAxis("Mouse X") * Time.deltaTime * 10.0f;
            if (Input.GetAxis("Mouse X") > 0)
            {
                transform.position += new Vector3(-rotationX, 0, 0);
                obj.transform.position += new Vector3(-rotationX, 0, 0);
            }
            else if (Input.GetAxis("Mouse X") < 0)
            {
                transform.position += new Vector3(rotationX, 0, 0);
                obj.transform.position += new Vector3(rotationX, 0, 0);
            }
        }
        else if (Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0)
        {
            // Debug.Log(Time.deltaTime);
            rotationX += Input.GetAxis("Mouse X") * Time.deltaTime * 10.0f;
            if (Input.GetAxis("Mouse X") > 0)
            {
                transform.position += new Vector3(-rotationX, 0, 0);
                obj.transform.position += new Vector3(-rotationX, 0, 0);
            }
            else if (Input.GetAxis("Mouse X") < 0)
            {
                transform.position += new Vector3(rotationX, 0, 0);
                obj.transform.position += new Vector3(rotationX, 0, 0);
            }
        }
    }
    private void moveY()
    {
        if (Input.GetMouseButton(0) && GUIUtility.hotControl == 0)
        {
            rotationY += Input.GetAxis("Mouse Y") * Time.deltaTime * 10.0f;
            // Debug.Log(Input.GetAxis("Mouse Y"));
            if (Input.GetAxis("Mouse Y") < 0)
            {
                transform.position += new Vector3(0, -rotationY, 0);
                obj.transform.position += new Vector3(0, -rotationY, 0);
            }
            else if (Input.GetAxis("Mouse Y") > 0)
            {
                transform.position += new Vector3(0, rotationY, 0);
                obj.transform.position += new Vector3(0, rotationY, 0);
            }
        }
        else if (Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0)
        {
            rotationY += Input.GetAxis("Mouse Y") * Time.deltaTime * 10.0f;
            //  Debug.Log(Input.GetAxis("Mouse Y"));
            if (Input.GetAxis("Mouse Y") < 0)
            {
                transform.position += new Vector3(0, -rotationY, 0);
                obj.transform.position += new Vector3(0, -rotationY, 0);
            }
            else if (Input.GetAxis("Mouse Y") > 0)
            {
                transform.position += new Vector3(0, rotationY, 0);
                obj.transform.position += new Vector3(0, rotationY, 0);
            }
        }
    }
    private void moveZ()
    {
        if (Input.GetMouseButton(0) && GUIUtility.hotControl == 0)
        {
            rotationZ += Input.GetAxis("Mouse X") * Time.deltaTime * 10.0f;
            Debug.Log(Input.GetAxis("Mouse X"));
            if (Input.GetAxis("Mouse X") < 0)
            {
                transform.position += new Vector3(0, 0, rotationZ);
                obj.transform.position += new Vector3(0, 0, rotationZ);
            }
            else if (Input.GetAxis("Mouse X") > 0)
            {
                transform.position += new Vector3(0, 0, -rotationZ);
                obj.transform.position += new Vector3(0, 0, -rotationZ);
            }
        }
        else if (Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0)
        {
            rotationZ += Input.GetAxis("Mouse X") * Time.deltaTime * 10.0f;
            Debug.Log(Input.GetAxis("Mouse X"));
            if (Input.GetAxis("Mouse X") < 0)
            {
                transform.position += new Vector3(0, 0, rotationZ);
                obj.transform.position += new Vector3(0, 0, rotationZ);
            }
            else if (Input.GetAxis("Mouse X") > 0)
            {
                transform.position += new Vector3(0, 0, -rotationZ);
                obj.transform.position += new Vector3(0, 0, -rotationZ);
            }
        }
    }
}
