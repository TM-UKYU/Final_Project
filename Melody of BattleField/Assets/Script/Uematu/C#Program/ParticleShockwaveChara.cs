using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleShockwaveChara : MonoBehaviour
{

    public enum State
    {
        normal,
        damage
    }

    private CharacterController characterController;
    private Animator animator;
    private Vector3 velocity;
    [SerializeField]
    private float walkSpeed = 2f;
    [SerializeField]
    private float jumpPower = 7f;
    private State state;

    // Use this for initialization
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        velocity = Vector3.zero;
        state = State.normal;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.normal)
        {
            if (characterController.isGrounded)
            {
                velocity = Vector3.zero;

                var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

                if (input.magnitude > 0.1f)
                {
                    transform.LookAt(transform.position + input.normalized);
                    animator.SetFloat("Speed", input.magnitude);
                    velocity = transform.forward * walkSpeed;
                }
                else
                {
                    animator.SetFloat("Speed", 0f);
                }

                if (Input.GetButtonDown("Jump"))
                {
                    velocity.y += jumpPower;
                }

            }
        }
        else if (state == State.damage)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Damage")
                && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                SetState(State.normal);
            }
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    public void Damage()
    {
        animator.SetTrigger("Damage");
        velocity = new Vector3(0f, velocity.y, 0f);
        SetState(State.damage);
    }

    public void SetState(State tempState)
    {
        state = tempState;
    }

    public State GetState()
    {
        return state;
    }
}