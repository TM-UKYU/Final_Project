using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelodySystem : MonoBehaviour
{
    private bool KeyManage;

    private List<Spell> MelodyList = new List<Spell>();

    // Update is called once per frame
    void Update()
    {
        if (MelodyList.Count > 3)
        {
            MelodyList.RemoveAt(0);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!KeyManage)
            {
                MelodyList.Add(Spell.Deel);
                Debug.Log("ディール");
                KeyManage = true;
            }
        }
        else
        {
            KeyManage = false;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (!KeyManage)
            {
                MelodyList.Add(Spell.Srea);
                Debug.Log("スリア");
                KeyManage = true;
            }
        }
        else
        {
            KeyManage = false;
        }

        if (Input.GetKey(KeyCode.Return))
        {
            if(MelodyList.Count<2)
            {
                Debug.Log("詠唱が足りません");
                return;
            }
            if (MelodyList[0] == Spell.Deel)
            {
                if (MelodyList[1] == Spell.Deel)
                {
                    if (MelodyList[2] == Spell.Deel)
                    {
                        Debug.Log("炎の魔法");
                    }
                    else
                    {
                        Debug.Log("氷の魔法");
                    }
                }
                else
                {
                    Debug.Log("雷の魔法");
                }
            }
            else
            {
                Debug.Log("風の魔法");
            }
            MelodyList.Clear();
        }
    }
}
