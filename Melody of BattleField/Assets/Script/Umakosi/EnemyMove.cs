using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    public float power;             //攻撃力
    public float hp;                //体力
    public float jumpPower;         //ジャンプ力
    public float chaseSpeed;        //追いかけるスピード
    public float jumpAttackspeed;   //ジャンプ攻撃のスピード


    //private NavMeshAgent navMeshAgent;
    private bool longAttackFlg;  //遠距離攻撃に入るフラグ(ヒエラルキー内のSearchAreaに入ったらtrue)
    private bool shortAttackFlg; //近距離攻撃に入るフラグ(ヒエラルキー内のAttackに入ったらtrue)
    private Vector3 player;      //操作キャラの座標取得

    private Rigidbody rb;        //  Rigidbodyを使うための変数

    private bool isGround;       //  地面に着地しているか判定する変数

    public float weight;         //ジャンプ攻撃の頻度(600フレームに一回行う)
    public float frame;          //アクション遷移+ジャンプ攻撃のクールタイム
                                 //(ジャンプ攻撃後150フレームの間動かなくなって700フレームで追跡モード)

    //スクリプト取得/////////////////
    private GameObject prowling;
    private GameObject anim;
    private GameObject p;
    /// //////////////////////////////

    int num;//アクション
    int memoryNum;//アクション
    private void Start()
    {
        power = 10;
        //navMeshAgent = GetComponent<NavMeshAgent>(); // NavMeshAgentを保持しておく
        longAttackFlg = false;
        shortAttackFlg = false;
        rb = GetComponent<Rigidbody>();
        prowling = GameObject.Find("enemy");
        anim = GameObject.Find("enemy");
        p = GameObject.Find("Player");
        num = 3;
    }

    private void Update()
    {
        //transform.Rotate(new Vector3(0, 5, 0));
        memoryNum = num;
        if (Input.GetKey(KeyCode.Q))
        {
            num = 5;
        }
        if (Input.GetKey(KeyCode.E))
        {
            num = 6;
        }
        if (Input.GetKey(KeyCode.R))
        {
            num = 7;
        }

        Debug.Log(num);
        //Debug.Log(anim.GetComponent<Anim>().animEnd);       

        if (hp<0)
        {
            num = 6;
        }
        weight += 0.1f;
        hp -= 0.1f;
        switch (num)
        {
            case 1:
                Atack();
                break;

            case 2:
                JumpAtack();
                break;
            case 3:
                if (!shortAttackFlg && !longAttackFlg)
                {
                    prowling.GetComponent<Patrol>().Prowling();
                    anim.GetComponent<Anim>().WalkAnim();
                    Debug.Log("敵徘徊中");
                }
                break;
            case 4:
                Chase();
                break;
            case 5:
                Hit();
                AnimEnd();
                break; 
            case 6:
                Death();
                break;
            case 7:
                Protect();
                AnimEnd();
                break;
        }      
    }

    private void JumpAtack()
    {
        frame += 0.5f;

        if (frame > 700)
        {
            Debug.Log("帰る");
            num = 4;
            weight = 0;
            frame = 0;
        }

        if (frame > 450) {  return; }

        if (!longAttackFlg) { return; }

        float speed = this.jumpAttackspeed * Time.deltaTime;

        if (isGround)
        {
            rb.AddForce(new Vector3(0, jumpPower, 0));
            isGround = false;
        }

        transform.position = Vector3.MoveTowards(transform.position, player, speed);
        transform.LookAt(player);
        anim.GetComponent<Anim>().JumpAnim();


    }

    private void Chase()
    {
        if (!longAttackFlg) { return; }
        if (!isGround) { return; }
        float speed = this.chaseSpeed * Time.deltaTime;
        player = p.transform.position;

        Vector3 vector3 = player - transform.position;
        Quaternion quaternion = Quaternion.LookRotation(vector3);
        transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, Time.deltaTime * 3);

        transform.position = Vector3.MoveTowards(transform.position, player, speed);

        anim.GetComponent<Anim>().RunAnim();

    }

    private void Atack()
    {
        anim.GetComponent<Anim>().AtackAnim();
    }

    private void Hit()
    {
        anim.GetComponent<Anim>().HitAnim();
    }

    public void AnimEnd()
    {
        //if (anim.GetComponent<Anim>().animEnd)
        //{
        //    anim.GetComponent<Anim>().animEnd = false;
        //    num = 3;
        //    Debug.Log("Hit終わり");
        //}
    }

    private void Protect()
    {
        anim.GetComponent<Anim>().ProtectAnim();
    } 
    
    private void Death()
    {
        anim.GetComponent<Anim>().DeathAnim();
    }

    private void Jump()
    {
            shortAttackFlg = false;                
            anim.GetComponent<Anim>().JumpAnim();
            Debug.Log("ジャンプ攻撃！");
    }

    public void ShortRange(Collider collider)
    {
        if (CompareTag("Player", collider))
        {
            num = 1;
            shortAttackFlg = true;
        }
    }

    public void OutShortRange(Collider collider)
    {
        if (CompareTag("Player", collider))
        {
            shortAttackFlg = false;
            num = 4;
        }

    }

    public void LongRange(Collider collider)
    {
        Debug.Log("LongRange");

        if (num == 1) { return; }
        if (CompareTag("Player", collider))
        {
            Debug.Log("当たってます" + collider.tag);
            if(weight<600)
            {
                num = 4;
            }
            else
            {
                num = 2;
                player = p.transform.position;

            }
            longAttackFlg = true;
        }
    }

    public void OutLongRange(Collider collider)
    {
        if (CompareTag("Player", collider))
        {
            Debug.Log("逃走");

            longAttackFlg = false;
            num = 3;
        }

    }
    void OnCollisionStay(Collision other)//  地面に触れた時の処理
    {

        if (other.gameObject.tag == "Ground")
        {
            Debug.Log(other.gameObject.tag);

            isGround = true;//  Groundedをtrueにする
        }
    }

    private bool CompareTag(string tagName, Collider collider)
    {
        if (collider.CompareTag(tagName))
        {
            if (isGround)
            {
                return true;
            }
        }
        return false;

    }
}