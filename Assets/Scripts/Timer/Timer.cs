using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class Timer : MonoBehaviour
{
    [SerializeField] private int currentTime;
    [SerializeField] private TextMeshProUGUI timeText;

    private IDisposable disposable;

    private void Start()
    {
        Generator.AddtimeEvent += AddTime;

        StartTimer();
        UpdateText();
    }

    private void OnDisable()
    {
        Generator.AddtimeEvent -= AddTime;
    }

    private void UpdateText()
    {
        timeText.text = currentTime.ToString();
    }

    private void StartTimer()
    {
        if (disposable != null)
            disposable.Dispose();

        UpdateText();
        disposable = Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(_ =>
        {
            currentTime--;
            UpdateText();
        });
    }

    private void AddTime(int secondsToAdd)
    {
        currentTime += secondsToAdd;
        StartTimer();
    }
}
