using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float vision;
    [SerializeField] private LayerMask visionLayerMask;
    [SerializeField] private Transform visionPos;

    private Rigidbody2D rb;
    private Animator anim;

    public bool dead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        RaycastHit2D rightZone = Physics2D.Raycast(visionPos.position, Vector2.right, vision, visionLayerMask);
        RaycastHit2D leftZone = Physics2D.Raycast(visionPos.position, Vector2.left, vision, visionLayerMask);
        Vector2 moveVector = Vector2.zero;
        if (dead == false)
        {
            if (leftZone)
            {
                moveVector = new Vector2(-1, rb.velocity.y * speed * Time.fixedDeltaTime);
            }
            else if (rightZone)
            {
                moveVector = new Vector2(1, rb.velocity.y * speed * Time.fixedDeltaTime);
            }
            rb.velocity = moveVector;
            if (rightZone || leftZone)
            {
                anim.SetBool("Walk", true);
                anim.SetBool("Idle", false);
            }
            else
            {
                anim.SetBool("Walk", false);
                anim.SetBool("Idle", true);
            }
        }
    }
}