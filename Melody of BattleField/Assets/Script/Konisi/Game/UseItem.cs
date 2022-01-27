using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    // プレイヤーオブジェクト
    private GameObject playerObject;
    // プレイヤー
    public Player player;
    // ステータス情報
    private PlayerStatus status;
    // 所持アイテム
    public ItemManager itemManager;
    // 装備中のアイテム取得用
    public ChangeEquipItem changeEquipItem;

    private void Start()
    {
        // 武器によって変更
        switch (StageInformation.Weapon)
        {
            case StageInformation.WEAPON_ID.KEYBOARD: playerObject = GameObject.Find("Keyboard"); break;
            case StageInformation.WEAPON_ID.GUITAR: playerObject = GameObject.Find("Guitar"); break;
            default: break;
        }

        player = playerObject.GetComponent<Player>();
        // プレイヤーの中のステータス情報を取得
        status = player.GetComponent<PlayerStatus>();
    }

    void Update()
    {
        // ステータスを変更
        if(Input.GetKeyDown(KeyCode.E))
        {
            switch(changeEquipItem.centerArrayNum)
            {
                case 0:
                    if (itemManager.GetItemNum(ItemManager.Item.HEAL) > 0)
                    {
                        if (status.Hp != status.MaxHp)
                        {
                            // Hp回復
                            status.Hp += 60;
                            // Hpが上限を超えないように
                            if(status.Hp >= status.MaxHp) { status.Hp = status.MaxHp; }
                            // アイテム消費
                            itemManager.ConsumptionItem(ItemManager.Item.HEAL);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
