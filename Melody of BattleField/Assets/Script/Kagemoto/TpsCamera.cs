//--カメラのTPS追従スクリプト--
// プレイヤーへの追従
// マウス移動での視点回転

//--Unity側操作
// 空オブジェクトにスクリプトを設定
// スクリプトに追従するプレイヤーを設定
// メインカメラを空オブジェクトの子に設定

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsCamera : MonoBehaviour
{
    // 入力値(Inspectorで設定)
    private GameObject Player;  // 追従対象
    [SerializeField] float RotateSpeed; // 回転速度

    float side, forward;

    private void Start()
    {
        // 回転速度
        RotateSpeed = 1;




        //追従するオブジェクトを選択

        //ギターをプレイヤーとして追従
        Player = GameObject.Find("Guitar");

        //ギターがインスタンス化されていなければキーボードに追従
        if (Player == null) 
        {
            Player = GameObject.Find("Keyboard");
        }
    }

    void Update()
    {
        // プレイヤー位置を追従する
        transform.position = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);

        // マウスによる回転
        side += Input.GetAxis("Mouse X") * RotateSpeed; // 横回転入力
        forward -= Input.GetAxis("Mouse Y") * RotateSpeed; // 縦回転入力

        forward = Mathf.Clamp(forward, -80, 60); //縦回転の角度制限

        transform.eulerAngles = new Vector3(forward, side, 0.0f); // 回転の実行
    }
}
