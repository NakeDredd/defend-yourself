using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private int maxHealth;

    private int currentHealth;

    public int MaxHealth { get => maxHealth; }

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }
}
