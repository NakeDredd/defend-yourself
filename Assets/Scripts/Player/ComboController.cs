using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboController : MonoBehaviour
{
    [SerializeField] private float comboTimerGround;
    [SerializeField] private float comboTimerAir;

    private Animator animator;
    private PlayerMovement movement;

    private int countOfAttacks;
    private float comboTime;

    public int CountOfAttacks { get => countOfAttacks; set => countOfAttacks = value; }

    public void PlayAttackAnimation(bool isAir)
    {
        if (isAir)
        {
            if (countOfAttacks + 1 > 2)
            {
                countOfAttacks = 0;
            }

            countOfAttacks++;

            switch (countOfAttacks)
            {
                case 1:
                    animator.SetTrigger("FirstAirAttack");
                    break;
                case 2:
                    animator.SetTrigger("SecondAirAttack");
                    break;
                    
            }
        }
        else
        {
            if (countOfAttacks + 1 > 3)
            {
                countOfAttacks = 0;
            }

            countOfAttacks++;

            switch (countOfAttacks)
            {
                case 1:
                    animator.SetTrigger("FirstAttack");
                    break;
                case 2:
                    animator.SetTrigger("SecondAttack");
                    break;
                case 3:
                    animator.SetTrigger("ThirdAttack");
                    break;
            }
        }
        comboTime = 0;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();

        countOfAttacks = 0;
        comboTime = comboTimerGround;
    }

    private void Update()
    {
        if (movement.IsGrounded())
        {
            if (comboTime <= comboTimerGround)
            {
                comboTime += Time.deltaTime;
            }
            else
            {
                comboTime = comboTimerGround;
                countOfAttacks = 0;
            }
        }else
        {
            if (comboTime <= comboTimerAir)
            {
                comboTime += Time.deltaTime;
            }
            else
            {
                comboTime = comboTimerAir;
                countOfAttacks = 0;
            }
        }
    }
}
