using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTool : MonoBehaviour
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
                if (hit.transform.name == X.name)
                    ScaleX();
                else if (hit.transform.name == Y.name)
                    ScaleY();
                else if (hit.transform.name == Z.name)
                    ScaleZ();
                else if (hit.transform.name == obj.name)
                    Debug.Log(obj.name);
            }
        }
    }
    private void ScaleX()
    {
        if (Input.GetMouseButton(0) && GUIUtility.hotControl == 0)
        {
            Vector3 scale = obj.transform.localScale;
            if (Input.GetAxis("Mouse X") > 0)
            {
                scale = new Vector3(scale.x * 0.9f, scale.y, scale.z);
                obj.transform.localScale = scale;
            }
            else if (Input.GetAxis("Mouse X") < 0)
            {
                scale = new Vector3(scale.x * 1.1f, scale.y, scale.z);
                obj.transform.localScale = scale;
            }
        }
        else if (Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0)
        {
            Vector3 scale = obj.transform.localScale;
            if (Input.GetAxis("Mouse X") > 0)
            {
                scale = new Vector3(scale.x * 0.9f, scale.y, scale.z);
                obj.transform.localScale = scale;
            }
            else if (Input.GetAxis("Mouse X") < 0)
            {
                scale = new Vector3(scale.x * 1.1f, scale.y, scale.z);
                obj.transform.localScale = scale;
            }
        }
    }
    private void ScaleY()
    {
        if (Input.GetMouseButton(0) && GUIUtility.hotControl == 0)
        {
            Vector3 scale = obj.transform.localScale;
            if (Input.GetAxis("Mouse Y") > 0)
            {
                scale = new Vector3(scale.x, scale.y * 0.9f, scale.z);
                obj.transform.localScale = scale;
            }
            else if (Input.GetAxis("Mouse Y") < 0)
            {
                scale = new Vector3(scale.x, scale.y * 1.1f, scale.z);
                obj.transform.localScale = scale;
            }
        }
        else if (Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0)
        {
            Vector3 scale = obj.transform.localScale;
            if (Input.GetAxis("Mouse Y") > 0)
            {
                scale = new Vector3(scale.x, scale.y * 0.9f, scale.z);
                obj.transform.localScale = scale;
            }
            else if (Input.GetAxis("Mouse Y") < 0)
            {
                scale = new Vector3(scale.x, scale.y * 1.1f, scale.z);
                obj.transform.localScale = scale;
            }
        }
    }
    private void ScaleZ()
    {
        if (Input.GetMouseButton(0) && GUIUtility.hotControl == 0)
        {
            Vector3 scale = obj.transform.localScale;
            if (Input.GetAxis("Mouse X") > 0)
            {
                scale = new Vector3(scale.x, scale.y, scale.z * 0.9f);
                obj.transform.localScale = scale;
            }
            else if (Input.GetAxis("Mouse X") < 0)
            {
                scale = new Vector3(scale.x, scale.y, scale.z * 1.1f);
                obj.transform.localScale = scale;
            }
        }
        else if (Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0)
        {
            Vector3 scale = obj.transform.localScale;
            if (Input.GetAxis("Mouse X") > 0)
            {
                scale = new Vector3(scale.x, scale.y, scale.z * 0.9f);
                obj.transform.localScale = scale;
            }
            else if (Input.GetAxis("Mouse X") < 0)
            {
                scale = new Vector3(scale.x, scale.y, scale.z * 1.1f);
                obj.transform.localScale = scale;
            }
        }
    }
}
