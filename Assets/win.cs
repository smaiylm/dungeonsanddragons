using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class win : MonoBehaviour {

	// Use this for initialization
	private void OnGUI()
	{
		// you lose scene
		//play again
		if (GUI.Button (new Rect (15, 15, 200, 110), "Play Again")) {
			
			Application.LoadLevel ("dungeonsAndDragons");
		}
	}
}
