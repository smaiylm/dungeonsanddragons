using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGUI : MonoBehaviour {
	//bollean for opening inventory
	static public bool inventoryWindowToggle = false;
	//boolean for opening shop
	static public bool shopWindowToggle = false;
	//constructs a rectangle for inventory
	private Rect inventoryWindowRect = new Rect(300,100,400,400);
	//constructs a rectangle for shop
	private Rect shopWindowRect = new Rect (300,100,400,400);
	//player attack value
	public int attackValue = 1;
	//enemy health
	private int enemyhealth = 10;
	//clicking stuff
	private Ray mouseRay;
	private RaycastHit rayHit;
	public GameObject g;
	//enemy health text box
	public TextMesh text;
	//load playerhealth
	private int playerhealth = 10;
	//enemy attack value
	private int enemyDamage = 1;
	//enemy attack text box
	public TextMesh enemyAttack;
	//player health text box
	public TextMesh player;
	//player attack value text box
	public TextMesh playerAttackValue;
	//booleans to activate items
	private bool swordActivated = false;
	private bool bowActivated = false;
	private bool breadActivated = false;
	private bool excaliburActivated = false;
	//money value
	private int coin = 0;
	//money text box
	public TextMesh moneyvalue;
	public float endtime = 0;
	private int run = 0;
	//dictionary for inventory
	static public Dictionary<int,string> inventoryNameDictionary=new Dictionary<int,string> () {
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
	//dictionary for shop
	static public Dictionary<int,string> shopNameDictionary=new Dictionary<int,string> () {
		//29 characters
		{0,"Sword"},
		{1,"Bread"},
		{2,"Bow and Arrow"},
		{3,"Excalibur"},
		{4,string.Empty},
		{5,string.Empty},
		{6,string.Empty},
		{7,string.Empty},
		{8,string.Empty}

	};
	//list that shows how much of items one has in inventory
	static public List<int> dictionaryAmounts = new List<int>()
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
	//list that shows shop costs
	static public List<int> shopcosts = new List<int>()
	{
		8,
		8,
		10,
		20,
		0,
		0,
		0,
		0,
		0
	};
	itemClass itemObject = new itemClass();

	// Use this for initialization
	void Start () {
		//set all text values
		moneyvalue.text = "Money: " + coin.ToString();
		text.text = "";
		player.text = "Health: " + playerhealth.ToString();
		enemyAttack.text = "Attack: " + enemyDamage.ToString ();
		playerAttackValue.text = "Your attack value is " + attackValue.ToString ();
	}

	// Update is called once per frame
	void Update () {
		//losing condition
		if (playerhealth <=0)
		{
			Application.LoadLevel ("youlose");
		}
		//sword specialty
		if (swordActivated)
		{
			attackValue = 5;
		}
		//bow specialty
		if (bowActivated)
		{
			attackValue = 3;
			enemyDamage = 0;
		}
		//bread specialty
		if (breadActivated)
		{
			playerhealth = playerhealth + 5;
			breadActivated = false;
		}
		//excalibur speciality
		if (excaliburActivated)
		{
			attackValue = 10;
		}
		//update texts
		moneyvalue.text = "Money: " + coin.ToString();
		text.text = "Health: " + enemyhealth;
		player.text = "Health: " + playerhealth.ToString();
		enemyAttack.text = "Attack: " + enemyDamage.ToString ();
		playerAttackValue.text = "Your attack value is " + attackValue.ToString ();
		//killing enemy
		if (enemyhealth<=0 && playerhealth>0)
		{
			Destroy (g);
			//load next scene
			text.text = "Congrats! You completed level 1. Loading Level 2...";
			run++;
			//play winning music
			if (run<2) {
				GameObject congrats = GameObject.Find ("Congrats Sound");
				congrats.gameObject.AddComponent<AudioSource> ();
				congrats.GetComponent<AudioSource> ().Play ();
			}
			enemyAttack.text = "";
			//save player

			//load everything to global control (save data)
			GlobalControl.Instance.inventoryNameDictionary = inventoryNameDictionary;
			GlobalControl.Instance.dictionaryAmounts = dictionaryAmounts;
			GlobalControl.Instance.playerhealth = playerhealth;
			GlobalControl.Instance.coin = coin;
			GlobalControl.Instance.shopcosts = shopcosts;
			// WaitForSeconds(3);
			endtime += Time.deltaTime;
			//  Application.LoadLevel("Level 2");
		}

		//wait for 5 seconds, then load next level
		if(endtime > 5)
		{
			Application.LoadLevel("Level 2");
		}
	
		mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		//detecting clicking
		if (Input.GetButtonDown ("Fire1"))
		{

			Physics.Raycast (mouseRay, out rayHit);
			//detecting clicking on enemy
			if (rayHit.collider.transform.name == "Enemy" && !inventoryWindowToggle && !Loot.inventoryWindowShow)
			{
				//deal damage to enemy and recoil damage to player
				enemyhealth=enemyhealth - getAttackValue();
				playerhealth = playerhealth - enemyDamage;



				//add coins

				coin = coin + 2;
				//reset booleans and attack values
				swordActivated = false;
				bowActivated = false;
				excaliburActivated = false;
				attackValue = 1;
				enemyDamage = 1;
				//play damage sound
				GameObject sound = GameObject.Find("Ouch Sound");
				sound.gameObject.AddComponent<AudioSource>();
				sound.GetComponent<AudioSource>().Play();
			}

		}

	}
	//detect collision and increase coin (not working)
	 void OnCollisionEnter(Collision col)
	{

		if(col.gameObject.name == "M" )
		{
			coin= coin + 20;
		
			Destroy(col.gameObject);

		}

	}

	void OnGUI()
	{
		//display inventory
		inventoryWindowToggle = GUI.Toggle (new Rect(800,50,100,50), inventoryWindowToggle, "Inventory");
		//display shop
		shopWindowToggle = GUI.Toggle (new Rect(150,50,100,50), shopWindowToggle, "Shop");
		if (inventoryWindowToggle)
		{
			//display inventory
			inventoryWindowRect = GUI.Window (0, inventoryWindowRect, InventoryWindowMethod, "Inventory");
			Loot.inventoryWindowShow = false;
		}
		else if (shopWindowToggle)
		{
			//display shop
			shopWindowRect = GUI.Window (1, shopWindowRect, shopWindowMethod, "Shop");

		}
	}

	//method to display shop window
	void shopWindowMethod(int windowID)
	{

		GUILayout.BeginArea (new Rect(0,50,400,400));

		GUILayout.BeginHorizontal ();
		int y1 = 0;
		int y2 = 0;
		int y3 = 0;
		int y4 = 0;
		int y5 = 0;
		int y6 = 0;
		int y7 = 0;
		int y8 = 0;
		int y9 = 0;
		bool bool1 = true;
		bool bool2 = true;
		bool bool3 = true;
		bool bool4 = true;
		bool bool5 = true;
		bool bool6 = true;
		bool bool7 = true;
		bool bool8 = true;
		bool bool9 = true;
		//create a button
		//first box
		if (GUILayout.Button (shopNameDictionary [0], GUILayout.Height (50))) {
			if(shopNameDictionary[0] != string.Empty && coin >= shopcosts[0])
			{
				//get position for item to be placed in inventory
				for(int x = 0;x<9; x++)
				{
					if (inventoryNameDictionary[x] == shopNameDictionary[0])
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
						if (inventoryNameDictionary[x] == string.Empty)
						{
							y1 = x;

							break;
						}

					}
				}

				//places item in inventory
				inventoryNameDictionary [y1] = shopNameDictionary [0];
				//add amount of item
				dictionaryAmounts [y1] += 1;
				//subtract money
				coin = coin - shopcosts[0];
			}
		

		}
		GUILayout.Box ("Cost: " + shopcosts[0].ToString(), GUILayout.Height(50));
		//second box
		if (GUILayout.Button (shopNameDictionary [1], GUILayout.Height (50))) {
			if (shopNameDictionary [1] != string.Empty && coin >= shopcosts [1]) {
				for(int x = 0;x<9; x++)
				{
					if (inventoryNameDictionary[x] == shopNameDictionary[1])
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
						if (inventoryNameDictionary[x] == string.Empty)
						{
							y2 = x;

							break;
						}

					}
				}
				inventoryNameDictionary [y2] = shopNameDictionary [1];
				dictionaryAmounts [y2] += 1;
				coin = coin - shopcosts[1];
			}
		}
		GUILayout.Box ("Cost: " + shopcosts[1].ToString(), GUILayout.Height(50));
		//third box
		if (GUILayout.Button (shopNameDictionary [2], GUILayout.Height (50))) {
			if (shopNameDictionary [2] != string.Empty && coin >= shopcosts [2]) {
				for(int x = 0;x<9; x++)
				{
					if (inventoryNameDictionary[x] == shopNameDictionary[2])
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
						if (inventoryNameDictionary[x] == string.Empty)
						{
							y3 = x;

							break;
						}

					}
				}
				inventoryNameDictionary [y3] = shopNameDictionary [2];
				dictionaryAmounts [y3] += 1;
				coin = coin - shopcosts[2];
			}

		}
		GUILayout.Box ("Cost: " + shopcosts[2].ToString(), GUILayout.Height(50));
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		//fourth box
		if (GUILayout.Button (shopNameDictionary [3], GUILayout.Height (50))) {
			if (shopNameDictionary [3] != string.Empty && coin >= shopcosts [3]) {
				for(int x = 0;x<9; x++)
				{
					if (inventoryNameDictionary[x] == shopNameDictionary[3])
					{
						y4 = x;
						bool4 = false;
						break;
					}

				}
				if (bool4)
				{
					for(int x = 0;x<9; x++)
					{
						if (inventoryNameDictionary[x] == string.Empty)
						{
							y4 = x;

							break;
						}

					}
				}
				inventoryNameDictionary [y4] = shopNameDictionary [3];
				dictionaryAmounts [y4] += 1;
				coin = coin - shopcosts[3];
			}
		}
		GUILayout.Box ("Cost: " + shopcosts[3].ToString(), GUILayout.Height(50));
		//fifth box
		if (GUILayout.Button (shopNameDictionary [4], GUILayout.Height (50))) {
			if (shopNameDictionary [4] != string.Empty && coin >= shopcosts [4]) {
				for(int x = 0;x<9; x++)
				{
					if (inventoryNameDictionary[x] == shopNameDictionary[4])
					{
						y5 = x;
						bool5 = false;
						break;
					}

				}
				if (bool5)
				{
					for(int x = 0;x<9; x++)
					{
						if (inventoryNameDictionary[x] == string.Empty)
						{
							y5 = x;

							break;
						}

					}
				}
				inventoryNameDictionary [y5] = shopNameDictionary [4];
				dictionaryAmounts [y5] += 1;
				coin = coin - shopcosts[4];
			}
		}
		GUILayout.Box ("Cost: " + shopcosts[4].ToString(), GUILayout.Height(50));
		//sixth box
		if (GUILayout.Button (shopNameDictionary [5], GUILayout.Height (50))) {
			if (shopNameDictionary [5] != string.Empty && coin >= shopcosts [5]) {
				for(int x = 0;x<9; x++)
				{
					if (inventoryNameDictionary[x] == shopNameDictionary[5])
					{
						y6 = x;
						bool6 = false;
						break;
					}

				}
				if (bool6)
				{
					for(int x = 0;x<9; x++)
					{
						if (inventoryNameDictionary[x] == string.Empty)
						{
							y6 = x;

							break;
						}

					}
				}
				inventoryNameDictionary [y6] = shopNameDictionary [5];
				dictionaryAmounts [y6] += 1;
				coin = coin - shopcosts[5];
			}
		}
		GUILayout.Box ("Cost: " + shopcosts[5].ToString(), GUILayout.Height(50));
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		//seventh box
		if (GUILayout.Button (shopNameDictionary [6], GUILayout.Height (50))) {
			if (shopNameDictionary [6] != string.Empty && coin >= shopcosts [6]) {
				for(int x = 0;x<9; x++)
				{
					if (inventoryNameDictionary[x] == shopNameDictionary[6])
					{
						y7 = x;
						bool7 = false;
						break;
					}

				}
				if (bool7)
				{
					for(int x = 0;x<9; x++)
					{
						if (inventoryNameDictionary[x] == string.Empty)
						{
							y7 = x;

							break;
						}

					}
				}
				inventoryNameDictionary [y7] = shopNameDictionary [6];
				dictionaryAmounts [y7] += 1;
				coin = coin - shopcosts[6];
			}
		}
		GUILayout.Box ("Cost: " + shopcosts[6].ToString(), GUILayout.Height(50));
		//eighth box
		if (GUILayout.Button (shopNameDictionary [7], GUILayout.Height (50))) {
			if (shopNameDictionary [7] != string.Empty && coin >= shopcosts [7]) {
				for(int x = 0;x<9; x++)
				{
					if (inventoryNameDictionary[x] == shopNameDictionary[7])
					{
						y8 = x;
						bool8 = false;
						break;
					}

				}
				if (bool8)
				{
					for(int x = 0;x<9; x++)
					{
						if (inventoryNameDictionary[x] == string.Empty)
						{
							y8 = x;

							break;
						}

					}
				}
				inventoryNameDictionary [y8] = shopNameDictionary [7];
				dictionaryAmounts [y8] += 1;
				coin = coin - shopcosts[7];
			}
		}
		GUILayout.Box ("Cost: " + shopcosts[7].ToString(), GUILayout.Height(50));
		//ninth box
		if (GUILayout.Button (shopNameDictionary [8], GUILayout.Height (50))) {
			if (shopNameDictionary [8] != string.Empty && coin >= shopcosts [8]) {
				for(int x = 0;x<9; x++)
				{
					if (inventoryNameDictionary[x] == shopNameDictionary[8])
					{
						y9 = x;
						bool9 = false;
						break;
					}

				}
				if (bool9)
				{
					for(int x = 0;x<9; x++)
					{
						if (inventoryNameDictionary[x] == string.Empty)
						{
							y9 = x;

							break;
						}

					}
				}
				inventoryNameDictionary [y9] = shopNameDictionary [8];
				dictionaryAmounts [y9] += 1;
				coin = coin - shopcosts[8];
			}
		}
		GUILayout.Box ("Cost: " + shopcosts[8].ToString(), GUILayout.Height(50));
		GUILayout.EndHorizontal ();
		GUILayout.EndArea ();
	}






	//method for displaying inventory
	void InventoryWindowMethod(int windowID)
	{
		GUILayout.BeginArea (new Rect(0,50,400,400));

		GUILayout.BeginHorizontal ();
		//First row
		//create a button
		//first box
		if (GUILayout.Button (inventoryNameDictionary [0], GUILayout.Height (50))) {
			if (inventoryNameDictionary[0] != string.Empty && dictionaryAmounts[0] !=0)
			{
				//activate items
				if(inventoryNameDictionary[0] == "Sword")
				{
					swordActivated = true;
				}
				if(inventoryNameDictionary[0] == "Bow and Arrow")
				{
					bowActivated = true;
				}
				if(inventoryNameDictionary[0] == "Bread")
				{
					breadActivated = true;
				}
				if(inventoryNameDictionary[0] == "Excalibur")
				{
					excaliburActivated = true;
				}
				//subtract items from inventory
				if (dictionaryAmounts[0]!=0)
				{
					dictionaryAmounts [0]-=1;
				}

			}
			if (dictionaryAmounts[0] == 0)
			{
				inventoryNameDictionary [0] = string.Empty;
			}

		}


		GUILayout.Box (dictionaryAmounts[0].ToString(), GUILayout.Height(50));

		//second box
		if (GUILayout.Button (inventoryNameDictionary [1], GUILayout.Height (50))) {
			if (inventoryNameDictionary[1] != string.Empty && dictionaryAmounts[1] !=0)
			{
				//do something here
				if(inventoryNameDictionary[1] == "Sword")
				{
					swordActivated = true;
				}
				if(inventoryNameDictionary[1] == "Bow and Arrow")
				{
					bowActivated = true;
				}
				if(inventoryNameDictionary[1] == "Bread")
				{
					breadActivated = true;
				}
				if(inventoryNameDictionary[1] == "Excalibur")
				{
					excaliburActivated = true;
				}
				if (dictionaryAmounts[1]!=0)
				{
					dictionaryAmounts [1]-=1;
				}

			}
			if (dictionaryAmounts[1] == 0)
			{
				inventoryNameDictionary [1] = string.Empty;
			}


		}

		GUILayout.Box (dictionaryAmounts[1].ToString(), GUILayout.Height(50));
		//third box
		if (GUILayout.Button (inventoryNameDictionary [2], GUILayout.Height (50))) {
			if (inventoryNameDictionary[2] != string.Empty && dictionaryAmounts[2] !=0)
			{
				//do something here
				if(inventoryNameDictionary[2] == "Sword")
				{
					swordActivated = true;
				}
				if(inventoryNameDictionary[2] == "Bow and Arrow")
				{
					bowActivated = true;
				}
				if(inventoryNameDictionary[2] == "Bread")
				{
					breadActivated = true;
				}
				if(inventoryNameDictionary[2] == "Excalibur")
				{
					excaliburActivated = true;
				}
				if (dictionaryAmounts[2]!=0)
				{
					dictionaryAmounts [2]-=1;
				}

			}
			if (dictionaryAmounts[2] == 0)
			{
				inventoryNameDictionary [2] = string.Empty;
			}
		}
		GUILayout.Box (dictionaryAmounts[2].ToString(), GUILayout.Height(50));

		GUILayout.EndHorizontal ();
		//second row
		GUILayout.BeginHorizontal ();
		//fourth box
		if (GUILayout.Button (inventoryNameDictionary [3], GUILayout.Height (50))) {
			if (inventoryNameDictionary[3] != string.Empty && dictionaryAmounts[3] !=0)
			{
				//do something here
				if(inventoryNameDictionary[3] == "Sword")
				{
					swordActivated = true;
				}
				if(inventoryNameDictionary[3] == "Bow and Arrow")
				{
					bowActivated = true;
				}
				if(inventoryNameDictionary[3] == "Bread")
				{
					breadActivated = true;
				}
				if(inventoryNameDictionary[3] == "Excalibur")
				{
					excaliburActivated = true;
				}
				if (dictionaryAmounts[3]!=0)
				{
					dictionaryAmounts [3]-=1;
				}

			}
			if (dictionaryAmounts[3] == 0)
			{
				inventoryNameDictionary [3] = string.Empty;
			}


		}
		GUILayout.Box (dictionaryAmounts[3].ToString(), GUILayout.Height(50));
		//fifth box
		if (GUILayout.Button (inventoryNameDictionary [4], GUILayout.Height (50))) {
			if (inventoryNameDictionary[4] != string.Empty && dictionaryAmounts[4] !=0)
			{
				//do something here
				if(inventoryNameDictionary[4] == "Sword")
				{
					swordActivated = true;
				}
				if(inventoryNameDictionary[4] == "Bow and Arrow")
				{
					bowActivated = true;
				}
				if(inventoryNameDictionary[4] == "Bread")
				{
					breadActivated = true;
				}
				if(inventoryNameDictionary[4] == "Excalibur")
				{
					excaliburActivated = true;
				}
				if (dictionaryAmounts[4]!=0)
				{
					dictionaryAmounts [4]-=1;
				}

			}
			if (dictionaryAmounts[4] == 0)
			{
				inventoryNameDictionary [4] = string.Empty;
			}

		}
		GUILayout.Box (dictionaryAmounts[4].ToString(), GUILayout.Height(50));
		//sixth box
		if (GUILayout.Button (inventoryNameDictionary [5], GUILayout.Height (50))) {
			if (inventoryNameDictionary[5] != string.Empty && dictionaryAmounts[5] !=0)
			{
				//do something here
				if(inventoryNameDictionary[5] == "Sword")
				{
					swordActivated = true;
				}
				if(inventoryNameDictionary[5] == "Bow and Arrow")
				{
					bowActivated = true;
				}
				if(inventoryNameDictionary[5] == "Bread")
				{
					breadActivated = true;
				}
				if(inventoryNameDictionary[5] == "Excalibur")
				{
					excaliburActivated = true;
				}
				if (dictionaryAmounts[5]!=0)
				{
					dictionaryAmounts [5]-=1;
				}

			}
			if (dictionaryAmounts[5] == 0)
			{
				inventoryNameDictionary [5] = string.Empty;
			}
		}
		GUILayout.Box (dictionaryAmounts[5].ToString(), GUILayout.Height(50));

		GUILayout.EndHorizontal ();
		//third row
		GUILayout.BeginHorizontal ();
		//seventh box
		if (GUILayout.Button (inventoryNameDictionary [6], GUILayout.Height (50))) {
			if (inventoryNameDictionary[6] != string.Empty && dictionaryAmounts[6] !=0)
			{
				//do something here
				if(inventoryNameDictionary[6] == "Sword")
				{
					swordActivated = true;
				}
				if(inventoryNameDictionary[6] == "Bow and Arrow")
				{
					bowActivated = true;
				}
				if(inventoryNameDictionary[6] == "Bread")
				{
					breadActivated = true;
				}
				if(inventoryNameDictionary[6] == "Excalibur")
				{
					excaliburActivated = true;
				}
				if (dictionaryAmounts[6]!=0)
				{
					dictionaryAmounts [6]-=1;
				}

			}
			if (dictionaryAmounts[6] == 0)
			{
				inventoryNameDictionary [6] = string.Empty;
			}
		}
		GUILayout.Box (dictionaryAmounts[6].ToString(), GUILayout.Height(50));
		//eighth box
		if (GUILayout.Button (inventoryNameDictionary [7], GUILayout.Height (50))) {
			if (inventoryNameDictionary[7] != string.Empty && dictionaryAmounts[7] !=0)
			{
				//do something here
				if(inventoryNameDictionary[7] == "Sword")
				{
					swordActivated = true;
				}
				if(inventoryNameDictionary[7] == "Bow and Arrow")
				{
					bowActivated = true;
				}
				if(inventoryNameDictionary[7] == "Bread")
				{
					breadActivated = true;
				}
				if(inventoryNameDictionary[7] == "Excalibur")
				{
					excaliburActivated = true;
				}
				if (dictionaryAmounts[7]!=0)
				{
					dictionaryAmounts [7]-=1;
				}

			}
			if (dictionaryAmounts[7] == 0)
			{
				inventoryNameDictionary [7] = string.Empty;
			}
		}
		GUILayout.Box (dictionaryAmounts[7].ToString(), GUILayout.Height(50));
		//ninth box
		if (GUILayout.Button (inventoryNameDictionary [8], GUILayout.Height (50))) {
			if (inventoryNameDictionary[8] != string.Empty && dictionaryAmounts[8] !=0)
			{
				//do something here
				if(inventoryNameDictionary[8] == "Sword")
				{
					swordActivated = true;
				}
				if(inventoryNameDictionary[8] == "Bow and Arrow")
				{
					bowActivated = true;
				}
				if(inventoryNameDictionary[8] == "Bread")
				{
					breadActivated = true;
				}
				if(inventoryNameDictionary[8] == "Excalibur")
				{
					excaliburActivated = true;
				}
				if (dictionaryAmounts[8]!=0)
				{
					dictionaryAmounts [8]-=1;
				}

			}
			if (dictionaryAmounts[8] == 0)
			{
				inventoryNameDictionary [8] = string.Empty;
			}
		}
		GUILayout.Box (dictionaryAmounts[8].ToString(), GUILayout.Height(50));

		GUILayout.EndHorizontal ();

		GUILayout.EndArea ();
	}
	public int getAttackValue()
	{

		return attackValue;
	}

}
