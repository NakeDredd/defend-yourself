using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private int maxHealth;
    [SerializeField] private float afterDeathTime;

    private int currentHealth;
    private Animator anim;

    public int MaxHealth { get => maxHealth; }

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void ApplyDamage(int damage)
    {
        if (CurrentHealth - damage <= 0)
        {
            Death();
        }
        else
        {
            CurrentHealth -= damage;
            anim.SetTrigger("Damaged");
        }

    }

    public void Death()
    {
        anim.SetTrigger("Death");
        this.GetComponent<Collider2D>().enabled = false;
        Observable.Timer(TimeSpan.FromSeconds(afterDeathTime)).Subscribe(_ => 
        { 
            Destroy(this.gameObject);
        });
    }
}
