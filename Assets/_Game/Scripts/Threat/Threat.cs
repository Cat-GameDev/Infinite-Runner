using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Threat : MovementObject
{
    [SerializeField] protected float spawnInternal = 2f;
    public float SpawnInternal { get => spawnInternal; }
}
