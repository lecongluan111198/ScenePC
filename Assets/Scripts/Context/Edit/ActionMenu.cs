using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenu : MonoBehaviour
{
	public GameObject myObj;

	public GameObject panel;
	public GameObject arrow;
	public GameObject scale;
	public GameObject rotate;
	public ActionManager action;
	public UserManual userManual;

	private Transform myTransform;
	private Vector3 destinationPosition;
	private float destinationDistance;
	public float moveSpeed;

	void Start()
	{
		myTransform = myObj.transform;
		destinationPosition = myTransform.position;
	}

	// Update is called once per frame
	void Update()
	{
		
	}
	//Turn on panel when click object
	private void OnMouseDown()
	{
		EditPanelManager.instance.updateCurrObject(gameObject);
		EditPanelManager.instance.setActive(true);
	}
}
