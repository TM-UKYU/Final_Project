using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // �ړ����x
    [SerializeField]
    private float speed = 5.0f;

    // �ړ���
    private Vector3 move;
    private float moveX = 0.0f;
    private float moveZ = 0.0f;

    // �J������]���x
    [SerializeField] private float applySpeed = 0.1f;

    // �J�����Q��
    [SerializeField] private TpsCamera refCamera;

    //Rigidbody
    private Rigidbody rbody;

    // ���C
    private Ray ray;
    private float rayDistance = 0.5f; // ���C�̒���
    private RaycastHit hit;           // ���C�Ƀq�b�g�������̏��
    private Vector3 rayPosition;      // ���C�̔��ˈʒu
    private bool IsGround;            // �������ɃI�u�W�F�N�g���邩�ǂ����̔���


    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody�擾
        rbody = this.transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        HitCheckGround();

        MovePlayer();

    }

    void MovePlayer()
    {
        // �v���C���[��Z������
        // �ړ��̕���(velocity)�ɏ��X�ɉ�]
        if (rbody.velocity != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation,
            //Quaternion.LookRotation(rbody.velocity),
            //Y�ړ��ʂ𖳎�
            Quaternion.LookRotation(Vector3.Scale(rbody.velocity, new Vector3(1, 0, 1)).normalized),
            applySpeed);
        // �ړ��ʂ̌v�Z(�L�[���� * �ړ����x)
        moveX = Input.GetAxis("Horizontal") * speed; // ���E
        moveZ = Input.GetAxis("Vertical") * speed; // �O��
                                                   // �J�����̕���(�J��������������Ă���ꍇ�ɔ���������������菜��)
        Vector3 cameraForward = Vector3.Scale(refCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(refCamera.transform.right, new Vector3(1, 0, 1)).normalized;
        // �J�����̕�������Ɉړ�����������
        move = cameraForward * moveZ + cameraRight * moveX;
        // �ړ��ʂ�������
        rbody.velocity = move;
    }

    void HitCheckGround()
    {
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
            rbody.useGravity = false; //�@�d�͂��I�t
        }
        else
        {
            // �d�͂��I��
            rbody.useGravity = true;
        }
    }
}
