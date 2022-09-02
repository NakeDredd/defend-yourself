using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEditor;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private LayerMask attackLayermask;
    [SerializeField] private Transform attackPoint;

    private Animator anim;
    private PlayerMovement movement;
    private ComboController comboController;
    private int damage;
    private bool isCanAttack = true;
    private enum AttackType
    {
        GroundAttack,
        AirAttack,
        HeavyAttack
    }

    public delegate void AttackCallBack();
    public static event AttackCallBack OnAttack;

    private void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        comboController = GetComponent<ComboController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isCanAttack)
        {
            if (movement.IsGrounded()) //ground attack
            {
                Attack(AttackType.GroundAttack);

                isCanAttack = false;
                
                float attackLength = anim.GetCurrentAnimatorStateInfo(0).length;
                Observable.Timer(TimeSpan.FromSeconds(attackLength)).Subscribe(_ =>
                {
                    isCanAttack = true;
                
                });
            }else if(!movement.IsGrounded())
            {
                Attack(AttackType.AirAttack);

                isCanAttack = false;

                float attackLength = anim.GetCurrentAnimatorStateInfo(0).length;
                Observable.Timer(TimeSpan.FromSeconds(attackLength + 0.1)).Subscribe(_ =>
                {
                    isCanAttack = true;

                });
            }
        }
    }

    private void Attack(AttackType type)
    {
        switch (type)
        {
            case AttackType.GroundAttack:
                comboController.PlayAttackAnimation(false); 
                break;
            case AttackType.AirAttack:
                comboController.PlayAttackAnimation(true);
                break;
            case AttackType.HeavyAttack:
                break;
        }
        OnAttack.Invoke();
    }

    public void ApplyDamageToEnemy(float attackDistance)
    {
        RaycastHit2D[] hits = null;
        if (transform.rotation.y == 0)
        {
            hits = Physics2D.RaycastAll(attackPoint.position, Vector2.right, attackDistance, attackLayermask.value);
        }
        else
        {
            hits = Physics2D.RaycastAll(attackPoint.position, Vector2.left, attackDistance, attackLayermask.value);
        }

        if (hits == null)
        {
            return;
        }

        switch (comboController.CountOfAttacks)
        {
            case 1:
                damage = 10;
                break;
            case 2:
                damage = 20;
                break;
            case 3:
                damage = 30;
                break;
            default:
                break;
        }

        foreach (var hit in hits)
        {
            IDamagable obj = hit.collider.GetComponent<IDamagable>();
            obj.ApplyDamage(damage);
        }
    }
}
