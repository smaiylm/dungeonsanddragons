using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// global control allows to save data
public class GlobalControl : MonoBehaviour {
	public static GlobalControl Instance;
	// we want to save player health, inventory, and money
	public Dictionary<int,string> inventoryNameDictionary;
	public List<int> dictionaryAmounts;
	public int playerhealth;
	public int coin;
	public List<int> shopcosts;
	// Use this for initialization
	void Awake ()   
	{
		if (Instance == null)
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy (gameObject);
		}
	}
}
