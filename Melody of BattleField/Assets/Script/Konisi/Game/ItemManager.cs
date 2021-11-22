using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public enum Item
    {
        HEAL,
        POWER_UP,
        TRAP
    }

	Dictionary<Item, int> itemDictionary = new Dictionary<Item, int>();

	// Use this for initialization
	void Start()
	{
		itemDictionary[Item.HEAL] = 0;
		itemDictionary[Item.POWER_UP] = 1;
		itemDictionary[Item.TRAP] = 2;

		foreach (var item in itemDictionary)
		{
			Debug.Log(item.Key + " : " + GetNum(item.Key));
		}
	}

	//　アイテムをいくつ持っているかどうか
	public int GetNum(Item key)
	{
		return itemDictionary[key];
	}
}
