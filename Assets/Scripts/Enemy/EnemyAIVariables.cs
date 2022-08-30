using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIVariables : MonoBehaviour
{
    public float speed;
    public int maxHp;
    public float attackRaidus;
    public int damage;
    public float attackCooldown;

    public int currentHp;

    public void InitVariables()
    {
        currentHp = maxHp;
    }
}
