//--�J������TPS�Ǐ]�X�N���v�g--
// �v���C���[�ւ̒Ǐ]
// �}�E�X�ړ��ł̎��_��]

//--Unity������
// ��I�u�W�F�N�g�ɃX�N���v�g��ݒ�
// �X�N���v�g�ɒǏ]����v���C���[��ݒ�
// ���C���J��������I�u�W�F�N�g�̎q�ɐݒ�

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsCamera : MonoBehaviour
{
    // ���͒l(Inspector�Őݒ�)
    [SerializeField] Transform Player;  // �Ǐ]�Ώ�
    [SerializeField] float RotateSpeed; // ��]���x

    float side, forward;

    private void Start()
    {
        // ��]���x
        RotateSpeed = 1;
    }

    void Update()
    {
        // �v���C���[�ʒu��Ǐ]����
        transform.position = new Vector3(Player.position.x, transform.position.y, Player.position.z);

        // �}�E�X�ɂ���]
        side += Input.GetAxis("Mouse X") * RotateSpeed; // ����]����
        forward -= Input.GetAxis("Mouse Y") * RotateSpeed; // �c��]����

        forward = Mathf.Clamp(forward, -80, 60); //�c��]�̊p�x����

        transform.eulerAngles = new Vector3(forward, side, 0.0f); // ��]�̎��s
    }
}
