using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private LayerMask attackLayermask;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;    

    private Animator anim;
    private PlayerMovement movement;
    private bool isCanAttack = true;

    public delegate void AttackCallBack();
    public static event AttackCallBack OnAttack;

    private void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isCanAttack)
        {
            if (movement.IsGrounded()) //ground attack
            {
                Attack();

                isCanAttack = false;

                
                float attackLength = anim.GetCurrentAnimatorStateInfo(0).length;
                Observable.Timer(TimeSpan.FromSeconds(attackLength+0.1)).Subscribe(_ =>
                {
                    isCanAttack = true;
                
                });
            }
        }
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");
        OnAttack.Invoke();
        Debug.Log("test");
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, attackLayermask.value);
        foreach (var hit in hits)
        {
            IDamagable obj = hit.GetComponent<IDamagable>();
            obj.ApplyDamage(damage);
        }
    }
}
