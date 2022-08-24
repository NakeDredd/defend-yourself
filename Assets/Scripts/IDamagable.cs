using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public int MaxHealth { get;}
    public int CurrentHealth { get; set; }

    public virtual void ApplyDamage(int damage)
    {
        if (CurrentHealth - damage <= 0)
        {
            Death();
        }else
        {
            CurrentHealth -= damage;
        }

    }

    private void SetHP()
    {
        CurrentHealth = MaxHealth;
    }

    public abstract void Death();
}
