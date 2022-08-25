using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float vision;
    [SerializeField] private LayerMask visionLayerMask;
    [SerializeField] private Transform visionPos;
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRadius;

    private Rigidbody2D rb;
    private Animator anim;
    private PlayerHP player;
    private IDisposable disposable;


    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool dead = false;

    public void ApplyDamageToPlayer()
    {
        player.ApplyDamage(damage);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        RaycastHit2D rightZone = Physics2D.Raycast(visionPos.position, Vector2.right, vision, visionLayerMask);
        RaycastHit2D leftZone = Physics2D.Raycast(visionPos.position, Vector2.left, vision, visionLayerMask);
        Vector2 moveVector = Vector2.zero;
        if (dead == false && !isAttacking)
        {
            if (leftZone)
            {
                moveVector = new Vector2(-1 * speed * Time.fixedDeltaTime, rb.velocity.y);
            }
            else if (rightZone)
            {
                moveVector = new Vector2(1 * speed * Time.fixedDeltaTime, rb.velocity.y);
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
        Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRadius, visionLayerMask.value);
        if (hit != null)
        {
            Debug.Log(hit.name);
            player = hit.GetComponent<PlayerHP>();
            //Attack();
            disposable = Observable.Interval(TimeSpan.FromSeconds(attackCooldown)).Subscribe(_ =>
            {
                Attack();
                Debug.Log("ebat vse");
            });
        }
        else
        {
            disposable?.Dispose();
            
        }
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.TryGetComponent(out PlayerHP player))
    //    {
    //        this.player = player;

    //        anim.SetTrigger("Attack");
    //        disposable = Observable.Interval(TimeSpan.FromSeconds(attackCooldown)).Subscribe(_ =>
    //        {
    //            anim.SetTrigger("Attack");
    //        });
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.TryGetComponent(out PlayerHP _))
    //    {
    //        disposable?.Dispose();
    //        this.player = null;
    //    }
    //}

}