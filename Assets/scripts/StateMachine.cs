using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{

    private IState CurrentRunningState;
    private IState PreviousState;

    public void ChangeState(IState newstate)
    {

        if (this.CurrentRunningState != null)
        {

            this.CurrentRunningState.Exit();
        }

        this.PreviousState = this.CurrentRunningState;
        this.CurrentRunningState = newstate;
        this.CurrentRunningState.Enter();

    }

    public void ExecuteStateUpdate()
    {
        var runningstate = this.CurrentRunningState;

        if (runningstate != null)
        {
            this.CurrentRunningState.Execute();
           // Debug.Log("called Execute");
        }




    }

    public void SwitchToPreviousState()
    {
        this.CurrentRunningState.Exit();

        CurrentRunningState = this.PreviousState;
        this.CurrentRunningState.Enter();
    }
}