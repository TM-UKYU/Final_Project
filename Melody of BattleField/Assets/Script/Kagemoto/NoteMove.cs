// プレイヤーから発射される音符の移動スクリプト
// プレイヤーの方向とキーの検知の為プレイヤーを参照する

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMove : MonoBehaviour
{
    [SerializeField] private GameObject Master;  // 自身を発射するオブジェクト
    [SerializeField] private float DestroyTime = 10.0f; // 生存時間
    [SerializeField] private GameObject PrefabColEffect; // 衝突エフェクト
    [SerializeField] private AudioClip noteColSE;        // 音符SE
    AudioSource audioSource;

    [SerializeField] private GameObject refAudioManager;

    // 内部値
    private Vector3 force;
    private float speed = 1.0f;
    private float direction; // 発射方向
    private Rigidbody rbody;
    private float aliveTime = 0.0f;


    void Start()
    {
        refAudioManager= GameObject.Find("AudioManager");

        //オーディオソースを取得
        audioSource = GetComponent<AudioSource>();

        Keyboard scMaster = Master.GetComponent<Keyboard>();
        //speed = scMaster.shootSpeed;
        rbody = GetComponent<Rigidbody>();
        //rbody.AddForce(0.0f,0.0f,10.0f, ForceMode.Impulse);
        //rbody.AddForce(Master.transform.forward * transform.rotate* speed, ForceMode.Impulse);
        rbody.AddForce(force * speed, ForceMode.Impulse);
    }

    void Update()
    {
        // 1秒に+1カウント
        aliveTime += Time.deltaTime;

        if (aliveTime >= DestroyTime) { Destroy(this.gameObject); }

    }

    public void SetMasForward(Vector3 value)
    {
        force = value;
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }

    public void Stop()
    {
        rbody.velocity = Vector3.zero;
    }


    public void ResetAliveTime()
    {
        aliveTime = 0.0f;
    }

    public void SetAliveTime(int time)
    {
        aliveTime = time;
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject hitObj = collision.gameObject;
        GetPlayerAttack scEnemy = hitObj.GetComponent<GetPlayerAttack>();

        //audioSource.PlayOneShot(noteColSE);
        AudioManager scAudio = refAudioManager.GetComponent<AudioManager>();
        scAudio.PlaySE(noteColSE);

        scEnemy.SetDamage(2);
        Destroy(this.gameObject);
        Debug.Log("当たった!");

        // 衝突パーティクル生成
        GameObject colEffectObj = Instantiate(
                       PrefabColEffect,
                       transform.position,
                       Quaternion.identity);
    }
}
