using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotstate : IState {
    private GameObject ownergameobject;
    // Use this for initialization

    public rotstate(GameObject ownergameobject){
        this.ownergameobject=ownergameobject;
        }


public void Enter()
{

}

public void Execute()
{
        ownergameobject.transform.Translate(Vector3.forward * Time.deltaTime);
    }

public void Exit()
{

}

}
