using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathShot : MonoBehaviour
{
    //�e�̃I�u�W�F�N�g
    public BreathBall breathBall;
    //�I�̃N���X
    public CrabScript crabScript;

    public void BreathFiring()
    {
        Debug.Log("�e����");

        //�e�̐���
        BreathBall obj = (BreathBall)Instantiate(breathBall, transform);
        //�e�𔭎˂�������CrabScriot�ɂ��`����
        crabScript.SetIsBreath(true);
    }
}
