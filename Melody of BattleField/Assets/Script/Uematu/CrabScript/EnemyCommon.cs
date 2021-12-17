using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommon : MonoBehaviour
{
    //�G��HP
    [SerializeField]
    private float HitPoint = 50;
    //���ݒl
    [SerializeField]
    private float FrightenedNum = 10;
    //���݂̋��ݒl
    private float NowFrightened = 0;
    //�����蔻��̃R���C�_�\
    public Collider HitCollider;

    void Start()
    {
        HitCollider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Weapon")
        {
            //�R�R�}�W�b�N�i���o�[�����ǌ�Ńv���C���[���Ǘ����Ă���U���͂ɕύX
            DecHP(10);
            //��ɓ���
            DecFrightened(1);
        }
    }

    //�_���[�W��H�������HP�̌v�Z
    public void DecHP(float Damage)
    {
        //HP�����炷
        HitPoint -= Damage;
    }

    public bool CheckDeath()
    {
        //HP��0�ȉ��ɂȂ�����True��Ԃ�
        if (HitPoint <= 0)
        {
            return true;
        }

        return false;
    }

    //�_���[�W��H��������̋��ݒl�̌v�Z
    private void DecFrightened(float DecNum)
    {
        //���ݒl�𑝉�������
        NowFrightened += DecNum;
    }

    public bool CheckFrightened()
    {
        //���ݒl�����l�𒴂�����True��Ԃ�
        if (FrightenedNum <= NowFrightened)
        {
            NowFrightened = 0;
            return true;
        }

        return false;
    }
}
