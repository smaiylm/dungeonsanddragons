using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class medkit : MonoBehaviour {
    public GameObject medical;
    // Use this for initialization
    void Start()
    {
		//spawn med kit randomly
		float x = Random.Range (-14.0f, 14.0f);
		float y = 1.5f;
		float z = Random.Range (-14.0f, 14.0f);
       
		Vector3 pos = new Vector3 (x,y,z);
		transform.position = pos;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
