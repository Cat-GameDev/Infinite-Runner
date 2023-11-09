using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMutiType : Object
{
    [SerializeField] protected ColorData colorData;
    [SerializeField] protected MeshFilter meshFilter;

    public void RandomMeshBuilding()
    {
        meshFilter.mesh = colorData.RamdonMeshBuidling();
    }
}
