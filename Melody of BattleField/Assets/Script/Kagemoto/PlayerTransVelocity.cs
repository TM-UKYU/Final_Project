//--プレイヤーの移動スクリプト--
// 左右移動・ジャンプ(Rigidbody.velocity)
// 移動方向への回転(Quaternion.Slerp)
// 地面との当たり判定(RayCast)

//--Unity側操作
// スクリプトにTPSカメラを設定

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransVelocity : MonoBehaviour
{
    private Rigidbody rbody;

    // 移動速度
    private float speed = 5.0f;

    // 移動量
    private Vector3 move;
    private float moveX = 0.0f;
    private float moveZ = 0.0f;

    // ジャンプ
    private float moveY = 1.0f;  // ジャンプの初速
    private bool IsGround = true;// 地面判定
    private bool isJump = false; // ジャンプ判定
    private bool maxJumpFlag = false; // 最高速に達したか
    private int offJumpClock = 0; // 最高速時間のカウンタ

    private bool IsStep; // ステップ中かどうか
    private float time;

    // レイ
    private Ray ray;
    private float rayDistance = 0.5f; // レイの長さ
    private RaycastHit hit;           // レイにヒットした物の情報
    private Vector3 rayPosition;      // レイの発射位置

    // 回転速度
    [SerializeField] private float applySpeed = 0.1f;

    // カメラ参照用変数(Inspectorで参照するカメラを指定する)
    [SerializeField] private TpsCamera refCamera;
    [SerializeField] private Vector3 transforward;
   

    void Start()
    {
        // Rigitbodyの取得
        rbody = GetComponent<Rigidbody>();
    }

    void Update()
    {

        transforward = transform.forward;

        // 移動方向に回転
        {
            // プレイヤーの回転(transform.rotation)
            // プレイヤーのZ方向を
            // 移動の方向(velocity)に徐々に回転
            if (rbody.velocity != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      //Quaternion.LookRotation(rbody.velocity),
                                                      //Y移動量を無視
                                                      Quaternion.LookRotation(Vector3.Scale(rbody.velocity, new Vector3(1, 0, 1)).normalized),
                                                      applySpeed);

        }

        // カメラ方向に回転
        {
            // プレイヤーの回転(transform.rotation)
            // プレイヤーのZ方向を
            // カメラの水平回転方向(refCamera.transform.rotation)に徐々に回転
            /*
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(refCamera.transform.rotation * rbody.velocity),
                                                  applySpeed);
            */
        }

        // カメラの方向に回転
        //transform.eulerAngles = refCamera.transform.eulerAngles;
        if (Input.GetKeyDown("left shift"))
        {
            IsStep = true;
        }
        
        if (IsStep)
        {
            speed = 30.0f;
           
            time+= Time.deltaTime;
            if (time > 0.5f) {
                IsStep = false;
            }
        }
        else
        {
            speed = 5.0f;
            time = 0.0f;
        }
      
        

        // 移動量の計算(キー入力 * 移動速度)
        moveX = Input.GetAxis("Horizontal") * speed; // 左右
        moveZ = Input.GetAxis("Vertical") * speed;   // 前後

        // カメラの方向(カメラが上を向いている場合に備え高さ成分を取り除く)
        Vector3 cameraForward = Vector3.Scale(refCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(refCamera.transform.right, new Vector3(1, 0, 1)).normalized;

        // カメラの方向を基準に移動方向を決定
        move = cameraForward * moveZ + cameraRight * moveX;

        // レイの地面判定
        rayPosition = transform.position + new Vector3(0.0f, 0.5f, 0.0f); // レイの長さ分プレイヤ-座標から浮かせる
        ray = new Ray(rayPosition, transform.up * -1); //レイを下に発射
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red); //レイを赤色表示

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
            // ジャンプ変数関連
            {
                maxJumpFlag = false;
                moveY = 1.0f;
                offJumpClock = 0;
                isJump = false;
            }

            rbody.useGravity = false; //　重力をオフ
            // ジャンプ
            if (Input.GetKey(KeyCode.Space))
            {
                isJump = true;
                IsGround = false;// 地面判定オフ
            }
        }
        else
        {
            // 接地なし&ジャンプ中ではない
            if (!isJump)
            {
                // 重力をオン
                rbody.useGravity = true;
            }
        }

        // ジャンプフラグ
        if (isJump) { Jump(); }

        // 移動量を加える
        rbody.velocity = move;
    }

    // ジャンプ関数(仮)
    void Jump()
    {
        move = new Vector3(move.x, moveY, move.z);// 移動量にジャンプ力を加える
        if (maxJumpFlag == false)
        { // 最高速に達していないなら
            if (moveY <= 3.0f) // 最高速度以下なら
            {
                moveY += 0.1f; // 加速
            }
            else
            {
                maxJumpFlag = true;
            }
        }
        // 最高速に達したなら
        else
        {
            // カウント
            offJumpClock++;
            // 60フレーム経過したら
            if (offJumpClock > 60)
            {
                if (moveY >= 1.0f)
                { // 最低速度以上なら
                    moveY -= 0.05f; // 減速
                }
                else
                {
                    // 初速に戻ったらジャンプ終了
                    isJump = false;
                }
            }
        }
    }
}
