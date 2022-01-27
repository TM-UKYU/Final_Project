using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public enum Item
    {
        POWER_UP,
        HEAL,
        TRAP
    }

	Dictionary<Item, int> itemDictionary = new Dictionary<Item, int>();

	// Use this for initialization
	void Start()
	{
		itemDictionary[Item.HEAL] = 2;
		itemDictionary[Item.POWER_UP] = 2;
		itemDictionary[Item.TRAP] = 2;

		foreach (var item in itemDictionary)
		{
			Debug.Log(item.Key + " : " + GetItemNum(item.Key));
		}
	}

	//�@�A�C�e�������������Ă��邩�ǂ���
	public int GetItemNum(Item key)
	{
		return itemDictionary[key];
	}

	// �A�C�e��������
	public void ConsumptionItem(Item key)
    {
		itemDictionary[key]--;
    }
}
