using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class Generator : MonoBehaviour, IInteractable
{
    [SerializeField] private int secondsToAdd;
    [SerializeField] private GameObject key;
    [SerializeField] private TextMeshPro costText;
    [SerializeField] private int energyCost;

    private GameObject buttonKey;

    private IDisposable disposable;

    public delegate void GeneratorEvent(int secondsToAdd);
    public static GeneratorEvent AddtimeEvent;

    private void Start()
    {
        
    }

    private void TurnOn()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        costText.text = $"x{energyCost}";

        disposable = Observable.EveryUpdate().Subscribe(_ =>
        {
            if (Input.GetKeyDown(KeyCode.F) && CanInteract())
            {
                Interact();
            }
        });
    }

    private void TurnOff()
    {
        

        transform.GetChild(0).gameObject.SetActive(false);

        disposable.Dispose();
    }

    private bool CanInteract()
    {
        return PlayerInventory.Instance.CanTakeItem(Collectable.Type.Energy, energyCost);
    }

    public void Interact()
    {
        PlayerInventory.Instance.TakeItem(Collectable.Type.Energy, energyCost);
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
