using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lose : MonoBehaviour {
	static public List<int> temp = new List<int>()
	{
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0
	};
	static public Dictionary<int,string> temp1=new Dictionary<int,string> () {
		//29 characters
		{0,string.Empty},
		{1,string.Empty},
		{2,string.Empty},
		{3,string.Empty},
		{4,string.Empty},
		{5,string.Empty},
		{6,string.Empty},
		{7,string.Empty},
		{8,string.Empty}

	};
	// Use this for initialization
	private void OnGUI()
	{
		// you lose scene
		//play again
		if (GUI.Button (new Rect (15, 15, 200, 110), "Play Again")) {
			PlayerPrefs.DeleteAll ();   
			Application.LoadLevel ("dungeonsAndDragons");

		}
	}
}
