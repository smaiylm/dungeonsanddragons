using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemClass : MonoBehaviour {
	//create possible items
	public ItemCreatorClass swordItem = new ItemCreatorClass(0,"Sword", "DESTORY!!");
	public ItemCreatorClass arrowItem = new ItemCreatorClass (1, "Bow and Arrow", "Cool");
	public ItemCreatorClass breadItem = new ItemCreatorClass (2, "Bread", "Fiber");
	public ItemCreatorClass Excalibur = new ItemCreatorClass (3, "Excalibur", "Sword");
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//item class constructor
	public class ItemCreatorClass
	{
		public int id;
		public string name;

		public string description;

		public ItemCreatorClass(int ide, string namee, string descriptione)
		{
			id=ide;
			name=namee;

			description=descriptione;
		}

	}
}
