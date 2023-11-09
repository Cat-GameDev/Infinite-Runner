using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatMutiType : ObjectMutiType 
{
    [SerializeField] protected float spawnInternal = 2f;
    public float SpawnInternal { get => spawnInternal; }
    public void RandomMeshCar()
    {
        meshFilter.mesh = colorData.RamdonMeshCar();
    }

    public void RandomMeshTrain()
    {
        meshFilter.mesh = colorData.RandonMeshTrain();
    }
}
