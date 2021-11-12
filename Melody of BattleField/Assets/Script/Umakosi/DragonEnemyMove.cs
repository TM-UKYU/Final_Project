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
    public float jumpAttackspeed;   //�W�����v�U���̃X�s�[�h


    private bool animLock;
    private bool longAttackFlg;  //�������U���ɓ���t���O(�q�G�����L�[����SearchArea�ɓ�������true)
    private bool shortAttackFlg; //�ߋ����U���ɓ���t���O(�q�G�����L�[����Attack�ɓ�������true)
    private Vector3 player;      //����L�����̍��W�擾

    private Rigidbody rb;        //  Rigidbody���g�����߂̕ϐ�

    private bool isGround;       //  �n�ʂɒ��n���Ă��邩���肷��ϐ�

    public float fireSpan;       
    public float weight;         //�W�����v�U���̕p�x(600�t���[���Ɉ��s��)
    public float frame;          //�A�N�V�����J��+�W�����v�U���̃N�[���^�C��
    public float a;          //�A�N�V�����J��+�W�����v�U���̃N�[���^�C��
                                 //(�W�����v�U����150�t���[���̊ԓ����Ȃ��Ȃ���700�t���[���ŒǐՃ��[�h)

    //�X�N���v�g�擾/////////////////
    private GameObject prowling;
    private GameObject anim;
    private GameObject p;
    private GameObject bullet;
    /// //////////////////////////////

    int num;//�A�N�V����
    int memoryNum;//�A�N�V����

    private void Start()
    {
        power               = 10;
        longAttackFlg       = false;
        shortAttackFlg      = false;
        animLock            = false;
        rb                  = GetComponent<Rigidbody>();
        prowling            = GameObject.Find("enemy");
        anim                = GameObject.Find("enemy");
        p                   = GameObject.Find("Player");
        bullet              = GameObject.Find("FireBallPoint");
        num                 = 3;
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

        if (Input.GetMouseButtonDown(0))
        {
            num ++;
        }

        Debug.Log("Action"+num);
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
                
                    prowling.GetComponent<Patrol>().Prowling();
                    anim.GetComponent<Anim>().WalkAnim();
                    Debug.Log("�G�p�j��");
         
                break;
            case 4:
                Debug.Log("Chase");
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
            case 8:
                FireBallAnim();
                break;
            case 9:
                FlyFireBallAnim();
                break;
            case 10:
                TailAttackAnim();
                break;
            case 11:
                SleepAnim();
                break;
        }      
    }

    private void JumpAtack()
    {
        frame += 0.5f;

        if (frame > 700)
        {
            Debug.Log("�A��");
            num = 3;
            weight = 0;
            frame = 100;
            animLock = false;
            return;
        }

        a += 1;
        Debug.Log(a+"aaaaaa");
        transform.LookAt(player);
        anim.GetComponent<Anim>().FireBallAnim();
        if (frame% fireSpan == 0)
        {
            bullet.GetComponent<FireBall>().Bullet();
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
        if (anim.GetComponent<Anim>().animEnd)
        {
            anim.GetComponent<Anim>().animEnd = false;
            num = 3;
            Debug.Log("Hit�I���");
        }
    }

    private void Death()
    {
        anim.GetComponent<Anim>().DeathAnim();
    } 
    
    private void Protect()
    {
        anim.GetComponent<Anim>().ProtectAnim();
    } 
    
    private void FireBallAnim()
    {
        anim.GetComponent<Anim>().FireBallAnim();
    }

    private void FlyFireBallAnim()
    {
        anim.GetComponent<Anim>().FlyFireBallAnim();
    }

    private void TailAttackAnim()
    {
        player = p.transform.position;

        Vector3 vector3 = player - transform.position;
        Quaternion quaternion = Quaternion.LookRotation(vector3);
        transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, Time.deltaTime * 3);
        anim.GetComponent<Anim>().TailAttackAnim();
    }
    
    private void TakeOffAnim()
    {
        anim.GetComponent<Anim>().TakeOffAnim();
    }   
    
    private void SleepAnim()
    {
        anim.GetComponent<Anim>().SleepAnim();
    }

    private void Jump()
    {
            shortAttackFlg = false;                
            anim.GetComponent<Anim>().JumpAnim();
            Debug.Log("�W�����v�U���I");
    }

    public void ShortRange(Collider collider)
    {
        longAttackFlg = false;

        if (CompareTag("Player", collider))
        {
            num = 10;
            shortAttackFlg = true;
        }
    }

    public void OutShortRange(Collider collider)
    {
        if (CompareTag("Player", collider))
        {
            shortAttackFlg = false;
            num = 3;
        }

    }

    public void LongRange(Collider collider)
    {
       // if (!longAttackFlg) { return; }

        Debug.Log("LongRange");

        if (CompareTag("Player", collider))
        {
            Debug.Log("�������Ă܂�" + collider.tag);
            if(weight<600)
            {
                if (num != 3){num = 3;}

            }
            else
            {
                if (num != 2)
                {
                    num = 2;
                    player = p.transform.position; ;
                }

                

            }
            longAttackFlg = true;
        }
    }

    public void OutLongRange(Collider collider)
    {
        if (CompareTag("Player", collider))
        {
            Debug.Log("����");

            longAttackFlg = false;
            num = 3;
        }

    }
    void OnCollisionStay(Collision other)//  �n�ʂɐG�ꂽ���̏���
    {

        if (other.gameObject.tag == "Ground")
        {
            Debug.Log(other.gameObject.tag);

            isGround = true;//  Grounded��true�ɂ���
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