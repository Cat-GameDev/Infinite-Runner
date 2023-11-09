using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStateMachine : MonoBehaviour
{
    StateMachine stateMachine = new StateMachine();

    
    void Start()
    {
        stateMachine.ChangeState(IdleState);
    }

   
    void Update()
    {
        stateMachine?.Execute();
    }

    private void IdleState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        onEnter = () =>
        {
            Debug.Log("Start Idle");
        };

        onExecute = () =>
        {
            Debug.Log("Execute Idle");
        };

        onExit = () =>
        {
            Debug.Log("Exit Idle");
        };
    }

    
    private void AttackState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        onEnter = () =>
        {

        };

        onExecute = () =>
        {

        };

        onExit = () =>
        {

        };
    }


}
