using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {
	private Rect inventoryWindowRect = new Rect(300,100,400,400);
	static public bool inventoryWindowShow = false;
	//create a dictionary for the loot chest
	private Dictionary<int,string> lootDictionary = new Dictionary<int,string>()
	{
		{0, string.Empty},
		{1, string.Empty},
		{2, string.Empty},
		{3, string.Empty},
		{4, string.Empty},
		{5, string.Empty},
	};
	//display how much of one item there is
	private List<int> lootAmounts = new List<int>()
	{
		0,
		0,
		0,
		0,
		0,
		0
	};
	itemClass itemObject = new itemClass();
	//detecting mouse click
	private Ray mouseRay;
	private RaycastHit rayHit;
	// Use this for initialization
	void Start () {
		//spawn random loot
		lootDictionary [0] = lootRandomizer();
		lootAmounts [0] = amountRandomizer();
		lootDictionary [1] = lootRandomizer();
		lootAmounts [1] = amountRandomizer();
		lootDictionary [2] = lootRandomizer();
		lootAmounts [2] = amountRandomizer();
	}
	
	// Update is called once per frame
	void Update () {
		mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Input.GetButtonDown ("Fire1"))
		{
			
			Physics.Raycast (mouseRay, out rayHit);
			//if clicked on chest
			if (rayHit.collider.transform.name == "Chest" && !InventoryGUI.inventoryWindowToggle)
			{
				inventoryWindowShow = true;
				//InventoryGUI.inventoryWindowToggle = false;
				//Inventory_Level_2.inventoryWindowToggle = false;
			}
		

		}
		//close chest window when l is pressed
		if(Input.GetKeyDown("l"))
		{
			inventoryWindowShow = false;

		}
	}
	void OnGUI()
	{
		//displays chest window
		if(inventoryWindowShow)
		{
			inventoryWindowRect = GUI.Window (0, inventoryWindowRect, InventoryWindowMethod, "Chest");
		}

	}
	//generate loot window
	void InventoryWindowMethod(int windowId)
	{
		GUILayout.BeginArea (new Rect(0,50,400,400));

		GUILayout.BeginHorizontal ();
		int y1 = 0;
		int y2 = 0;
		int y3 = 0;
		bool bool1 = true;
		bool bool2 = true;
		bool bool3 = true;
		//first item
		//create a button
		if (GUILayout.Button(lootDictionary[0], GUILayout.Height(50)))
		{
			
			
			if(lootDictionary[0]!= string.Empty && lootAmounts[0] != 0)
			{
				for(int x = 0;x<9; x++)
				{
					if (InventoryGUI.inventoryNameDictionary[x] == lootDictionary[0])
					{
						y1 = x;
						bool1 = false;
						break;
					}

				}
				if (bool1)
				{
					for(int x = 0;x<9; x++)
					{
						if (InventoryGUI.inventoryNameDictionary[x] == string.Empty)
						{
							y1 = x;
						
							break;
						}

					}
				}
				InventoryGUI.inventoryNameDictionary [y1] = lootDictionary [0];
				if (lootAmounts[0] != 0) {
					lootAmounts[0] -= 1;
					InventoryGUI.dictionaryAmounts [y1] += 1;
				}
			}
			if (lootAmounts[0] == 0)
			{
				lootDictionary [0] = string.Empty;
			}
		}
		GUILayout.Box (lootAmounts[0].ToString(), GUILayout.Height(50));




		//second item


	




		if (GUILayout.Button(lootDictionary[1], GUILayout.Height(50)))
		{
			
			if(lootDictionary[1]!= string.Empty && lootAmounts[1] != 0)
			{
				for(int x = 0;x<9; x++)
				{
					if (InventoryGUI.inventoryNameDictionary[x] == lootDictionary[1])
					{
						y2 = x;
						bool2 = false;
						break;
					}

				}
				if (bool2)
				{
					for(int x = 0;x<9; x++)
					{
						if (InventoryGUI.inventoryNameDictionary[x] == string.Empty)
						{
							y2 = x;

							break;
						}

					}
				}
				InventoryGUI.inventoryNameDictionary [y2] = lootDictionary [1];
				if (lootAmounts[1] != 0) {
					lootAmounts[1] -= 1;
					InventoryGUI.dictionaryAmounts [y2] += 1;
				}
			}
			if (lootAmounts[1] == 0)
			{
				lootDictionary [1] = string.Empty;
			}
		}
		GUILayout.Box (lootAmounts[1].ToString(), GUILayout.Height(50));



		//third item






		if (GUILayout.Button(lootDictionary[2], GUILayout.Height(50)))
		{
			if(lootDictionary[2]!= string.Empty && lootAmounts[2] != 0)
			{
				for(int x = 0;x<9; x++)
				{
					if (InventoryGUI.inventoryNameDictionary[x] == lootDictionary[2])
					{
						y3 = x;
						bool3 = false;
						break;
					}

				}
				if (bool3)
				{
					for(int x = 0;x<9; x++)
					{
						if (InventoryGUI.inventoryNameDictionary[x] == string.Empty)
						{
							y3 = x;

							break;
						}

					}
				}
				InventoryGUI.inventoryNameDictionary [y3] = lootDictionary [2];
				if (lootAmounts[2] != 0) {
					lootAmounts[2] -= 1;
					InventoryGUI.dictionaryAmounts [y3] += 1;
				}
			}
			if (lootAmounts[2] == 0)
			{
				lootDictionary [2] = string.Empty;
			}
		}
		GUILayout.Box (lootAmounts[2].ToString(), GUILayout.Height(50));
		GUILayout.EndHorizontal ();
		GUILayout.EndArea ();
	}
	//randomizes loot and how much loot
	public string lootRandomizer()
	{
		itemClass items = new itemClass ();
		string returnString = string.Empty;
		int randomNumber = Random.Range (0,4);
		switch (randomNumber)
		{
		//if case 0, then item is a sword
		case 0:
			returnString = items.swordItem.name;
			break;
			//if case 1 then arrow
		case 1:
			returnString = items.arrowItem.name;
			break;
			//if case 2 then bread
		case 2:
			returnString = items.breadItem.name;
			break;
			//if case 3 then excalibur
		case 3:
			returnString = items.Excalibur.name;
			break;
		}

		return returnString;
	}
	//randomizes how much loot is generated
	public int amountRandomizer()
	{
		int returnAmount = 0;
		returnAmount = Random.Range (1,3);
		return returnAmount;
	}

}
