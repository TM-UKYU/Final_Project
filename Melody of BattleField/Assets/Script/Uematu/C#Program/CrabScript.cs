using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabScript : MonoBehaviour
{
    public enum CrabState
    {
        idle,
        patrol,
        chase,
        attack,
        shockwaveAttack,
        breath,
        Die,
        Frightened
    }

    private CharacterController characterController;
    private Animator animator;
    //カニが登場した最初の位置
    private Vector3 defaltPos;
    //カニの状態
    private CrabState crabState = CrabState.idle;
    //　目的地
    private Vector3 destination;
    //　移動範囲
    [SerializeField]
    private float movementRange = 20f;
    //　移動速度
    private Vector3 velocity = Vector3.zero;
    //　歩くスピード
    [SerializeField]
    private float walkSpeed = 0.5f;
    //　追いかけるスピード
    [SerializeField]
    private float chaseSpeed = 1f;
    //　向きを回転する速さ
    [SerializeField]
    private float rotateSpeed = 2f;
    //　idle状態の経過時間
    private float elapsedTimeOfIdleState = 0f;
    //　idle状態で留まる時間
    [SerializeField]
    private float timeToStayInIdle = 5f;
    //　攻撃対象のTransform
    private Transform attackTargetTransform;
    //　攻撃時の対象の位置
    private Vector3 attackTargetPos;
    //ブレスを発射しているかどうかの変数
    private bool IsBreath = false;
    //弾の数を管理しておく変数
    private int BulletNum = 0;
    //弾の最大数
    [SerializeField]
    private int MaxBulletNum = 5;
    //弾のオブジェクト
    public BreathBall breathBall;
    //泡のオブジェクト
    public Bubble bubble;
    //カニのHP
    [SerializeField]
    private float HitPoint = 50;
    //怯み値
    [SerializeField]
    private float FrightenedNum = 10;
    //現在の怯み値
    private float NowFrightened = 0;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        defaltPos = transform.position;
        SetRandomDestination();
    }

    // Update is called once per frame
    void Update()
    {
        //カニの状態によって処理を変える
        if(crabState==CrabState.idle)
        {
            Idle();
        }
        else if(crabState==CrabState.patrol)
        {
            Patrol();
        }
        else if (crabState == CrabState.chase)
        {
            Chase();
        }
        else if (crabState == CrabState.attack)
        {
            Attack();
        }
        else if (crabState == CrabState.shockwaveAttack)
        {
            ShockwaveAttack();
        }
        else if(crabState==CrabState.breath)
        {
            Breath();
        }

        //　共通するCharacterControllerの移動処理
        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        Vector3 pos = transform.position;
        pos.y -= 3.480005f;
        transform.position = pos;
    }

    //目的地を設定する
    void SetRandomDestination()
    {
        //最初の位置から有効範囲のランダム位置を取得
        var randomPos = defaltPos + Random.insideUnitSphere * movementRange;
        var ray = new Ray(randomPos + Vector3.up * 10f, Vector3.down);
        RaycastHit hit;
        //目的地が地面になるように再設定
        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Field"))) 
        {
            destination = hit.point;
        }
    }

    public void SetStatus(CrabState craState, Transform playerTransform = null)
    {
        crabState = craState;
        if (crabState == CrabState.idle)
        {
            velocity = new Vector3(0f, velocity.y, 0f);
            animator.SetFloat("Move Speed", 0f);
            animator.SetBool("Chase", false);
            SetRandomDestination();
            Debug.Log("アイドル");
        }
        else if (crabState == CrabState.patrol)
        {
            //アニメーションを変更する
            animator.SetTrigger("Patrol");
            Debug.Log("パトロール");
        }
        else if (crabState == CrabState.attack)
        {
            attackTargetTransform = playerTransform;
            attackTargetPos = attackTargetTransform.position;
            velocity = new Vector3(0f, velocity.y, 0f);
            animator.SetTrigger("Attack");
            animator.SetBool("Chase", false);
            Debug.Log("通常攻撃");
        }
        else if (crabState == CrabState.shockwaveAttack)
        {
            attackTargetTransform = playerTransform;
            attackTargetPos = attackTargetTransform.position;
            velocity = new Vector3(0f, velocity.y, 0f);
            animator.SetTrigger("ShockwaveAttack");
            animator.SetBool("Chase", false);
            Debug.Log("衝撃波攻撃");
        }
        else if (crabState == CrabState.chase)
        {
            animator.SetBool("Chase", true);
            attackTargetTransform = playerTransform;
            Debug.Log("チェイス");
        }
        else if(crabState==CrabState.breath)
        {
            attackTargetTransform = playerTransform;
            attackTargetPos = attackTargetTransform.position;
            animator.SetTrigger("Breath");
            animator.SetBool("Chase", false);
            velocity = new Vector3(0f, velocity.y, 0f);
            BulletNum = 0;
            IsBreath = false;
            Debug.Log("ブレス");
        }
        else if(crabState==CrabState.Die)
        {
            Debug.Log("死");
            animator.SetBool("Die", true);
        }
        else if(crabState==CrabState.Frightened)
        {
            Debug.Log("怯み");
            animator.SetTrigger("Frightened");
        }
    }

    //状態取得メソッド
    public CrabState GetStatus()
    {
        return crabState;
    }

    //Idle状態の時の処理
    private void Idle()
    {
        elapsedTimeOfIdleState += Time.deltaTime;
        //一定時間が経過したらpatrol状態にする
        if(elapsedTimeOfIdleState>=timeToStayInIdle)
        {
            elapsedTimeOfIdleState = 0.0f;
            SetStatus(CrabState.patrol);
        }
    }

    void Patrol()
    {
        //　通常移動処理
        if (characterController.isGrounded)
        {
            velocity = Vector3.zero;
            //　目的地の方向を計算し、向きを変えて前方に進める
            var direction = (destination - transform.position).normalized;
            animator.SetFloat("Move Speed", direction.magnitude);
            var targetRot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(destination - transform.position), Time.deltaTime * rotateSpeed);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, targetRot.eulerAngles.y, transform.eulerAngles.z);
            velocity = transform.forward * walkSpeed;
            transform.position = transform.forward * walkSpeed;
        }
        //　目的地に着いたらidle状態にする
        if (Vector3.Distance(transform.position, destination) < 0.5f)
        {
            SetStatus(CrabState.idle);
            animator.SetBool("Patrol", false);
        }
    }

    //Chase状態時の処理
    void Chase()
    {
        //目的地を毎回設定しなおす
        destination = attackTargetTransform.position;
        //追いかける処理
        if(characterController.isGrounded)
        {
            velocity = Vector3.zero;
            //　目的地の方向を計算し、向きを変えて前方に進める
            var direction = (destination - transform.position).normalized;
            animator.SetFloat("Move Speed", direction.magnitude);
            var targetRot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(destination - transform.position), Time.deltaTime * rotateSpeed);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, targetRot.eulerAngles.y, transform.eulerAngles.z);
            velocity = transform.forward * chaseSpeed;
        }
    }

    void Attack()
    {
        //攻撃状態になった時のキャラクターの向きを計算し、徐々にそちらの向きへ回転させる
        var targetRot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(attackTargetPos - transform.position),
                                          Time.deltaTime * 2f);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, targetRot.eulerAngles.y, transform.eulerAngles.z);

        //Attackアニメーションが終了したらIdle状態にする
        if(//animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")&&
           animator.GetCurrentAnimatorStateInfo(0).normalizedTime>=1f)
        {
            SetStatus(CrabState.idle);
        }
    }

    void ShockwaveAttack()
    {
        //攻撃状態になった時のキャラクターの向きを計算し、徐々にそちらの向きへ回転させる
        var targetRot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(attackTargetPos - transform.position),
                                          Time.deltaTime * 2f);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, targetRot.eulerAngles.y, transform.eulerAngles.z);

        //Attackアニメーションが終了したらIdle状態にする
        if (//animator.GetCurrentAnimatorStateInfo(0).IsName("ShockwaveAttack")&& 
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            SetStatus(CrabState.idle);
        }
    }

    //ブレスを撃つときの処理
    void Breath()
    {

        Bubble instance = (Bubble)Instantiate(bubble, transform);

        if (!IsBreath)
        {
            //攻撃状態になった時のキャラクターの向きを計算し、徐々にそちらの向きへ回転させる
            var targetRot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(attackTargetPos - transform.position),
                                              Time.deltaTime * 2f);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, targetRot.eulerAngles.y, transform.eulerAngles.z);

        }
        else
        {
            //最大発射数出なければ発射する
            if (MaxBulletNum > BulletNum)
            {
                //弾の生成
                BreathBall obj = (BreathBall)Instantiate(breathBall, transform);

                //弾の数を増やす
                BulletNum++;
            }
        }

        //アニメーションが終わったらアイドルに変更
        if (//animator.GetCurrentAnimatorStateInfo(0).IsName("Breath")&& 
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            SetStatus(CrabState.idle);
        }
    }

    //ブレスを発射したかどうか管理している変数のセット
    public void SetIsBreath(bool flg)
    {
        IsBreath = flg;
    }

    //ダメージを食らった時の処理
    public void DecHP(float Damage)
    {
        HitPoint -= Damage;
        NowFrightened++;

        if(NowFrightened>=FrightenedNum)
        {
            SetStatus(CrabState.Frightened);
        }

        if(HitPoint<=0)
        {
            SetStatus(CrabState.Die);
        }
    }
}
