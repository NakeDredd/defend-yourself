using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class PlayerDeathController : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI timeText;

    private void Start()
    {
        PlayerHP.Death += DeathWithHp;
        Timer.TimeUp += DeathWithTime;
    }

    private void OnDisable()
    {
        PlayerHP.Death -= DeathWithHp;
        Timer.TimeUp -= DeathWithTime;
    }

    private void DeathWithTime()
    {
        Death(true);
    }

    private void DeathWithHp()
    {
        Death(false);
    }

    private void Death(bool isTime)
    {
        deathPanel.SetActive(true);
        if (isTime)
        {
            timeText.gameObject.SetActive(true);
        }
        else
        {
            hpText.gameObject.SetActive(true);
        }

        Observable.Timer(TimeSpan.FromSeconds(0)).Subscribe(_ =>
        {
            Destroy(this.gameObject);
        });
    }
}
