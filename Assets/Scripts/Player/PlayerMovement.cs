using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Security.Cryptography;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int jumpForce;
    [SerializeField] private float characterHeight;
    [SerializeField] private float coyoteTime;

    [SerializeField] private LayerMask groundCheck;

    private Rigidbody2D rb;
    private Animator anim;
    private int currentSpeed;

    private float moveInput;
    private float coyoteTimeCounter;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        PlayerCombat.OnAttack += SlowPlayer;
        currentSpeed = speed;
    }

    private void OnDisable()
    {
        PlayerCombat.OnAttack -= SlowPlayer;
    }

    private void FixedUpdate()
    {
        //Animtions
        anim.SetFloat("yVelocity", rb.velocity.y);
        //Animtions
        Move();

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.fixedDeltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && coyoteTimeCounter > 0f)
        {
            Jump(true);

        }
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            Jump(false);
            coyoteTimeCounter = 0f;
        }
    }

    private void Move()
    {
        moveInput = Input.GetAxis("Horizontal") * currentSpeed;
        //Animtions
        anim.SetBool("isMove", moveInput != 0);
        //Animtions
        Vector2 moveVector = new Vector2(moveInput * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = moveVector;
        if (moveInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }

    private void Jump(bool isCoyote)
    {
        //Animtions
        anim.SetTrigger("Jump");
        //Animtions
        if (isCoyote)
        {
            Vector2 jumpVector2 = new Vector2(rb.velocity.x, jumpForce);
            rb.velocity = jumpVector2;
        }else
        {
            Vector2 jumpVector2 = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }
    
    public bool IsGrounded()
    {
        bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, characterHeight, groundCheck.value).collider != null;
        //Animtions
        anim.SetBool("isGrounded", isGrounded);
        //Animtions
        return isGrounded; 
    }

    private void SlowPlayer()
    {
        currentSpeed = 100;
        Observable.Timer(TimeSpan.FromSeconds(0.2f)).Subscribe(_ =>
        {
            currentSpeed = speed;
        });
    }
}
