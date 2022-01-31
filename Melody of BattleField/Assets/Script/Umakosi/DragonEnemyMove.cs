using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DragonEnemyMove : MonoBehaviour
{
    public float power;             //�U����
    public float hp;                //�̗�
    public float jumpPower;         //�W�����v��
    public float chaseSpeed;        //�ǂ�������X�s�[�h
    public float walkSpeed;         //�����X�s�[�h
    public float jumpAttackspeed;   //�W�����v�U���̃X�s�[�h
    private Vector3 latestPos;

    public bool isFly;

    private bool longAttackFlg;  //�������U���ɓ���t���O(�q�G�����L�[����SearchArea�ɓ�������true)
    private bool shortAttackFlg; //�ߋ����U���ɓ���t���O(�q�G�����L�[����Attack�ɓ�������true)
    private Vector3 player;      //����L�����̍��W�擾

    private Rigidbody rb;        //  Rigidbody���g�����߂̕ϐ�


    public float weight;         //�W�����v�U���̕p�x(600�t���[���Ɉ��s��)

    public int fireBallMax;
    private int fireBallNum;
    private bool fireBallFlg;
    private bool finishFireBall;
    private bool isShotBomb;
    private bool isGround;       //  �n�ʂɒ��n���Ă��邩���肷��ϐ�

    private float coolTime;

    private int actionPattern;


    //�X�N���v�g�擾/////////////////
    private GameObject prowling;
    private GameObject anim;
    private GameObject p;
    private GameObject bullet;
    private GameObject Sphere;
    private GameObject bomb;
    private GameObject effect;
    /// //////////////////////////////

    public enum Hp_State
    {
        Hp_Energetic,
        Hp_Low,
        Hp_Moribund
    }

    private Hp_State hp_State;

    public enum Action_State
    {
        Action_Sleep,
        Action_Patrol,
        Action_Attack,
        Action_TailAttack,
        Action_FireBall,
        Action_Chase,
        Action_TakeOff,
        Action_Revolve,
        Action_FlyTackle,
        Action_Protect,
        Action_Hit,
        Action_Death,
        Action_Back,
        Action_Land,
        Action_FlyFloat,
        Action_Wait,
        Action_Walk,
        Action_Tukiage
    }

    private Action_State action_State;

    //int num;//�A�N�V����
    Action_State nextNum;//�A�N�V����
    int memoryNum;//�A�N�V����

    public float countDown;

    [SerializeField] private Vector3 center = Vector3.zero;

    // ��]��
    [SerializeField] private Vector3 axis = Vector3.up;

    // �~�^������
    [SerializeField] private float period = 2;

    //�J�����ύX�p�̃I�u�W�F�N�g
    public CameraChange cameraChange;

    private void Start()
    {
        power = 10;

        fireBallFlg = false;
        finishFireBall = false;
        longAttackFlg = false;
        shortAttackFlg = false;
        isShotBomb = false;

        rb = GetComponent<Rigidbody>();

        prowling = GameObject.Find("enemy");
        anim = GameObject.Find("enemy");
        p = GameObject.Find("Keyboard");
        bullet = GameObject.Find("FireBallPoint");
        Sphere = GameObject.Find("Sphere");
        bomb = GameObject.Find("BombPoint");
        effect = GameObject.Find("enemy");

        if (Random.Range(1, 10) == 1)
        {
            action_State = Action_State.Action_Sleep;
        }
        else
        {
            action_State = Action_State.Action_Sleep;
        }
        hp_State = Hp_State.Hp_Energetic;

        countDown = 2;

        //�ŏ��~�߂�
        //GetComponent<CrabScript>().enabled = false;

       // StartCoroutine("Pouse");
    }

    private void Update()
    {
        Debug.Log("�ɂ�����");

        player = p.transform.position;

        if (hp < 0)
        {
            action_State = Action_State.Action_Death;
        }
        weight += 0.1f;
        hp -= 0.1f;

        switch (action_State)
        {
            case Action_State.Action_Patrol:
                if (!shortAttackFlg && !longAttackFlg)
                {
                    prowling.GetComponent<Patrol>().Prowling();
                    anim.GetComponent<Anim>().WalkAnim();
                    Debug.Log("�G�p�j��");
                }
                break;

            case Action_State.Action_Attack:
                AtackAnim();
                break;

            case Action_State.Action_FireBall:
                FireBall(fireBallMax, isFly);
                break;

            case Action_State.Action_Chase:
                Chase();
                break;

            case Action_State.Action_Hit:
                HitAnim();
                AnimEnd();
                break;

            case Action_State.Action_Death:
                DeathAnim();
                break;

            case Action_State.Action_Protect:
                ProtectAnim();
                AnimEnd();
                break;

            case Action_State.Action_TailAttack:
                TailAttackAnim();
                break;

            case Action_State.Action_TakeOff:
                TakeOff();
                break;

            case Action_State.Action_Revolve:
                Revolve();
                break;
            case Action_State.Action_FlyTackle:
                FlyTackle();
                break;

            case Action_State.Action_Tukiage:
                Tukiage();
                break;

            case Action_State.Action_Sleep:
                SleepAnim();
                break;

            case Action_State.Action_Back:
                Back();
                break;

            case Action_State.Action_Land:
                LandAnim();
                break;

            case Action_State.Action_FlyFloat:
                LandAnim();
                break;

            case Action_State.Action_Wait:
                Wait();
                break;

            case Action_State.Action_Walk:
                Walk();
                break;
        }

        if (Input.GetKey(KeyCode.F))
        {
            action_State = Action_State.Action_TakeOff;

            nextNum = Action_State.Action_FlyTackle;
            coolTime = 800;
        }

        if (Input.GetKey(KeyCode.K))
        {
            hp_State = Hp_State.Hp_Low;
        }

        if (!isShotBomb)
        {
            HpCheck();
        }

        if (!longAttackFlg)
        {
            actionPattern = Random.Range(1, 4);
        }
    }

    public bool CoolTime(float Time, Action_State nextState)
    {
        if (Time > 0.0f)
        {
            Debug.Log("�N�[���^�C��" + Time);
            Time--;
            if (Time < 200.0f)
            {
                action_State = nextState;
                coolTime = Time;
                return false;
            }
            coolTime = Time;
            return false;

        }
        coolTime = Time;
        return true;
    }

    public void IsFireBall()
    {
        Debug.Log("�A�j���[�V�����C�x���g:���˂�����������������������");
        fireBallNum++;
        fireBallFlg = true;
    }
    private void HpCheck()
    {
        if (hp_State == Hp_State.Hp_Low)
        {
            bomb.GetComponent<Bomb>().FireBombs();
            isShotBomb = true;
        }
    }

    private void Wait()
    {
        switch (hp_State)
        {
            case Hp_State.Hp_Energetic:
                action_State = Action_State.Action_Walk;
                coolTime = 0;
                break;

            case Hp_State.Hp_Low:
                action_State = Action_State.Action_Chase;
                coolTime = 0;
                break;

            case Hp_State.Hp_Moribund:
                IdleAnim();
                break;
        }
    }

    public void FinishFireBall()
    {
        finishFireBall = true;

        Debug.Log("�A�j���[�V�����C�x���g:�I������������");

        //nextNum = Action_State.Action_FlyTackle;


    }

    private void FireBall(int fireMax, bool isFly)
    {
        transform.LookAt(player);
        if (isFly)
        {
            anim.GetComponent<Anim>().FlyFireBallAnim();
        }
        else
        {
            anim.GetComponent<Anim>().FireBallAnim();
        }

        if (fireBallFlg)
        {
            fireBallFlg = false;
            bullet.GetComponent<FireBall>().Bullet();
        }

        if (fireBallNum >= fireMax && finishFireBall)
        {
            if (isFly)
            {
                action_State = Action_State.Action_Land;
            }
            else
            {
                action_State = Action_State.Action_TakeOff;
            }
            coolTime = 600.0f;
            //nextNum = Action_State.Action_Wait;

            //action_State = Action_State.Action_Chase;
            weight = 0;
            finishFireBall = false;
            Debug.Log("��");
            return;
        }
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

    private void Walk()
    {
        transform.Translate(0, 0, walkSpeed * Time.deltaTime);
        WalkAnim();
    }

    private void Revolve()
    {
        anim.GetComponent<Anim>().FlyAnim();

        var tr = transform;
        // ��]�̃N�H�[�^�j�I���쐬
        var angleAxis = Quaternion.AngleAxis(360 / period * Time.deltaTime, axis);

        // �~�^���̈ʒu�v�Z
        var pos = tr.position;

        pos -= center;
        pos = angleAxis * pos;
        pos += center;

        pos.y *= 0;

        tr.position = pos;

        Vector3 diff = transform.position - latestPos;   //�O�񂩂�ǂ��ɐi�񂾂����x�N�g���Ŏ擾
        latestPos = transform.position;  //�O���Position�̍X�V

        //�x�N�g���̑傫����0.01�ȏ�̎��Ɍ�����ς��鏈��������
        if (diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(diff); //������ύX����
        }

    }
    private void FlyTackle()
    {
        countDown -= Time.deltaTime;

        if (countDown < 1)
        {
            Invoke("ActionChange", 3.0f);
            transform.Translate(0, 0, 50 * Time.deltaTime);
            anim.GetComponent<Anim>().FlyGlide();
        }
        else
        {
            transform.Rotate(0.3f, 0, 0);

            if (!longAttackFlg) { return; }
            if (!isGround) { return; }
            float speed = (this.chaseSpeed * 6) * Time.deltaTime;

            player = p.transform.position;
            Vector3 vector3 = (player - transform.position).normalized;

            Quaternion quaternion = Quaternion.LookRotation(vector3);
            transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, Time.deltaTime * 3);

        }

        //transform.position = Vector3.MoveTowards(transform.position, player, speed);

    }

    private void ActionChange()
    {
        Debug.Log("�˂��グ�U��");

        action_State = Action_State.Action_Tukiage;
        coolTime = 1000;
    }

    private void Tukiage()
    {
        if (Vector3.Distance(transform.position, Sphere.transform.position) < 1.0f)
        {
            //transform.rotation.ToAngleAxis(out float angle, out Vector3 axis);
            //nowAngle = angle;
            //if (nowAngle>0)
            //{
            //    transform.Rotate(-0.03f, 0, 0);
            //    Debug.Log("�˂��グ�I��");

            //}

            action_State = Action_State.Action_Land;
            transform.rotation = Quaternion.AngleAxis(0.0f, new Vector3(1, 0, 0));
        }
        else
        {
            transform.LookAt(Sphere.transform);
            transform.Translate(0, 0, 75 * Time.deltaTime);
            //Invoke("abc", 5.0f);
        }
    }

    private void abc()
    {
        Vector3 direction = this.transform.up;
        this.GetComponent<Rigidbody>().AddForce(direction * 25, ForceMode.Impulse);
    }

    private void Back()
    {
        FlyFloatAnim();
    }

    private void TakeOff()
    {
        transform.Translate(0, 0, -7 * Time.deltaTime);
        TakeOffAnim();
    }

    private void AtackAnim()
    {
        anim.GetComponent<Anim>().AtackAnim();
    }

    private void HitAnim()
    {
        anim.GetComponent<Anim>().HitAnim();
    }

    public void AnimEnd()
    {
        if (anim.GetComponent<Anim>().AnimEnd() > 1)
        {
            action_State = Action_State.Action_FireBall;
            Debug.Log("Hit�I���");
        }
    }

    private void DeathAnim()
    {
        anim.GetComponent<Anim>().DeathAnim();
    }

    private void ProtectAnim()
    {
        anim.GetComponent<Anim>().ProtectAnim();
    }

    private void FireBallAnim()
    {
        anim.GetComponent<Anim>().FireBallAnim();
    }

    public void Fly()
    {
        Debug.Log("�A�j���[�V�����C�x���g:Flyyyyyyyyyyyyyyyyyyyyyyyyyyyyy");
        if (nextNum == Action_State.Action_FlyTackle)
        {
            action_State = Action_State.Action_Revolve;
            center = p.transform.position;
        }
        else
        {
            action_State = Action_State.Action_Land;
        }
    }

    private void FlyFireBallAnim()
    {
        anim.GetComponent<Anim>().FlyFireBallAnim();
    }

    private void WalkAnim()
    {
        anim.GetComponent<Anim>().WalkAnim();
    }

    private void TailAttackAnim()
    {
        anim.GetComponent<Anim>().TailAttackAnim();
    }

    private void TakeOffAnim()
    {
        anim.GetComponent<Anim>().TakeOffAnim();
    }

    private void LandAnim()
    {
        anim.GetComponent<Anim>().LandAnim();
    }

    private void FlyFloatAnim()
    {
        anim.GetComponent<Anim>().FlyFloatAnim();
    }

    private void SleepAnim()
    {
        anim.GetComponent<Anim>().SleepAnim();
    }

    private void IdleAnim()
    {
        anim.GetComponent<Anim>().IdleAnim();
    }

    public void ShortRange(Collider collider)
    {
        if (CompareTag("Player", collider))
        {
            if (!CoolTime(coolTime, nextNum)) { return; }
            action_State = Action_State.Action_TailAttack;
            shortAttackFlg = true;
        }
    }

    public void OutShortRange(Collider collider)
    {

        if (CompareTag("Player", collider))
        {
            if (!CoolTime(coolTime, nextNum)) { return; }
            shortAttackFlg = false;
            action_State = Action_State.Action_Chase;
        }

    }

    public void LongRange(Collider collider)
    {
        Debug.Log("LongRange");

        if (CompareTag("Player", collider))
        {
            if (shortAttackFlg) { return; }

            if (!CoolTime(coolTime, nextNum)) { return; }

            Debug.Log("�������Ă܂�" + collider.tag);

            switch (actionPattern)
            {
                case 1:
                    action_State = Action_State.Action_Chase;
                    break;

                case 2:
                    action_State = Action_State.Action_FireBall;
                    break;

                case 3:
                    action_State = Action_State.Action_TakeOff;

                    nextNum = Action_State.Action_FlyTackle;
                    coolTime = 800; break;
            }
            //if (weight < 600)
            //{
            //    action_State = Action_State.Action_Chase;
            //}
            //else
            //{
            //    action_State = Action_State.Action_FireBall;
            //}

            //if (weight>600)
            //{
            //    num=Random.Range(1, 10);
            //}

            longAttackFlg = true;
        }
    }

    public void OutLongRange(Collider collider)
    {
        if (CompareTag("Player", collider))
        {
            if (!CoolTime(coolTime, nextNum)) { return; }

            Debug.Log("����");

            longAttackFlg = false;
            action_State = Action_State.Action_Patrol;
        }

    }
    void OnCollisionStay(Collision other)//  �n�ʂɐG�ꂽ���̏���
    {
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAA���Ă�!");

        Debug.Log("���n");

        if (other.gameObject.tag == "Ground")
        {
            Debug.Log(other.gameObject.tag);

            isGround = true;//  Grounded��true�ɂ���
        }
        if (other.gameObject.tag == "Player") { return; }
        //if (other.gameObject.tag == "Untagged") { return; }
        if (other.gameObject.tag == "Ground") { return; }
        effect.GetComponent<SetEffects>().EffectUpdate(other.transform.position);
        Invoke("EffectEnd",1.0f);
    }

    void EffectEnd()
    {
        effect.GetComponent<SetEffects>().EffectEnd();
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

    private IEnumerator Pouse() //�R���[�`���֐��̖��O
    {
        //�R���[�`���̓��e
        Debug.Log("�X�^�[�g");
        yield return new WaitForSeconds(10.0f);
        Debug.Log("10�b��");
        GetComponent<CrabScript>().enabled = true;
        if (cameraChange == null) { yield return new WaitForSeconds(0.0f); }
        cameraChange.ChangeCamera();
    }
}