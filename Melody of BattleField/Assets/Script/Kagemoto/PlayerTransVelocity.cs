//--�v���C���[�̈ړ��X�N���v�g--
// ���E�ړ��E�W�����v(Rigidbody.velocity)
// �ړ������ւ̉�](Quaternion.Slerp)
// �n�ʂƂ̓����蔻��(RayCast)

//--Unity������
// �X�N���v�g��TPS�J������ݒ�

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransVelocity: MonoBehaviour
{
    private Rigidbody rbody;

    // �ړ����x
    private float speed = 5.0f;

    // �ړ���
    private Vector3 move;
    private float moveX = 0.0f;
    private float moveZ = 0.0f;

    // �W�����v
    private float moveY = 1.0f;  // �W�����v�̏���
    private bool IsGround = true;// �n�ʔ���
    private bool isJump = false; // �W�����v����
    private bool maxJumpFlag = false; // �ō����ɒB������
    private int offJumpClock = 0; // �ō������Ԃ̃J�E���^

    // ���C
    private Ray ray;
    private  float rayDistance = 0.5f; // ���C�̒���
    private  RaycastHit hit;           // ���C�Ƀq�b�g�������̏��
    private Vector3 rayPosition;      // ���C�̔��ˈʒu

    // ��]���x
    [SerializeField] private float applySpeed = 0.1f;

    // �J�����Q�Ɨp�ϐ�(Inspector�ŎQ�Ƃ���J�������w�肷��)
    [SerializeField] private TpsCamera refCamera;

    void Start()
    {
        // Rigitbody�̎擾
        rbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // �ړ������ɉ�]
        {
            // �v���C���[�̉�](transform.rotation)
            // �v���C���[��Z������
            // �ړ��̕���(velocity)�ɏ��X�ɉ�]
            if (rbody.velocity != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      //Quaternion.LookRotation(rbody.velocity),
                                                      //Y�ړ��ʂ𖳎�
                                                      Quaternion.LookRotation(Vector3.Scale(rbody.velocity, new Vector3(1, 0, 1)).normalized),
                                                      applySpeed);

        }

        // �J���������ɉ�]
        {
            // �v���C���[�̉�](transform.rotation)
            // �v���C���[��Z������
            // �J�����̐�����]����(refCamera.transform.rotation)�ɏ��X�ɉ�]
            /*
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(refCamera.transform.rotation * rbody.velocity),
                                                  applySpeed);
            */
        }

        // �J�����̕����ɉ�]
        //transform.eulerAngles = refCamera.transform.eulerAngles;

        // �ړ��ʂ̌v�Z(�L�[���� * �ړ����x)
        moveX = Input.GetAxis("Horizontal") * speed; // ���E
        moveZ = Input.GetAxis("Vertical") * speed;   // �O��

        // �J�����̕���(�J��������������Ă���ꍇ�ɔ���������������菜��)
        Vector3 cameraForward = Vector3.Scale(refCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(refCamera.transform.right, new Vector3(1, 0, 1)).normalized;

        // �J�����̕�������Ɉړ�����������
        move = cameraForward * moveZ + cameraRight * moveX;

        // ���C�̒n�ʔ���
        rayPosition = transform.position + new Vector3(0.0f, 0.5f, 0.0f); // ���C�̒������v���C��-���W���畂������
        ray = new Ray(rayPosition, transform.up * -1); //���C�����ɔ���
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red); //���C��ԐF�\��

        if (Physics.Raycast(ray, out hit, rayDistance)) // ���C�������������̏���
        {
            IsGround = true; // �n�ʂɐG�ꂽ���Ƃɂ���

        }
        else
        {
            IsGround = false; // �n�ʂɐG��ĂȂ����Ƃɂ���

        }
        // �n�ʂɐG��Ă�����
        if (IsGround)
        {
            // �W�����v�ϐ��֘A
            {
                maxJumpFlag = false;
                moveY = 1.0f;
                offJumpClock = 0;
                isJump = false;
            }

            rbody.useGravity = false; //�@�d�͂��I�t
            // �W�����v
            if (Input.GetKey(KeyCode.Space))
            {
                isJump = true;
                IsGround = false;// �n�ʔ���I�t
            }
        }
        else
        {
            // �ڒn�Ȃ�&�W�����v���ł͂Ȃ�
            if (!isJump)
            {
                // �d�͂��I��
                rbody.useGravity = true;
            }
        }

        // �W�����v�t���O
        if (isJump) { Jump(); }

        // �ړ��ʂ�������
        rbody.velocity = move;
    }

    // �W�����v�֐�(��)
    void Jump()
    {
        move = new Vector3(move.x, moveY, move.z);// �ړ��ʂɃW�����v�͂�������
        if (maxJumpFlag == false)
        { // �ō����ɒB���Ă��Ȃ��Ȃ�
            if (moveY <= 3.0f) // �ō����x�ȉ��Ȃ�
            {
                moveY += 0.1f; // ����
            }
            else
            {
                maxJumpFlag = true;
            }
        }
        // �ō����ɒB�����Ȃ�
        else
        {
            // �J�E���g
            offJumpClock++;
            // 60�t���[���o�߂�����
            if (offJumpClock > 60)
            {
                if (moveY >= 1.0f)
                { // �Œᑬ�x�ȏ�Ȃ�
                    moveY -= 0.05f; // ����
                }
                else
                {
                    // �����ɖ߂�����W�����v�I��
                    isJump = false;
                }
            }
        }
    }
}
