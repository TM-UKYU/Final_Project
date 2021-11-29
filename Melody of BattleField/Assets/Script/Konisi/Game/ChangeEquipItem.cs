using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeEquipItem : MonoBehaviour
{
    public List<Image> itemList;

    public int centerArrayNum;

    // Start is called before the first frame update
    void Start()
    {
        // アイテムスライダーの真ん中以外非表示
        for(int i = 0; i < itemList.Count; i++)
        {
            if(i != itemList.Count / 2)
            {
                itemList[i].gameObject.SetActive(false);
            }
        }

        centerArrayNum = itemList.Count / 2;
    }

    // Update is called once per frame
    void Update()
    {
        // LCTRLが長押しされたら
        if (Input.GetKey(KeyCode.LeftControl))
        {
            // アイテムリストの真ん中と左右以外表示
            for (int i = 0; i < itemList.Count; i++)
            {
                if(centerArrayNum == 0)
                {
                    if (i == centerArrayNum ||
                        i == centerArrayNum + 1 ||
                        i == centerArrayNum - itemList.Count - 1)
                    {
                        itemList[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        itemList[i].gameObject.SetActive(false);
                    }
                }
                else if(centerArrayNum == itemList.Count)
                {
                    if (i == centerArrayNum ||
                        i == centerArrayNum + itemList.Count - 1||
                        i == centerArrayNum - 1)
                    {
                        itemList[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        itemList[i].gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (i == centerArrayNum ||
                        i == centerArrayNum + 1 ||
                        i == centerArrayNum - 1)
                    {
                        itemList[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        itemList[i].gameObject.SetActive(false);
                    }
                }
            }

            // 位置変換用
            Vector3 itemListFakePos;

            // マウスホイール検出
            float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
            if (mouseScrollWheel > 0.0f)
            {
                if (centerArrayNum != 0)
                {
                    // 左端記憶
                    itemListFakePos = itemList[0].transform.position;
                    // 位置入れ替え(右へ)
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        if (i != (itemList.Count - 1))
                        {
                            itemList[i].transform.position = itemList[i + 1].transform.position;
                        }
                    }
                    centerArrayNum--;

                    // 記憶した左端を右端へ
                    itemList[itemList.Count - 1].transform.position = itemListFakePos;
                }
            }
            else if (mouseScrollWheel < 0.0f)
            {
                if (centerArrayNum != itemList.Count - 1)
                {
                    // 右端記憶
                    itemListFakePos = itemList[itemList.Count - 1].transform.position;
                    // 位置入れ替え(左へ)
                    for (int i = itemList.Count - 1; i > 0; i--)
                    {
                        itemList[i].transform.position = itemList[i - 1].transform.position;
                    }
                    centerArrayNum++;

                    // 記憶した右端を左端へ
                    itemList[0].transform.position = itemListFakePos;
                }
            }
        }
        else
        {
            // アイテムリスト非表示
            for (int i = 0; i < itemList.Count; i++)
            {
                if(i == centerArrayNum)
                {
                    itemList[i].gameObject.SetActive(true);
                }
                else
                {
                    itemList[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
