using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform TF;
    public Transform playerTF;

    [SerializeField] Vector3 offset;

    private void LateUpdate()
    {
        if(!GameManager.Instance.IsState(GameState.Gameplay))
            return;
        
        else if(playerTF != null)
        {
            TF.position = Vector3.Lerp(TF.position, playerTF.position + offset, Time.deltaTime * 5f);
        }
        else
        {
            playerTF = LevelManager.Instance.GetPlayer();
        }
        
    }
}
