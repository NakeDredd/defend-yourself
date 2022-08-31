using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public enum Type
    {
        Energy
    }

    [SerializeField] private Type type;

    public Type Type1 { get => type;}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerInventory playerInventory))
        {
            playerInventory.AddItem(type);
            Destroy(gameObject);
        }
    }
}
