using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    // オブジェクト・アセット関連
    [SerializeField] private GameObject Master;       // 発射元
    [SerializeField] private GameObject prefabNote;   // 音符
    [SerializeField] private GameObject prefabString; // 弦
    [SerializeField] private GameObject PrefabColEffect; // 衝突エフェクト
    [SerializeField] private AudioClip noteSE;        // 音符SE
    [SerializeField] private AudioClip noteSE2;        // 音符SE
    [SerializeField] private AudioClip noteSE3;        // 音符SE

    private int CountSE;
    [SerializeField] private AudioClip stringSE;     // 弦SE
    AudioSource audioSource;

    // 攻撃関連数値
    [SerializeField] private bool resetFlg = true; // ゲーム開始時に数値をリセットするか(デバッグ用)
    [SerializeField] private float betweenTime; // 発射間隔
    [SerializeField] private float shootPower;   // 発射速度
    [SerializeField] private float raiseY;      // 発射するオブジェクトを持ち上げる座標
    [SerializeField] private float moveZ;      // 発射するオブジェクトからZ方向に離す座標(プレイヤーではなく楽器の位置から飛ばす)
    [SerializeField] private float waveSpanTime; // 弦の時間間隔
    [SerializeField] private float waveSpanFar; // 弦の間隔  
    [SerializeField] private Vector3 GapFromOrigin; // 弦の原点(音符)からのズレ
    [SerializeField] private int waveNumMax;   // 衝撃波の弦上限数

    // デバッグ表示
    [SerializeField] private GameObject shot1;     // 1番目に発射した音符の参照
    [SerializeField] private GameObject shot2;     // 2番目に発射した音符の参照

    [SerializeField] private GameObject Setshot1;
    [SerializeField] private GameObject Setshot2;

    [SerializeField] private GameObject stringObj; // 弦の参照
    [SerializeField] private Vector3 pos1; //音符1の位置
    [SerializeField] private Vector3 pos2; //音符2の位置
    [SerializeField] private Vector3 temp;   // 音符1->音符2へのベクトル
    [SerializeField] private Vector3 normal; // 音符1->音符2への方向ベクトル(tempの正規化)
    [SerializeField] private Vector3 stringRot; // 弦モデルの回転

    // 攻撃関連内部値
    private bool canWave = false; // 衝撃波を飛ばせる状態か(弦のレイがあるか)
    private float nowTime = 0.0f; // 衝撃波の弦発生間隔カウンタ
    private int waveNum = 0;      // 衝撃波の弦数カウンタ

    NoteMove scnote1;
    NoteMove scnote2;

    private bool flg = false;
    private float stringTime = 0.0f;
    private float betweenRayTime = 0.0f;
    private float signedAngle = 0.0f;

    void Start()
    {
        CountSE = 1;

        //オーディオソースを取得
        audioSource = GetComponent<AudioSource>();

        if (resetFlg)
        {
            // 各種初期値設定
            betweenTime = 1.0f; // 最初は発射可能
            shootPower = 1.0f; // 初期速度
            //raiseY = 1.3f;
            moveZ = 1.0f;
            waveSpanTime = 0.4f;
            waveSpanFar = 0.5f;
            waveNumMax = 8;
        }
    }

    void Update()
    {
        //Note();
        betweenTime += Time.deltaTime; // 時間をカウント
        {
            // 前回の発射から1秒以上経過していたら
            if (betweenTime >= 1.0f)
            {
                // Vキーが押されていたら
                if (Input.GetKey(KeyCode.V))
                {
                    ////最大速度まで
                    //if (shootPower <= 10.0f)
                    //{
                    //    // 初期速度を加算
                    //    shootPower += 0.02f;
                    //}
                    ChargeNote();
                }

                // Vキーが離されたら
                if (Input.GetKeyUp(KeyCode.V))
                {
                    //audioSource.PlayOneShot(noteSE);

                    //GameObject noteObj = Instantiate(
                    //    prefabNote,// インスタンス化するプレハブ
                    //    Master.transform.position + new Vector3(0.0f, raiseY, 0.0f) + Master.transform.forward, // 発射元オブジェクトの座標より少し持ち上げる
                    //    Quaternion.Euler(90, 0, 0)
                    //    );

                    //// 音符の参照からスクリプトを取得
                    //NoteMove scNote = noteObj.GetComponent<NoteMove>();

                    //// 移動速度とプレイヤーの正面の向きをセット
                    //scNote.SetSpeed(shootPower);
                    //scNote.SetMasForward(transform.forward);


                    //// 音符の参照をセット
                    //if (shot1 == null) { shot1 = noteObj; }
                    //else if (shot1 != null && shot2 == null) { shot2 = noteObj; }
                    //else if (shot1 != null && shot2 != null)
                    //{
                    //    shot1 = shot2;
                    //    shot2 = noteObj;
                    //}


                    //// 前回発射からの時間リセット
                    //betweenTime = 0.0f;

                    //// 発射力リセット
                    //shootPower = 1.0f;

                    ShootNote();
                }
            }
        }

        // 最新の音符2つの座標確認用
        if (shot2 != null)
        {
            pos1 = shot2.transform.position;
        }
        if (shot1 != null)
        {
            pos2 = shot1.transform.position;
        }

        // 弦
        // Cキーが押されたら
        if (Input.GetKeyDown(KeyCode.C))
        {
            String();
            //// 有効な音符が2つ以上あるなら
            //if (shot1 && shot2)
            //{
            //    Setshot1 = shot1;
            //    Setshot2 = shot2;

            //    audioSource.PlayOneShot(stringSE);
            //    flg = true;

            //    // レイ部分に弦オブジェクトを生成
            //    stringObj = Instantiate(
            //   prefabString,// インスタンス化するプレハブ
            //   shot1.transform.position,
            //   Quaternion.Euler(0, 0, 0)
            //   );// 発射元オブジェクト

            //    // 弦(Z方向ベクトル)とレイのベクトルのY軸角度差を求める
            //    signedAngle = Vector3.SignedAngle(normal, Vector3.back, Vector3.up);

            //    // 求めた角度分弦を回転させる
            //    stringObj.transform.Rotate(0, -signedAngle, 0);

            //    // 弦の発生元の音符の参照から、スクリプトを取得
            //    scnote1 = shot1.GetComponent<NoteMove>();
            //    scnote2 = shot2.GetComponent<NoteMove>();

            //    // 有効な音符の生存時間を再設定
            //    scnote1.SetAliveTime(5);
            //    scnote2.SetAliveTime(5);

            //}
        }

        if (flg)
        {
            if (shot1 && shot2)
            {
                stringTime += Time.deltaTime;

                if (stringTime <= 5.0f)
                {
                    betweenRayTime += Time.deltaTime;
                    if (betweenRayTime >= 0.2f)
                    {
                        // ヒットオブジェクト
                        RaycastHit hit;

                        // 音符1と音符2の差分を求め
                        temp = shot2.transform.position - shot1.transform.position;
                        // 正規化して方向ベクトルを求める
                        normal = temp.normalized;

                        canWave = true;

                        // レイの可視化
                        Debug.DrawRay(shot1.transform.position, temp, Color.red, 5.0f);

                        // 弦(Z方向ベクトル)とレイのベクトルのY軸角度差を求める
                        signedAngle = Vector3.SignedAngle(normal, -stringObj.transform.forward, Vector3.up);

                        // 求めた角度分弦を回転させる
                        stringObj.transform.Rotate(0, -signedAngle, 0);

                        // 音符の差分のベクトル長を求める
                        float vecLen = temp.magnitude;

                        // ベクトル長を元に拡大率を設定
                        stringObj.transform.localScale = new Vector3(1.0f, 1.0f, vecLen);

                        stringObj.transform.position = shot1.transform.position;

                        // レイ処理
                        if (Physics.Raycast(shot1.transform.position, temp, out hit, 3))
                        {
                            // 敵ヒット
                            if (hit.collider.CompareTag("Enemy"))
                            {
                                GameObject hitObj = hit.collider.gameObject;
                                if (hit.collider.gameObject)
                                {
                                    GetPlayerAttack scEnemy = hitObj.GetComponent<GetPlayerAttack>();

                                    scEnemy.SetDamage(1);
                                }

                                // 衝突パーティクル生成
                                GameObject colEffectObj = Instantiate(
                                               PrefabColEffect,
                                               hitObj.transform.position,
                                               Quaternion.identity);
                            }
                        }
                        betweenRayTime = 0.0f;

                    }
                }
                else
                {
                    flg = false;
                    stringTime = 0.0f;
                }
            }

        }
    }

    public void Note()
    {
        betweenTime += Time.deltaTime; // 時間をカウント
        {
            // 前回の発射から1秒以上経過していたら
            if (betweenTime >= 1.0f)
            {
                // Vキーが押されていたら
                if (Input.GetKey(KeyCode.V))
                {
                    ChargeNote();
                }

                // Vキーが離されたら
                if (Input.GetKeyUp(KeyCode.V))
                {
                    ShootNote();
                }
            }
        }
    }

    public void ChargeNote()
    {
        //最大速度まで
        if (shootPower <= 10.0f)
        {
            // 初期速度を加算
            shootPower += 0.02f;
        }
    }

    public void ShootNote()
    {
        // Vキーが離されたら
        if (Input.GetKeyUp(KeyCode.V))
        {
            switch (CountSE) {
                case 1: audioSource.PlayOneShot(noteSE);CountSE++; break;
                case 2: audioSource.PlayOneShot(noteSE2); CountSE++; break;
                case 3: audioSource.PlayOneShot(noteSE3); CountSE++; break;
                default:break;
            };

            GameObject noteObj = Instantiate(
                prefabNote,// インスタンス化するプレハブ
                Master.transform.position + new Vector3(0.0f, raiseY, 0.0f) + Master.transform.forward, // 発射元オブジェクトの座標より少し持ち上げる
                Quaternion.Euler(90, 0, 0)
                );

            // 音符の参照からスクリプトを取得
            NoteMove scNote = noteObj.GetComponent<NoteMove>();

            // 移動速度とプレイヤーの正面の向きをセット
            scNote.SetSpeed(shootPower);
            scNote.SetMasForward(transform.forward);


            // 音符の参照をセット
            if (shot1 == null) { shot1 = noteObj; }
            else if (shot1 != null && shot2 == null) { shot2 = noteObj; }
            else if (shot1 != null && shot2 != null)
            {
                shot1 = shot2;
                shot2 = noteObj;
            }


            // 前回発射からの時間リセット
            betweenTime = 0.0f;

            // 発射力リセット
            shootPower = 1.0f;
        }
    }

    public void String()
    {
        // 有効な音符が2つ以上あるなら
        if (shot1 && shot2)
        {
            Setshot1 = shot1;
            Setshot2 = shot2;

            audioSource.PlayOneShot(stringSE);
            flg = true;

            // レイ部分に弦オブジェクトを生成
            stringObj = Instantiate(
           prefabString,// インスタンス化するプレハブ
           shot1.transform.position,
           Quaternion.Euler(0, 0, 0)
           );// 発射元オブジェクト

            // 弦(Z方向ベクトル)とレイのベクトルのY軸角度差を求める
            signedAngle = Vector3.SignedAngle(normal, Vector3.back, Vector3.up);

            // 求めた角度分弦を回転させる
            stringObj.transform.Rotate(0, -signedAngle, 0);

            // 弦の発生元の音符の参照から、スクリプトを取得
            scnote1 = shot1.GetComponent<NoteMove>();
            scnote2 = shot2.GetComponent<NoteMove>();

            // 有効な音符の生存時間を再設定
            scnote1.SetAliveTime(5);
            scnote2.SetAliveTime(5);

        }
    }

    public void MoveString()
    {
        if (shot1 && shot2)
        {
            stringTime += Time.deltaTime;

            if (stringTime <= 5.0f)
            {
                betweenRayTime += Time.deltaTime;
                if (betweenRayTime >= 0.2f)
                {
                    // ヒットオブジェクト
                    RaycastHit hit;

                    // 音符1と音符2の差分を求め
                    temp = shot2.transform.position - shot1.transform.position;
                    // 正規化して方向ベクトルを求める
                    normal = temp.normalized;

                    canWave = true;

                    // レイの可視化
                    Debug.DrawRay(shot1.transform.position, temp, Color.red, 5.0f);

                    // 弦(Z方向ベクトル)とレイのベクトルのY軸角度差を求める
                    signedAngle = Vector3.SignedAngle(normal, -stringObj.transform.forward, Vector3.up);

                    // 求めた角度分弦を回転させる
                    stringObj.transform.Rotate(0, -signedAngle, 0);

                    // 音符の差分のベクトル長を求める
                    float vecLen = temp.magnitude;

                    // ベクトル長を元に拡大率を設定
                    stringObj.transform.localScale = new Vector3(1.0f, 1.0f, vecLen);

                    stringObj.transform.position = shot1.transform.position;

                    // レイ処理
                    if (Physics.Raycast(shot1.transform.position, temp, out hit, 3))
                    {
                        // 敵ヒット
                        if (hit.collider.CompareTag("Enemy"))
                        {
                            GameObject hitObj = hit.collider.gameObject;
                            if (hit.collider.gameObject)
                            {
                                GetPlayerAttack scEnemy = hitObj.GetComponent<GetPlayerAttack>();

                                scEnemy.SetDamage(1);
                            }

                            // 衝突パーティクル生成
                            GameObject colEffectObj = Instantiate(
                                           PrefabColEffect,
                                           hitObj.transform.position,
                                           Quaternion.identity);
                        }
                    }
                    betweenRayTime = 0.0f;

                }
            }
            else
            {
                flg = false;
                stringTime = 0.0f;
            }
        }
    }
}





