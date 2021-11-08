using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseChara : MonoBehaviour
{
    private CrabScript crabScript;

    // Start is called before the first frame update
    void Start()
    {
        crabScript = GetComponentInParent<CrabScript>();
    }

    private void OnTriggerStay(Collider other)
    {
        //�L�����N�^�[���͈͓��ɓ�������ǂ�������
        if(other.tag=="Player"
            && crabScript.GetStatus()!=CrabScript.CrabState.chase
            && crabScript.GetStatus()!= CrabScript.CrabState.attack
            && crabScript.GetStatus()!= CrabScript.CrabState.shockwaveAttack
            && crabScript.GetStatus() != CrabScript.CrabState.breath)
        {
            if (Random.value < 0.8f)
            {
                crabScript.SetStatus(CrabScript.CrabState.chase, other.transform);
            }
            else
            {
                crabScript.SetStatus(CrabScript.CrabState.breath, other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Player"
            && crabScript.GetStatus() == CrabScript.CrabState.chase)
        {
            crabScript.SetStatus(CrabScript.CrabState.idle);
        }
    }
}
