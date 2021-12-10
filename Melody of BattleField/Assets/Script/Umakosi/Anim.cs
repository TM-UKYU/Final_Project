using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    private Animator animator;
    AnimatorStateInfo stateInfo;


    void Start()
    {
        animator = GetComponent<Animator>();

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

    public float AnimEnd()
    {
        Debug.Log("AnimEnd");

        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
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
    
    public void FlyAnim()
    {
        animator.Play("Fly");
        Debug.Log("FlyAnim");
    }

    public void FlyFloatAnim()
    {
        animator.Play("Fly Float");
        Debug.Log("Fly Float");
    }

    public void FlyGlide()
    {
        animator.Play("FlyGlide");
        Debug.Log("Fly Glide");
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
    
    public void IdleAnim()
    {
        animator.Play("Idle");
        Debug.Log("Idle");
    }

    public void TakeOffAnim()
    {
        animator.Play("TakeOff");
        Debug.Log("TakeOff");
    }

    public void LandAnim()
    {
        animator.Play("Land");
        Debug.Log("Land");
    }

}
