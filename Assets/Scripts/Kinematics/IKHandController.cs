using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandController : MonoBehaviour
{
    public bool isRight = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isRight)
        {
            if (RightController.Instance != null && RightController.Instance.enabled)
            {
                transform.position = RightController.Instance.transform.position;
                transform.rotation = RightController.Instance.transform.rotation;
            }
        }
        else
        {
            //if (RightController.Instance != null && RightController.Instance.enabled)
            //{
            //    transform.position = RightController.Instance.transform.position;
            //    transform.rotation = RightController.Instance.transform.rotation;
            //}
        }
    }
}
