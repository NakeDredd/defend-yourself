using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private Image hpBar;
    [SerializeField] private TextMeshProUGUI hpText;

    [SerializeField] private int currentHp;

    public delegate void PlayerHPEvent();
    public static PlayerHPEvent Death;

    public void ApplyDamage(int damage)
    {
        if (currentHp - damage <= 0)
        {
            currentHp = 0;
            Death();
        }
        else
        {
            currentHp -= damage;
        }
        UpdateGUIHP();
    }

    private void Start()
    {
        currentHp = maxHp;
        hpText.text = $"{currentHp}/{maxHp}";
    }

    private void UpdateGUIHP()
    {
        hpText.text = $"{currentHp}/{maxHp}";
        float onePerc = maxHp / 100;
        float currPerc = currentHp / onePerc / 100;
        hpBar.fillAmount = currPerc;
    }

}
