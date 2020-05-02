using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenu : MonoBehaviour
{
	public GameObject panel;
	public GameObject go;
	public ActionManager action;

	private Transform myTransform;
	private Vector3 destinationPosition;
	private float destinationDistance;
	public float moveSpeed;

	Vector3 minScale = new Vector3(0.1f, 0.1f, 0.1f);
	Vector3 maxScale = new Vector3(3, 3, 3);

	void Start()
	{
		myTransform = go.transform;
		destinationPosition = myTransform.position;
	}

	// Update is called once per frame
	void Update()
	{
		if (panel.active == true)
		{
			if (action.IsMove)
			{
				Move();
			}
			else if (action.IsScale)
			{
				Scale();
			}
		}
	}
	//Turn on panel when click object
	private void OnMouseDown()
	{
		panel.SetActive(!panel.active);
	}
	private void Move()
	{
		destinationDistance = Vector3.Distance(destinationPosition, myTransform.position);

		if (destinationDistance < 0.5f)
		{
			moveSpeed = 0;
		}
		else if (destinationDistance > 0.5f)
		{
			moveSpeed = 3;
		}
		// Moves the Player if the Left Mouse Button was clicked
		if (Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0)
		{

			Plane playerPlane = new Plane(Vector3.up, myTransform.position);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float hitdist = 0.0f;

			if (playerPlane.Raycast(ray, out hitdist))
			{
				Vector3 targetPoint = ray.GetPoint(hitdist);
				destinationPosition = ray.GetPoint(hitdist);
				Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
				myTransform.rotation = targetRotation;
			}
		}

		// Moves the player if the mouse button is hold down
		else if (Input.GetMouseButton(0) && GUIUtility.hotControl == 0)
		{

			Plane playerPlane = new Plane(Vector3.up, myTransform.position);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float hitdist = 0.0f;

			if (playerPlane.Raycast(ray, out hitdist))
			{
				Vector3 targetPoint = ray.GetPoint(hitdist);
				destinationPosition = ray.GetPoint(hitdist);
				Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
				myTransform.rotation = targetRotation;
			}
		}

		if (destinationDistance > .5f)
		{
			myTransform.position = Vector3.MoveTowards(myTransform.position, destinationPosition, moveSpeed * Time.deltaTime);
		}
	}
	private void Scale()
	{
		//float zoomValue = Input.GetAxis("Mouse ScrollWheel");
		//Debug.Log(zoomValue);
		//if (zoomValue != 0)
		//{
		//	transform.localScale += Vector3.one * zoomValue;
		//	transform.localScale = Vector3.Max(transform.localScale, minScale);
		//	transform.localScale = Vector3.Min(transform.localScale, maxScale);
		//}
		Vector3 scale = go.transform.localScale;
		if (Input.GetKey(KeyCode.Z))
		{
			scale = scale * 0.9f;
		}
		if (Input.GetKey(KeyCode.X))
		{
			scale = scale * 1.1f;
		}
		go.transform.localScale = scale;
	}
}
