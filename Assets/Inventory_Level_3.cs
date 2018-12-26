using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Level_3 : MonoBehaviour
{
    static public bool inventoryWindowToggle = false;
    static public bool shopWindowToggle = false;
    private Rect inventoryWindowRect = new Rect(300, 100, 400, 400);
    private Rect shopWindowRect = new Rect(300, 100, 400, 400);
    public int attackValue = 1;
    private int enemyhealth = 10;
    private Ray mouseRay;
    private RaycastHit rayHit;
    public GameObject g;
    public TextMesh text;
    //load playerhealth
    private int playerhealth;
    private int enemyDamage = 5;
    public TextMesh enemyAttack;
    public TextMesh player;
    public TextMesh playerAttackValue;
    private bool swordActivated = false;
    private bool bowActivated = false;
    private bool breadActivated = false;
    private bool excaliburActivated = false;
    private int coin;
    public TextMesh moneyvalue;
    public float endtime = 0;
    private int run = 0;
    static public Dictionary<int, string> inventoryNameDictionary = new Dictionary<int, string>() {
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
    static public Dictionary<int, string> shopNameDictionary = new Dictionary<int, string>() {
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
    void Start()
    {
        inventoryNameDictionary = GlobalControl.Instance.inventoryNameDictionary;
        dictionaryAmounts = GlobalControl.Instance.dictionaryAmounts;
        playerhealth = GlobalControl.Instance.playerhealth;
        coin = GlobalControl.Instance.coin;
        shopcosts = GlobalControl.Instance.shopcosts;
        text.text = "";
        player.text = "Health: " + playerhealth.ToString();
        enemyAttack.text = "Attack: " + enemyDamage.ToString();
        playerAttackValue.text = "Your attack value is " + attackValue.ToString();
        //hint.text = "Press J if you are stuck and want a hint";
        moneyvalue.text = "Money: " + coin.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerhealth <= 0)
        {
            Application.LoadLevel("youlose");
        }
        if (swordActivated)
        {
            attackValue = 5;
        }
        if (bowActivated)
        {
            attackValue = 3;
            enemyDamage = 0;
        }
        if (breadActivated)
        {
            playerhealth = playerhealth + 5;
            breadActivated = false;
        }
        if (excaliburActivated)
        {
            attackValue = 10;
        }
        moneyvalue.text = "Money: " + coin.ToString();
        text.text = "Health: " + enemyhealth;
        player.text = "Health: " + playerhealth.ToString();
        enemyAttack.text = "Attack: " + enemyDamage.ToString();
        playerAttackValue.text = "Your attack value is " + attackValue.ToString();
        if (enemyhealth <= 0)
        {
            Destroy(g);
            text.text = "Congrats! You completed level 3.";
            run++;
            if (run < 2)
            {
                GameObject congrats = GameObject.Find("Congrats Sound");
                congrats.gameObject.AddComponent<AudioSource>();
                congrats.GetComponent<AudioSource>().Play();
            }
            enemyAttack.text = "";
            //save player
            savePlayer();
            // WaitForSeconds(3);
            endtime += Time.deltaTime;
            //  Application.LoadLevel("Level 2");
        }


        if (endtime > 5)
        {
            Application.LoadLevel("youwin");
        }

        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Input.GetButtonDown("Fire1"))
        {

            Physics.Raycast(mouseRay, out rayHit);
            if (rayHit.collider.transform.name == "Enemy" && !inventoryWindowToggle && !Loot.inventoryWindowShow)
            {
                enemyhealth = enemyhealth - getAttackValue();
                playerhealth = playerhealth - enemyDamage;



                //add coins?????

                coin = coin + 2;
                swordActivated = false;
                bowActivated = false;
                excaliburActivated = false;
                attackValue = 1;
                enemyDamage = 5;
                GameObject sound = GameObject.Find("Ouch Sound");
                sound.gameObject.AddComponent<AudioSource>();
                sound.GetComponent<AudioSource>().Play();
            }

        }

    }

    void OnCollisionEnter(Collision col)
    {
        /*
       if (enemyhealth <= 0)
        {
            if (col.gameObject.name == "hitmecube")
            {
                Destroy(col.gameObject);
            }
        }

        if (col.gameObject.name == "Medkit")
        {
            Destroy(col.gameObject);
            playerhealth = playerhealth + 5;
        }
        */

        if(col.gameObject.name == "Enemy")
        {
            playerhealth = 0;
        }



    }
    void OnGUI()
    {
        inventoryWindowToggle = GUI.Toggle(new Rect(800, 50, 100, 50), inventoryWindowToggle, "Inventory");
        shopWindowToggle = GUI.Toggle(new Rect(150, 50, 100, 50), shopWindowToggle, "Shop");
        if (inventoryWindowToggle)
        {

            inventoryWindowRect = GUI.Window(0, inventoryWindowRect, InventoryWindowMethod, "Inventory");
            Loot.inventoryWindowShow = false;
        }
        else if (shopWindowToggle)
        {

            shopWindowRect = GUI.Window(1, shopWindowRect, shopWindowMethod, "Shop");

        }
    }


    void shopWindowMethod(int windowID)
    {

        GUILayout.BeginArea(new Rect(0, 50, 400, 400));

        GUILayout.BeginHorizontal();
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
        if (GUILayout.Button(shopNameDictionary[0], GUILayout.Height(50)))
        {
            if (shopNameDictionary[0] != string.Empty && coin >= shopcosts[0])
            {
                for (int x = 0; x < 9; x++)
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
                    for (int x = 0; x < 9; x++)
                    {
                        if (inventoryNameDictionary[x] == string.Empty)
                        {
                            y1 = x;

                            break;
                        }

                    }
                }


                inventoryNameDictionary[y1] = shopNameDictionary[0];
                dictionaryAmounts[y1] += 1;
                coin = coin - shopcosts[0];
            }
            //do something with coin here

        }
        GUILayout.Box("Cost: " + shopcosts[0].ToString(), GUILayout.Height(50));

        if (GUILayout.Button(shopNameDictionary[1], GUILayout.Height(50)))
        {
            if (shopNameDictionary[1] != string.Empty && coin >= shopcosts[1])
            {
                for (int x = 0; x < 9; x++)
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
                    for (int x = 0; x < 9; x++)
                    {
                        if (inventoryNameDictionary[x] == string.Empty)
                        {
                            y2 = x;

                            break;
                        }

                    }
                }
                inventoryNameDictionary[y2] = shopNameDictionary[1];
                dictionaryAmounts[y2] += 1;
                coin = coin - shopcosts[1];
            }
        }
        GUILayout.Box("Cost: " + shopcosts[1].ToString(), GUILayout.Height(50));
        if (GUILayout.Button(shopNameDictionary[2], GUILayout.Height(50)))
        {
            if (shopNameDictionary[2] != string.Empty && coin >= shopcosts[2])
            {
                for (int x = 0; x < 9; x++)
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
                    for (int x = 0; x < 9; x++)
                    {
                        if (inventoryNameDictionary[x] == string.Empty)
                        {
                            y3 = x;

                            break;
                        }

                    }
                }
                inventoryNameDictionary[y3] = shopNameDictionary[2];
                dictionaryAmounts[y3] += 1;
                coin = coin - shopcosts[2];
            }

        }
        GUILayout.Box("Cost: " + shopcosts[2].ToString(), GUILayout.Height(50));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(shopNameDictionary[3], GUILayout.Height(50)))
        {
            if (shopNameDictionary[3] != string.Empty && coin >= shopcosts[3])
            {
                for (int x = 0; x < 9; x++)
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
                    for (int x = 0; x < 9; x++)
                    {
                        if (inventoryNameDictionary[x] == string.Empty)
                        {
                            y4 = x;

                            break;
                        }

                    }
                }
                inventoryNameDictionary[y4] = shopNameDictionary[3];
                dictionaryAmounts[y4] += 1;
                coin = coin - shopcosts[3];
            }
        }
        GUILayout.Box("Cost: " + shopcosts[3].ToString(), GUILayout.Height(50));
        if (GUILayout.Button(shopNameDictionary[4], GUILayout.Height(50)))
        {
            if (shopNameDictionary[4] != string.Empty && coin >= shopcosts[4])
            {
                for (int x = 0; x < 9; x++)
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
                    for (int x = 0; x < 9; x++)
                    {
                        if (inventoryNameDictionary[x] == string.Empty)
                        {
                            y5 = x;

                            break;
                        }

                    }
                }
                inventoryNameDictionary[y5] = shopNameDictionary[4];
                dictionaryAmounts[y5] += 1;
                coin = coin - shopcosts[4];
            }
        }
        GUILayout.Box("Cost: " + shopcosts[4].ToString(), GUILayout.Height(50));
        if (GUILayout.Button(shopNameDictionary[5], GUILayout.Height(50)))
        {
            if (shopNameDictionary[5] != string.Empty && coin >= shopcosts[5])
            {
                for (int x = 0; x < 9; x++)
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
                    for (int x = 0; x < 9; x++)
                    {
                        if (inventoryNameDictionary[x] == string.Empty)
                        {
                            y6 = x;

                            break;
                        }

                    }
                }
                inventoryNameDictionary[y6] = shopNameDictionary[5];
                dictionaryAmounts[y6] += 1;
                coin = coin - shopcosts[5];
            }
        }
        GUILayout.Box("Cost: " + shopcosts[5].ToString(), GUILayout.Height(50));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(shopNameDictionary[6], GUILayout.Height(50)))
        {
            if (shopNameDictionary[6] != string.Empty && coin >= shopcosts[6])
            {
                for (int x = 0; x < 9; x++)
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
                    for (int x = 0; x < 9; x++)
                    {
                        if (inventoryNameDictionary[x] == string.Empty)
                        {
                            y7 = x;

                            break;
                        }

                    }
                }
                inventoryNameDictionary[y7] = shopNameDictionary[6];
                dictionaryAmounts[y7] += 1;
                coin = coin - shopcosts[6];
            }
        }
        GUILayout.Box("Cost: " + shopcosts[6].ToString(), GUILayout.Height(50));
        if (GUILayout.Button(shopNameDictionary[7], GUILayout.Height(50)))
        {
            if (shopNameDictionary[7] != string.Empty && coin >= shopcosts[7])
            {
                for (int x = 0; x < 9; x++)
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
                    for (int x = 0; x < 9; x++)
                    {
                        if (inventoryNameDictionary[x] == string.Empty)
                        {
                            y8 = x;

                            break;
                        }

                    }
                }
                inventoryNameDictionary[y8] = shopNameDictionary[7];
                dictionaryAmounts[y8] += 1;
                coin = coin - shopcosts[7];
            }
        }
        GUILayout.Box("Cost: " + shopcosts[7].ToString(), GUILayout.Height(50));
        if (GUILayout.Button(shopNameDictionary[8], GUILayout.Height(50)))
        {
            if (shopNameDictionary[8] != string.Empty && coin >= shopcosts[8])
            {
                for (int x = 0; x < 9; x++)
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
                    for (int x = 0; x < 9; x++)
                    {
                        if (inventoryNameDictionary[x] == string.Empty)
                        {
                            y9 = x;

                            break;
                        }

                    }
                }
                inventoryNameDictionary[y9] = shopNameDictionary[8];
                dictionaryAmounts[y9] += 1;
                coin = coin - shopcosts[8];
            }
        }
        GUILayout.Box("Cost: " + shopcosts[8].ToString(), GUILayout.Height(50));
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
        moneyvalue.text = "Money: " + coin.ToString();
    }







    void InventoryWindowMethod(int windowID)
    {
        GUILayout.BeginArea(new Rect(0, 50, 400, 400));

        GUILayout.BeginHorizontal();
        //First row
        if (GUILayout.Button(inventoryNameDictionary[0], GUILayout.Height(50)))
        {
            if (inventoryNameDictionary[0] != string.Empty && dictionaryAmounts[0] != 0)
            {
                if (inventoryNameDictionary[0] == "Sword")
                {
                    swordActivated = true;
                }
                if (inventoryNameDictionary[0] == "Bow and Arrow")
                {
                    bowActivated = true;
                }
                if (inventoryNameDictionary[0] == "Bread")
                {
                    breadActivated = true;
                }
                if (inventoryNameDictionary[0] == "Excalibur")
                {
                    excaliburActivated = true;
                }
                if (dictionaryAmounts[0] != 0)
                {
                    dictionaryAmounts[0] -= 1;
                }

            }
            if (dictionaryAmounts[0] == 0)
            {
                inventoryNameDictionary[0] = string.Empty;
            }

        }


        GUILayout.Box(dictionaryAmounts[0].ToString(), GUILayout.Height(50));


        if (GUILayout.Button(inventoryNameDictionary[1], GUILayout.Height(50)))
        {
            if (inventoryNameDictionary[1] != string.Empty && dictionaryAmounts[1] != 0)
            {
                //do something here
                if (inventoryNameDictionary[1] == "Sword")
                {
                    swordActivated = true;
                }
                if (inventoryNameDictionary[1] == "Bow and Arrow")
                {
                    bowActivated = true;
                }
                if (inventoryNameDictionary[1] == "Bread")
                {
                    breadActivated = true;
                }
                if (inventoryNameDictionary[1] == "Excalibur")
                {
                    excaliburActivated = true;
                }
                if (dictionaryAmounts[1] != 0)
                {
                    dictionaryAmounts[1] -= 1;
                }

            }
            if (dictionaryAmounts[1] == 0)
            {
                inventoryNameDictionary[1] = string.Empty;
            }


        }

        GUILayout.Box(dictionaryAmounts[1].ToString(), GUILayout.Height(50));

        if (GUILayout.Button(inventoryNameDictionary[2], GUILayout.Height(50)))
        {
            if (inventoryNameDictionary[2] != string.Empty && dictionaryAmounts[2] != 0)
            {
                //do something here
                if (inventoryNameDictionary[2] == "Sword")
                {
                    swordActivated = true;
                }
                if (inventoryNameDictionary[2] == "Bow and Arrow")
                {
                    bowActivated = true;
                }
                if (inventoryNameDictionary[2] == "Bread")
                {
                    breadActivated = true;
                }
                if (inventoryNameDictionary[2] == "Excalibur")
                {
                    excaliburActivated = true;
                }
                if (dictionaryAmounts[2] != 0)
                {
                    dictionaryAmounts[2] -= 1;
                }

            }
            if (dictionaryAmounts[2] == 0)
            {
                inventoryNameDictionary[2] = string.Empty;
            }
        }
        GUILayout.Box(dictionaryAmounts[2].ToString(), GUILayout.Height(50));

        GUILayout.EndHorizontal();
        //second row
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(inventoryNameDictionary[3], GUILayout.Height(50)))
        {
            if (inventoryNameDictionary[3] != string.Empty && dictionaryAmounts[3] != 0)
            {
                //do something here
                if (inventoryNameDictionary[3] == "Sword")
                {
                    swordActivated = true;
                }
                if (inventoryNameDictionary[3] == "Bow and Arrow")
                {
                    bowActivated = true;
                }
                if (inventoryNameDictionary[3] == "Bread")
                {
                    breadActivated = true;
                }
                if (inventoryNameDictionary[3] == "Excalibur")
                {
                    excaliburActivated = true;
                }
                if (dictionaryAmounts[3] != 0)
                {
                    dictionaryAmounts[3] -= 1;
                }

            }
            if (dictionaryAmounts[3] == 0)
            {
                inventoryNameDictionary[3] = string.Empty;
            }


        }
        GUILayout.Box(dictionaryAmounts[3].ToString(), GUILayout.Height(50));

        if (GUILayout.Button(inventoryNameDictionary[4], GUILayout.Height(50)))
        {
            if (inventoryNameDictionary[4] != string.Empty && dictionaryAmounts[4] != 0)
            {
                //do something here
                if (inventoryNameDictionary[4] == "Sword")
                {
                    swordActivated = true;
                }
                if (inventoryNameDictionary[4] == "Bow and Arrow")
                {
                    bowActivated = true;
                }
                if (inventoryNameDictionary[4] == "Bread")
                {
                    breadActivated = true;
                }
                if (inventoryNameDictionary[4] == "Excalibur")
                {
                    excaliburActivated = true;
                }
                if (dictionaryAmounts[4] != 0)
                {
                    dictionaryAmounts[4] -= 1;
                }

            }
            if (dictionaryAmounts[4] == 0)
            {
                inventoryNameDictionary[4] = string.Empty;
            }

        }
        GUILayout.Box(dictionaryAmounts[4].ToString(), GUILayout.Height(50));

        if (GUILayout.Button(inventoryNameDictionary[5], GUILayout.Height(50)))
        {
            if (inventoryNameDictionary[5] != string.Empty && dictionaryAmounts[5] != 0)
            {
                //do something here
                if (inventoryNameDictionary[5] == "Sword")
                {
                    swordActivated = true;
                }
                if (inventoryNameDictionary[5] == "Bow and Arrow")
                {
                    bowActivated = true;
                }
                if (inventoryNameDictionary[5] == "Bread")
                {
                    breadActivated = true;
                }
                if (inventoryNameDictionary[5] == "Excalibur")
                {
                    excaliburActivated = true;
                }
                if (dictionaryAmounts[5] != 0)
                {
                    dictionaryAmounts[5] -= 1;
                }

            }
            if (dictionaryAmounts[5] == 0)
            {
                inventoryNameDictionary[5] = string.Empty;
            }
        }
        GUILayout.Box(dictionaryAmounts[5].ToString(), GUILayout.Height(50));

        GUILayout.EndHorizontal();
        //third row
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(inventoryNameDictionary[6], GUILayout.Height(50)))
        {
            if (inventoryNameDictionary[6] != string.Empty && dictionaryAmounts[6] != 0)
            {
                //do something here
                if (inventoryNameDictionary[6] == "Sword")
                {
                    swordActivated = true;
                }
                if (inventoryNameDictionary[6] == "Bow and Arrow")
                {
                    bowActivated = true;
                }
                if (inventoryNameDictionary[6] == "Bread")
                {
                    breadActivated = true;
                }
                if (inventoryNameDictionary[6] == "Excalibur")
                {
                    excaliburActivated = true;
                }
                if (dictionaryAmounts[6] != 0)
                {
                    dictionaryAmounts[6] -= 1;
                }

            }
            if (dictionaryAmounts[6] == 0)
            {
                inventoryNameDictionary[6] = string.Empty;
            }
        }
        GUILayout.Box(dictionaryAmounts[6].ToString(), GUILayout.Height(50));

        if (GUILayout.Button(inventoryNameDictionary[7], GUILayout.Height(50)))
        {
            if (inventoryNameDictionary[7] != string.Empty && dictionaryAmounts[7] != 0)
            {
                //do something here
                if (inventoryNameDictionary[7] == "Sword")
                {
                    swordActivated = true;
                }
                if (inventoryNameDictionary[7] == "Bow and Arrow")
                {
                    bowActivated = true;
                }
                if (inventoryNameDictionary[7] == "Bread")
                {
                    breadActivated = true;
                }
                if (inventoryNameDictionary[7] == "Excalibur")
                {
                    excaliburActivated = true;
                }
                if (dictionaryAmounts[7] != 0)
                {
                    dictionaryAmounts[7] -= 1;
                }

            }
            if (dictionaryAmounts[7] == 0)
            {
                inventoryNameDictionary[7] = string.Empty;
            }
        }
        GUILayout.Box(dictionaryAmounts[7].ToString(), GUILayout.Height(50));

        if (GUILayout.Button(inventoryNameDictionary[8], GUILayout.Height(50)))
        {
            if (inventoryNameDictionary[8] != string.Empty && dictionaryAmounts[8] != 0)
            {
                //do something here
                if (inventoryNameDictionary[8] == "Sword")
                {
                    swordActivated = true;
                }
                if (inventoryNameDictionary[8] == "Bow and Arrow")
                {
                    bowActivated = true;
                }
                if (inventoryNameDictionary[8] == "Bread")
                {
                    breadActivated = true;
                }
                if (inventoryNameDictionary[8] == "Excalibur")
                {
                    excaliburActivated = true;
                }
                if (dictionaryAmounts[8] != 0)
                {
                    dictionaryAmounts[8] -= 1;
                }

            }
            if (dictionaryAmounts[8] == 0)
            {
                inventoryNameDictionary[8] = string.Empty;
            }
        }
        GUILayout.Box(dictionaryAmounts[8].ToString(), GUILayout.Height(50));

        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }
    public int getAttackValue()
    {

        return attackValue;
    }
    public void savePlayer()
    {
        //load everything to global control (save data)
        GlobalControl.Instance.inventoryNameDictionary = inventoryNameDictionary;
        GlobalControl.Instance.dictionaryAmounts = dictionaryAmounts;
        GlobalControl.Instance.playerhealth = playerhealth;
        GlobalControl.Instance.coin = coin;
    }
}
