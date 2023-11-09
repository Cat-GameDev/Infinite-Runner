using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "ColorData")]
public class ColorData : ScriptableObject
{
    [SerializeField] Mesh[] meshCars;
    [SerializeField] Mesh[] meshBuidings;
    [SerializeField] Mesh[] meshTrains;

    public Mesh RamdonMeshCar()
    {
        int randomIndex = Random.Range(0, meshCars.Length);
        return meshCars[randomIndex];
    }

    public Mesh RamdonMeshBuidling()
    {
        int randomIndex = Random.Range(0, meshBuidings.Length);
        return meshBuidings[randomIndex];
    }

     public Mesh RandonMeshTrain()
    {
        int randomIndex = Random.Range(0, meshTrains.Length);
        return meshTrains[randomIndex];
    }
}
