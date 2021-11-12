using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    private Animator animator;
    AnimatorStateInfo stateInfo;
    public bool animEnd;

    void Start()
    {
        animator = GetComponent<Animator>();

        animEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }

    public void AtackAnim()
    {
        animator.Play("Monster_anim|Atack_2");
        Debug.Log("aaaaaaaaa");
    }

    public void JumpAnim()
    {
        animator.Play("Jump");      
    }

    public void RunAnim()
    {
       animator.Play("Run");
       Debug.Log("Run");
    }
    public void HitAnim()
    {   
        animator.Play("Hit");
        Debug.Log("Hit");
    }

    public void AnimEnd()
    {
        animEnd = true;
        Debug.Log("HitAnimEnd");
    }

    public void ProtectAnim()
    {
        animator.Play("Protect");
        Debug.Log("Protect");
    }
    public void DeathAnim()
    {
        animator.Play("Death");
        Debug.Log("Death");
    }

    public void WalkAnim()
    {
        animator.Play("Walk");
        Debug.Log("wwwwww");
    }

    //dragon特有アニメーション//////////////////////////////////////////
    public void FireBallAnim()
    {
        animator.Play("FireBall");
        Debug.Log("FireBall");
    }
    
    public void FlyFireBallAnim()
    {
        animator.Play("FlyFireBall");
        Debug.Log("FlyFireBall");
    }
    
    public void TailAttackAnim()
    {
        animator.Play("TailAttack");
        Debug.Log("TailAttack");
    }
    
    public void SleepAnim()
    {
        animator.Play("Sleep");
        Debug.Log("Sleep");
    }
    
    public void TakeOffAnim()
    {
        animator.Play("TakeOff");
        Debug.Log("TakeOff");
    }


}
