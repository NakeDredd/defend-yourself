using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int jumpForce;
    [SerializeField] private float characterHeight;
    [SerializeField] private float coyoteTime;

    [SerializeField] private LayerMask groundCheck;

    private Rigidbody2D rb;

    private float moveInput;
    private float coyoteTimeCounter;

    public float MoveInput { get => moveInput;}

    public delegate void PlayerAnimationsCallback(PlayerAnimState newState);
    public static event PlayerAnimationsCallback ChangeAnim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
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
        moveInput = Input.GetAxis("Horizontal") * speed;
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
        ChangeAnim(PlayerAnimState.Jump);

        if (isCoyote)
        {
            Vector2 jumpVector2 = new Vector2(rb.velocity.x, jumpForce);
            rb.velocity = jumpVector2;
        }else
        {
            Vector2 jumpVector2 = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }
    
    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, characterHeight, groundCheck.value).collider != null; 
    }
}
