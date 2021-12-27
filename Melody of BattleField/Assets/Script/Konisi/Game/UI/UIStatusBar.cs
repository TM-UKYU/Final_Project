using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatusBar : MonoBehaviour
{
    // ステータス種類
    public enum STATUS_TYPE
    {
        HP,
        STAMINA
    };

    // 動かすバー
    public Slider slider;
    // プレイヤーオブジェクト
    private GameObject playerObject;
    // プレイヤー
    public Player player;
    // ステータス情報
    private PlayerStatus status;
    public STATUS_TYPE statusType;

    void Start()
    {
        playerObject = GameObject.Find("Keyboard");
        player = playerObject.GetComponent<Player>();
        // プレイヤーの中のステータス情報を取得
        status = player.GetComponent<PlayerStatus>();
    }

    void Update()
    {
        // ステータス情報をバーに反映
        switch (statusType)
        {
            case STATUS_TYPE.HP:
                slider.maxValue = status.MaxHp;
                slider.value = status.Hp;
                break;
            case STATUS_TYPE.STAMINA:
                slider.maxValue = status.MaxStamina;
                slider.value = status.Stamina;
                break;
            default:
                break;
        }
    }
}
