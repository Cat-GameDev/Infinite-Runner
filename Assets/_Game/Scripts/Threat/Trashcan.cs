using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Trashcan : ThreatOnlyType
{

    public Trashcan SpawnTrashcan(Vector3 spawPosition, Vector3 moveDirection, Transform endPoint, float moveSpeed)
    {
        Trashcan newTrashcan = SimplePool.Spawn<Trashcan>(PoolType.Trashcan, spawPosition , Quaternion.identity);
        newTrashcan.SetMoveSpeed(moveSpeed);
        newTrashcan.SetDestination(endPoint.position);
        newTrashcan.SetMoveDir(moveDirection);
        return newTrashcan;
    }


}
