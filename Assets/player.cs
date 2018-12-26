using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
	public Rigidbody rb;
	public float movementSpeed = 1.0f;
	public float yRotation = 2.0f;
	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		// move using wasd
		if (Input.GetKey ("d")) {

			rb.transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
			yRotation = yRotation + 2.0f;

		}
		if (Input.GetKey ("a")) {

			rb.transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
			yRotation = yRotation - 2.0f;
		}
		if (Input.GetKey ("w"))
		{
			transform.position += transform.forward * Time.deltaTime * movementSpeed;
		}
		if (Input.GetKey ("s")) {
			transform.position -= transform.forward * Time.deltaTime * movementSpeed;
		}

	}
}
