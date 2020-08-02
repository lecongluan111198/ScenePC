using UnityEngine;
using System.Collections;

public class ObjectRotation : MonoBehaviour {

	public float planetSpeedRotation = 1.0f;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (!MRDataHolder.Instance.IsEdit)
        {
            transform.Rotate(-Vector3.up * Time.deltaTime * planetSpeedRotation * 85.1f);
        }
        
	}
}
