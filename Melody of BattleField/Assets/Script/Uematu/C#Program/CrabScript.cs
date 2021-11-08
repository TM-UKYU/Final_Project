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
    //�J�j���o�ꂵ���ŏ��̈ʒu
    private Vector3 defaltPos;
    //�J�j�̏��
    private CrabState crabState = CrabState.idle;
    //�@�ړI�n
    private Vector3 destination;
    //�@�ړ��͈�
    [SerializeField]
    private float movementRange = 20f;
    //�@�ړ����x
    private Vector3 velocity = Vector3.zero;
    //�@�����X�s�[�h
    [SerializeField]
    private float walkSpeed = 0.5f;
    //�@�ǂ�������X�s�[�h
    [SerializeField]
    private float chaseSpeed = 1f;
    //�@��������]���鑬��
    [SerializeField]
    private float rotateSpeed = 2f;
    //�@idle��Ԃ̌o�ߎ���
    private float elapsedTimeOfIdleState = 0f;
    //�@idle��Ԃŗ��܂鎞��
    [SerializeField]
    private float timeToStayInIdle = 5f;
    //�@�U���Ώۂ�Transform
    private Transform attackTargetTransform;
    //�@�U�����̑Ώۂ̈ʒu
    private Vector3 attackTargetPos;
    //�u���X�𔭎˂��Ă��邩�ǂ����̕ϐ�
    private bool IsBreath = false;
    //�e�̐����Ǘ����Ă����ϐ�
    private int BulletNum = 0;
    //�e�̍ő吔
    [SerializeField]
    private int MaxBulletNum = 5;
    //�e�̃I�u�W�F�N�g
    public BreathBall breathBall;
    //�A�̃I�u�W�F�N�g
    public Bubble bubble;
    //�J�j��HP
    [SerializeField]
    private float HitPoint = 50;
    //���ݒl
    [SerializeField]
    private float FrightenedNum = 10;
    //���݂̋��ݒl
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
        //�J�j�̏�Ԃɂ���ď�����ς���
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

        //�@���ʂ���CharacterController�̈ړ�����
        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        Vector3 pos = transform.position;
        pos.y -= 3.480005f;
        transform.position = pos;
    }

    //�ړI�n��ݒ肷��
    void SetRandomDestination()
    {
        //�ŏ��̈ʒu����L���͈͂̃����_���ʒu���擾
        var randomPos = defaltPos + Random.insideUnitSphere * movementRange;
        var ray = new Ray(randomPos + Vector3.up * 10f, Vector3.down);
        RaycastHit hit;
        //�ړI�n���n�ʂɂȂ�悤�ɍĐݒ�
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
            Debug.Log("�A�C�h��");
        }
        else if (crabState == CrabState.patrol)
        {
            //�A�j���[�V������ύX����
            animator.SetTrigger("Patrol");
            Debug.Log("�p�g���[��");
        }
        else if (crabState == CrabState.attack)
        {
            attackTargetTransform = playerTransform;
            attackTargetPos = attackTargetTransform.position;
            velocity = new Vector3(0f, velocity.y, 0f);
            animator.SetTrigger("Attack");
            animator.SetBool("Chase", false);
            Debug.Log("�ʏ�U��");
        }
        else if (crabState == CrabState.shockwaveAttack)
        {
            attackTargetTransform = playerTransform;
            attackTargetPos = attackTargetTransform.position;
            velocity = new Vector3(0f, velocity.y, 0f);
            animator.SetTrigger("ShockwaveAttack");
            animator.SetBool("Chase", false);
            Debug.Log("�Ռ��g�U��");
        }
        else if (crabState == CrabState.chase)
        {
            animator.SetBool("Chase", true);
            attackTargetTransform = playerTransform;
            Debug.Log("�`�F�C�X");
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
            Debug.Log("�u���X");
        }
        else if(crabState==CrabState.Die)
        {
            Debug.Log("��");
            animator.SetBool("Die", true);
        }
        else if(crabState==CrabState.Frightened)
        {
            Debug.Log("����");
            animator.SetTrigger("Frightened");
        }
    }

    //��Ԏ擾���\�b�h
    public CrabState GetStatus()
    {
        return crabState;
    }

    //Idle��Ԃ̎��̏���
    private void Idle()
    {
        elapsedTimeOfIdleState += Time.deltaTime;
        //��莞�Ԃ��o�߂�����patrol��Ԃɂ���
        if(elapsedTimeOfIdleState>=timeToStayInIdle)
        {
            elapsedTimeOfIdleState = 0.0f;
            SetStatus(CrabState.patrol);
        }
    }

    void Patrol()
    {
        //�@�ʏ�ړ�����
        if (characterController.isGrounded)
        {
            velocity = Vector3.zero;
            //�@�ړI�n�̕������v�Z���A������ς��đO���ɐi�߂�
            var direction = (destination - transform.position).normalized;
            animator.SetFloat("Move Speed", direction.magnitude);
            var targetRot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(destination - transform.position), Time.deltaTime * rotateSpeed);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, targetRot.eulerAngles.y, transform.eulerAngles.z);
            velocity = transform.forward * walkSpeed;
            transform.position = transform.forward * walkSpeed;
        }
        //�@�ړI�n�ɒ�������idle��Ԃɂ���
        if (Vector3.Distance(transform.position, destination) < 0.5f)
        {
            SetStatus(CrabState.idle);
            animator.SetBool("Patrol", false);
        }
    }

    //Chase��Ԏ��̏���
    void Chase()
    {
        //�ړI�n�𖈉�ݒ肵�Ȃ���
        destination = attackTargetTransform.position;
        //�ǂ������鏈��
        if(characterController.isGrounded)
        {
            velocity = Vector3.zero;
            //�@�ړI�n�̕������v�Z���A������ς��đO���ɐi�߂�
            var direction = (destination - transform.position).normalized;
            animator.SetFloat("Move Speed", direction.magnitude);
            var targetRot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(destination - transform.position), Time.deltaTime * rotateSpeed);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, targetRot.eulerAngles.y, transform.eulerAngles.z);
            velocity = transform.forward * chaseSpeed;
        }
    }

    void Attack()
    {
        //�U����ԂɂȂ������̃L�����N�^�[�̌������v�Z���A���X�ɂ�����̌����։�]������
        var targetRot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(attackTargetPos - transform.position),
                                          Time.deltaTime * 2f);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, targetRot.eulerAngles.y, transform.eulerAngles.z);

        //Attack�A�j���[�V�������I��������Idle��Ԃɂ���
        if(//animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")&&
           animator.GetCurrentAnimatorStateInfo(0).normalizedTime>=1f)
        {
            SetStatus(CrabState.idle);
        }
    }

    void ShockwaveAttack()
    {
        //�U����ԂɂȂ������̃L�����N�^�[�̌������v�Z���A���X�ɂ�����̌����։�]������
        var targetRot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(attackTargetPos - transform.position),
                                          Time.deltaTime * 2f);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, targetRot.eulerAngles.y, transform.eulerAngles.z);

        //Attack�A�j���[�V�������I��������Idle��Ԃɂ���
        if (//animator.GetCurrentAnimatorStateInfo(0).IsName("ShockwaveAttack")&& 
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            SetStatus(CrabState.idle);
        }
    }

    //�u���X�����Ƃ��̏���
    void Breath()
    {

        Bubble instance = (Bubble)Instantiate(bubble, transform);

        if (!IsBreath)
        {
            //�U����ԂɂȂ������̃L�����N�^�[�̌������v�Z���A���X�ɂ�����̌����։�]������
            var targetRot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(attackTargetPos - transform.position),
                                              Time.deltaTime * 2f);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, targetRot.eulerAngles.y, transform.eulerAngles.z);

        }
        else
        {
            //�ő唭�ː��o�Ȃ���Δ��˂���
            if (MaxBulletNum > BulletNum)
            {
                //�e�̐���
                BreathBall obj = (BreathBall)Instantiate(breathBall, transform);

                //�e�̐��𑝂₷
                BulletNum++;
            }
        }

        //�A�j���[�V�������I�������A�C�h���ɕύX
        if (//animator.GetCurrentAnimatorStateInfo(0).IsName("Breath")&& 
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            SetStatus(CrabState.idle);
        }
    }

    //�u���X�𔭎˂������ǂ����Ǘ����Ă���ϐ��̃Z�b�g
    public void SetIsBreath(bool flg)
    {
        IsBreath = flg;
    }

    //�_���[�W��H��������̏���
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
