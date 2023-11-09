using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInfinate : MonoBehaviour
{
    private void OnTriggerExit(Collider other) 
    {
        if(!GameManager.Instance.IsState(GameState.Gameplay))
            return;
        
        else if(other.CompareTag("Road"))
        {
            Object newBlock = LevelManager.Instance.SpawnNewBlock(other.transform.position, LevelManager.Instance.MoveDirection);
            float newBlockHalfWidth = newBlock.GetComponent<BoxCollider>().bounds.size.z/2f;
            float prevoiusBlockHalfWidth = other.GetComponent<BoxCollider>().bounds.size.z/2f;

            Vector3 newBlockSpawnOffset = -(newBlockHalfWidth + prevoiusBlockHalfWidth) * LevelManager.Instance.MoveDirection;
            newBlock.transform.position += newBlockSpawnOffset;
            LevelManager.Instance.SpawnBuilding(other.transform.position, LevelManager.Instance.MoveDirection);
            LevelManager.Instance.SpawnStreetLight(other.transform.position, LevelManager.Instance.MoveDirection);

        }
    }
}
