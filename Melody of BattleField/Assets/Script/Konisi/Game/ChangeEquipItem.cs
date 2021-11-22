using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeEquipItem : MonoBehaviour
{
    public Image itemListCenter;
    public Image itemListLeft;
    public Image itemListRight;

    public Vector3 centerPos;

    // Start is called before the first frame update
    void Start()
    {
        // アイテムリスト非表示
        itemListLeft.enabled = false;
        itemListRight.enabled = false;
        
        centerPos = itemListCenter.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // LCTRLが長押しされたら
        if (Input.GetKey(KeyCode.LeftControl))
        {
            // アイテムリスト表示
            itemListCenter.enabled = true;
            itemListLeft.enabled = true;
            itemListRight.enabled = true;

            // 位置変換用
            Vector3 itemListFakePos;

            // マウスホイール検出
            float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
            if (mouseScrollWheel > 0.0f)
            {
                // 位置入れ替え(右へ)
                itemListFakePos = itemListCenter.transform.position;
                itemListCenter.transform.position = itemListRight.transform.position;
                itemListRight.transform.position = itemListLeft.transform.position;
                itemListLeft.transform.position = itemListFakePos;
            }
            else if (mouseScrollWheel < 0.0f)
            {
                // 位置入れ替え(左へ)
                itemListFakePos = itemListCenter.transform.position;
                itemListCenter.transform.position = itemListLeft.transform.position;
                itemListLeft.transform.position = itemListRight.transform.position;
                itemListRight.transform.position = itemListFakePos;
            }
        }
        else
        {
            // アイテムリスト非表示
            if(centerPos == itemListCenter.transform.position)
            {
                itemListCenter.enabled = true;
                itemListLeft.enabled = false;
                itemListRight.enabled = false;
            }
            else if(centerPos == itemListLeft.transform.position)
            {
                itemListCenter.enabled = false;
                itemListLeft.enabled = true;
                itemListRight.enabled = false;
            }
            else if(centerPos == itemListRight.transform.position)
            {
                itemListCenter.enabled = false;
                itemListLeft.enabled = false;
                itemListRight.enabled = true;
            }
        }
    }
}
