using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Object
{

    [SerializeField] private int amountCoinInLine;
    [SerializeField] private float gap;
    [SerializeField] private float spawnInternalCoin = 2f;

    [SerializeField] private float radiusY = 2.15f; 
    [SerializeField] private float radiusZ = 4f; 
    private float startAngle = 0f;
    private float endAngle = 180f;
    public float SpawnInternalCoin { get => spawnInternalCoin; }


    public Coin SpawnCoin(Vector3 spawPosition, Vector3 moveDirection, Transform endPoint, float moveSpeed)
    {
        Coin newCoin = null;
        for(int i =0; i< amountCoinInLine; i++)
        {
            Vector3 newSpawnPoint = spawPosition + transform.forward * gap * i;
            newCoin = SimplePool.Spawn<Coin>(PoolType.Coin, newSpawnPoint, Quaternion.identity);
            newCoin.SetMoveSpeed(moveSpeed);
            newCoin.SetDestination(endPoint.position);
            newCoin.SetMoveDir(moveDirection);
        }
        return newCoin;
    }

    //coin hinh vong cung
    public Coin SpawnCoinArcShape(Vector3 spawPosition, Vector3 moveDirection, Transform endPoint, float moveSpeed)
    {
        Coin newCoin = null;
        float angleStep = (endAngle - startAngle) / amountCoinInLine;

        for (int i = 0; i < amountCoinInLine; i++)
        {
            float angle = startAngle + i * angleStep;
            float radian = angle * Mathf.Deg2Rad;
            Vector3 newSpawnPoint = spawPosition + new Vector3(0f, radiusY * Mathf.Sin(radian), radiusZ * Mathf.Cos(radian));
            newCoin = SimplePool.Spawn<Coin>(PoolType.Coin, newSpawnPoint, Quaternion.identity);;
        }
       

        newCoin.SetMoveSpeed(moveSpeed);
        newCoin.SetDestination(endPoint.position);
        newCoin.SetMoveDir(moveDirection);
        return newCoin;
    }






}
