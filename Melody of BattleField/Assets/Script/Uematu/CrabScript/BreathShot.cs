using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathShot : MonoBehaviour
{
    //�e�̃I�u�W�F�N�g
    public BreathBall breathBall;
    //�I�̃N���X
    public CrabScript crabScript;
    //�U����
    public AudioClip attackSound;
    //�u���X�̔��ˉ�
    public AudioClip FireSound;
    //�����Ǘ�����X�N���v�g
    public SoundManager soundManager;

    public void BreathFiring()
    {
        Debug.Log("�e����");

        //�e�̐���
        BreathBall obj = (BreathBall)Instantiate(breathBall, transform);
        //�e�𔭎˂�������CrabScriot�ɂ��`����
        crabScript.SetIsBreath(true);
        //�e�𔭎˂����Ƃ��ɂ������o��
        soundManager.SoundPlayOne(FireSound);
    }

    //�I���U�������Ƃ��ɌĂ΂�鏈��
    public void AttackEvent()
    {
        soundManager.SoundPlayOne(attackSound);
    }
}
