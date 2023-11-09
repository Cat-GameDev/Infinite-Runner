using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : ThreatMutiType
{
    [SerializeField] private Vector2 amountTrainTail;
    [SerializeField] private BoxCollider boxCollider;
    //TO DO random amountTrainTail
    [SerializeField] Mesh headTrain;
    [SerializeField] Coin coin;

    public Train SpawnTrain(Vector3 spawPosition, Vector3 moveDirection, Transform endPoint, float moveSpeed)
    {
        Train newTrain = null;
        int bodyCount = Random.Range((int)amountTrainTail.x, (int)amountTrainTail.y);
        for(int i =0; i<bodyCount; i++)
        {
            Vector3 newSpawnPoint = spawPosition + transform.forward * 3 * i;
            
            if(i==0)
            {
                newTrain = SimplePool.Spawn<Train>(PoolType.Train, newSpawnPoint, Quaternion.identity);
                newTrain.meshFilter.mesh = headTrain;
                RandomCoin(spawPosition, moveDirection, endPoint, moveSpeed);
            }
            else
            {
                newTrain = SimplePool.Spawn<Train>(PoolType.Train, newSpawnPoint, Quaternion.identity);
                newTrain.RandomMeshTrain();
            }
            newTrain.SetMoveSpeed(moveSpeed);
            newTrain.SetDestination(endPoint.position);
            newTrain.SetMoveDir(moveDirection);
        }
        return newTrain;
    }

    
    private void RandomCoin(Vector3 spawPosition, Vector3 moveDirection, Transform endPoint, float moveSpeed)
    {
        int randomIndex = Random.Range(0,2);
        if(randomIndex==1)
        {
            coin.SpawnCoin(spawPosition + new Vector3(0f,1.8f,0f), moveDirection, endPoint, moveSpeed);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Threat") || other.CompareTag("Trashcan"))
        {
            Object objComponent = Cache.GetObject(other);
            objComponent.OnDespawn();
        }
        
    }


}
