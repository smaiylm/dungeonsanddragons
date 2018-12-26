using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}





    /*
    void onClick()
    {

    }
    */



    void OnMouseDown()
    {
		//play music
        this.gameObject.AddComponent<AudioSource>();
        this.GetComponent<AudioSource>().Play();
    }
}
