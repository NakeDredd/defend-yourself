using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public int MaxHealth { get;}
    public int CurrentHealth { get; set; }
    public abstract void ApplyDamage(int damage);
    public abstract void Death();
}
