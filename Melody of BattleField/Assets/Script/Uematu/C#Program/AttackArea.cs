using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private CrabScript crabScript;
    private Animator crabAnimator;

    // Start is called before the first frame update
    void Start()
    {
        crabScript = GetComponentInParent<CrabScript>();
        crabAnimator = crabScript.GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player"
            && crabScript.GetStatus()!=CrabScript.CrabState.attack
            && !crabAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack")
            && crabScript.GetStatus() != CrabScript.CrabState.shockwaveAttack
            && !crabAnimator.GetCurrentAnimatorStateInfo(0).IsName("shockwaveAttack")
            && crabScript.GetStatus()!=CrabScript.CrabState.breath
            && !crabAnimator.GetCurrentAnimatorStateInfo(0).IsName("Breath"))
        {
            if(Random.value<=0.5f)
            {
                crabScript.SetStatus(CrabScript.CrabState.attack, other.transform);
            }
            else
            {
                crabScript.SetStatus(CrabScript.CrabState.shockwaveAttack, other.transform);
            }
        }
    }
}
