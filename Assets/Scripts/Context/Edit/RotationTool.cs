using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTool : MonoBehaviour
{
    public GameObject obj;
    public GameObject X;
    public GameObject Y;
    public GameObject Z;

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
               // Debug.Log(hit.transform.name);
                if (hit.transform.name == X.name)
                    RotateX();
                else if (hit.transform.name == Y.name)
                    RotateY();
                else if (hit.transform.name == Z.name)
                    RotateZ();
                else if (hit.transform.name == obj.name)
                    Debug.Log(obj.name);
            }
        }
    }
    private void RotateX()
    {
        if (Input.GetMouseButton(0) && GUIUtility.hotControl == 0)
        {
            Debug.Log("click rotatex");
            Quaternion rotate = obj.transform.rotation;
            if (Input.GetAxis("Mouse X") > 0)
            {
                rotate = new Quaternion(rotate.x * 0.9f, rotate.y, rotate.z, 0);
                obj.transform.rotation = rotate;
                
            }
            else if (Input.GetAxis("Mouse X") < 0)
            {
                rotate = new Quaternion(rotate.x * 1.1f, rotate.y, rotate.z, 0);
                obj.transform.rotation = rotate;
               
            }
        }
        else if (Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0)
        {
            Quaternion rotate = obj.transform.rotation;
            if (Input.GetAxis("Mouse X") > 0)
            {
                rotate = new Quaternion(rotate.x * 0.9f, rotate.y, rotate.z, 0);
                obj.transform.rotation = rotate;
            }
            else if (Input.GetAxis("Mouse X") < 0)
            {
                rotate = new Quaternion(rotate.x * 1.1f, rotate.y, rotate.z, 0);
                obj.transform.rotation = rotate;
            }
        }
    }
    private void RotateY()
    {
        if (Input.GetMouseButton(0) && GUIUtility.hotControl == 0)
        {
            Debug.Log("click rotatey");
            Quaternion rotate = obj.transform.rotation;
            if (Input.GetAxis("Mouse Y") > 0)
            {
                rotate = new Quaternion(rotate.x, rotate.y * 0.9f, rotate.z, 0);
                obj.transform.rotation = rotate;
            }
            else if (Input.GetAxis("Mouse Y") < 0)
            {
                rotate = new Quaternion(rotate.x, rotate.y * 1.1f, rotate.z, 0);
                obj.transform.rotation = rotate;
            }
        }
        else if (Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0)
        {
            Quaternion rotate = obj.transform.rotation;
            if (Input.GetAxis("Mouse Y") > 0)
            {
                rotate = new Quaternion(rotate.x, rotate.y * 0.9f, rotate.z, 0);
                obj.transform.rotation = rotate;
            }
            else if (Input.GetAxis("Mouse Y") < 0)
            {
                rotate = new Quaternion(rotate.x, rotate.y * 1.1f, rotate.z, 0);
                obj.transform.rotation = rotate;
            }
        }
    }
    private void RotateZ()
    {
        if (Input.GetMouseButton(0) && GUIUtility.hotControl == 0)
        {
            Quaternion rotate = obj.transform.rotation;
            if (Input.GetAxis("Mouse X") > 0)
            {
                rotate = new Quaternion(rotate.x, rotate.y, rotate.z * 0.9f, 0);
                obj.transform.rotation = rotate;
            }
            else if (Input.GetAxis("Mouse X") < 0)
            {
                rotate = new Quaternion(rotate.x, rotate.y, rotate.z * 1.1f, 0);
                obj.transform.rotation = rotate;
            }
        }
        else if (Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0)
        {
            Quaternion rotate = obj.transform.rotation;
            if (Input.GetAxis("Mouse X") > 0)
            {
                rotate = new Quaternion(rotate.x, rotate.y, rotate.z * 0.9f, 0);
                obj.transform.rotation = rotate;
            }
            else if (Input.GetAxis("Mouse X") < 0)
            {
                rotate = new Quaternion(rotate.x, rotate.y, rotate.z * 1.1f, 0);
                obj.transform.rotation = rotate;
            }
        }
    }
}
