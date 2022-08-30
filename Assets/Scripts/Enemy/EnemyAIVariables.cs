using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIVariables : MonoBehaviour
{
    public float speed;
    public int maxHp;

    public int currentHp;

    public void InitVariables()
    {
        currentHp = maxHp;
    }
}
