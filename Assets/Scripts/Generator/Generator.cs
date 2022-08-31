using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Generator : MonoBehaviour, IInteractable
{
    [SerializeField] private int secondsToAdd;
    [SerializeField] private GameObject key;

    private GameObject buttonKey;

    private IDisposable disposable;

    public delegate void GeneratorEvent(int secondsToAdd);
    public static GeneratorEvent AddtimeEvent;

    private void Start()
    {
        
    }

    private void TurnOn()
    {
        //buttonKey = Instantiate(key);

        disposable = Observable.EveryUpdate().Subscribe(_ =>
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Interact();
            }
        });
    }

    private void TurnOff()
    {
        //Destroy(buttonKey);

        disposable.Dispose();
    }

    public void Interact()
    {
        AddtimeEvent.Invoke(secondsToAdd);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerHP _))
        {
            TurnOn();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerHP _))
        {
            TurnOff();
        }
    }
}
