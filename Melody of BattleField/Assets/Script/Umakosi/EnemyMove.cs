using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    public float power;             //�U����
    public float hp;                //�̗�
    public float jumpPower;         //�W�����v��
    public float chaseSpeed;        //�ǂ�������X�s�[�h
    public float jumpAttackspeed;   //�W�����v�U���̃X�s�[�h


    //private NavMeshAgent navMeshAgent;
    private bool longAttackFlg;  //�������U���ɓ���t���O(�q�G�����L�[����SearchArea�ɓ�������true)
    private bool shortAttackFlg; //�ߋ����U���ɓ���t���O(�q�G�����L�[����Attack�ɓ�������true)
    private Vector3 player;      //����L�����̍��W�擾

    private Rigidbody rb;        //  Rigidbody���g�����߂̕ϐ�

    private bool isGround;       //  �n�ʂɒ��n���Ă��邩���肷��ϐ�

    public float weight;         //�W�����v�U���̕p�x(600�t���[���Ɉ��s��)
    public float frame;          //�A�N�V�����J��+�W�����v�U���̃N�[���^�C��
                                 //(�W�����v�U����150�t���[���̊ԓ����Ȃ��Ȃ���700�t���[���ŒǐՃ��[�h)

    //�X�N���v�g�擾/////////////////
    private GameObject prowling;
    private GameObject anim;
    private GameObject p;
    /// //////////////////////////////

    int num;//�A�N�V����
    int memoryNum;//�A�N�V����
    private void Start()
    {
        power = 10;
        //navMeshAgent = GetComponent<NavMeshAgent>(); // NavMeshAgent��ێ����Ă���
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
                    Debug.Log("�G�p�j��");
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
            Debug.Log("�A��");
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
        //    Debug.Log("Hit�I���");
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
            Debug.Log("�W�����v�U���I");
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
            Debug.Log("�������Ă܂�" + collider.tag);
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