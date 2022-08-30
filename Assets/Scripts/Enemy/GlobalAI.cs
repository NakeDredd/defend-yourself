using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAI : Singleton<GlobalAI>
{
    [SerializeField] private GameObject player;

    public GameObject Player { get => player; set => player = value; }
}
