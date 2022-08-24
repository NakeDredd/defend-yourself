using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerAnimState
{
    Idle,
    Run,
    Run_End,
    Jump
}
public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    private PlayerMovement movement;


    private void Awake()
    {
        PlayerMovement.ChangeAnim += ChangeAnimState;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
    }
    private void OnDisable()
    {
        PlayerMovement.ChangeAnim -= ChangeAnimState;
    }

    private void Update()
    {
        if (rb.velocity.y == 0)
        {
            if (movement.MoveInput != 0)
            {
                ChangeAnimState(PlayerAnimState.Run);
            }
            else
            {
                ChangeAnimState(PlayerAnimState.Idle);
            }
        }
    }

    private void ChangeAnimState(PlayerAnimState newState)
    {
        switch (newState)
        {
            case PlayerAnimState.Idle:
                animator.Play("Idle");
                break;
            case PlayerAnimState.Run:
                animator.Play("Run");
                break;
            case PlayerAnimState.Run_End:
                animator.Play("Run_End");
                break;
            case PlayerAnimState.Jump:
                animator.Play("Jump");
                break;
            default:
                break;
        }
    }
}
