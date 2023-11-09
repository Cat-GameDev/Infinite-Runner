using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementObject : GameUnit
{
    protected float moveSpeed;
    private Vector3 moveDir;
    private Vector3 destination;

    private void Update() 
    {
        moveSpeed = LevelManager.Instance.EvnMoveSpeed;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        if(!GameManager.Instance.IsState(GameState.Gameplay))
        {
            OnDespawn();
            return;
        }
        else if(Vector3.Dot((destination - transform.position).normalized, moveDir) < 0)
        {
            OnDespawn();
        }

    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
    }

    public void SetMoveDir(Vector3 moveDir)
    {
        this.moveDir = moveDir;
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    public virtual void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}
