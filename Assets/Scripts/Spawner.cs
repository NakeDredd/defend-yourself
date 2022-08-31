using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject monster;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;

    private float currentSpawnticktime;

    private void Start()
    {
        currentSpawnticktime = Random.Range(minTime, maxTime);

        Observable.Interval(TimeSpan.FromSeconds(currentSpawnticktime)).Subscribe(_ =>
        {
            SpawnMonster();
            currentSpawnticktime = Random.Range(minTime, maxTime);
        });
    }

    private void SpawnMonster()
    {
        Instantiate(monster, transform);
    }
}
