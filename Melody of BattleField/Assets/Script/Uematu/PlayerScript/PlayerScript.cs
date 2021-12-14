using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // 移動速度
    [SerializeField]
    private float speed = 5.0f;

    // 移動量
    private Vector3 move;
    private float moveX = 0.0f;
    private float moveZ = 0.0f;

    // カメラ回転速度
    [SerializeField] private float applySpeed = 0.1f;

    // カメラ参照
    [SerializeField] private TpsCamera refCamera;

    //Rigidbody
    private Rigidbody rbody;

    // レイ
    private Ray ray;
    private float rayDistance = 0.5f; // レイの長さ
    private RaycastHit hit;           // レイにヒットした物の情報
    private Vector3 rayPosition;      // レイの発射位置
    private bool IsGround;            // 下方向にオブジェクトがるかどうかの判定


    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody取得
        rbody = this.transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        HitCheckGround();

        MovePlayer();

    }

    void MovePlayer()
    {
        // プレイヤーのZ方向を
        // 移動の方向(velocity)に徐々に回転
        if (rbody.velocity != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation,
            //Quaternion.LookRotation(rbody.velocity),
            //Y移動量を無視
            Quaternion.LookRotation(Vector3.Scale(rbody.velocity, new Vector3(1, 0, 1)).normalized),
            applySpeed);
        // 移動量の計算(キー入力 * 移動速度)
        moveX = Input.GetAxis("Horizontal") * speed; // 左右
        moveZ = Input.GetAxis("Vertical") * speed; // 前後
                                                   // カメラの方向(カメラが上を向いている場合に備え高さ成分を取り除く)
        Vector3 cameraForward = Vector3.Scale(refCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(refCamera.transform.right, new Vector3(1, 0, 1)).normalized;
        // カメラの方向を基準に移動方向を決定
        move = cameraForward * moveZ + cameraRight * moveX;
        // 移動量を加える
        rbody.velocity = move;
    }

    void HitCheckGround()
    {
        if (Physics.Raycast(ray, out hit, rayDistance)) // レイが当たった時の処理
        {
            IsGround = true; // 地面に触れたことにする
        }
        else
        {
            IsGround = false; // 地面に触れてないことにする
        }
        // 地面に触れていたら
        if (IsGround)
        {
            rbody.useGravity = false; //　重力をオフ
        }
        else
        {
            // 重力をオン
            rbody.useGravity = true;
        }
    }
}
