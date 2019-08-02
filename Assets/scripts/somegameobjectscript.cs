using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class somegameobjectscript : MonoBehaviour {



    private StateMachine statemachine = new StateMachine();
    [SerializeField]
    float viewrange;
    [SerializeField]
    LayerMask layermask;
    [SerializeField]
    NavMeshAgent navmesh;
    [SerializeField]
    private string tagtofind;



    void Start()
    {
        this.navmesh = GetComponent<NavMeshAgent>();

        //this.statemachine.ChangeState(new SearchFor(this.layermask, this.gameObject, this.viewrange, this.tagtofind, this.navmesh));
        this.statemachine.ChangeState(new rotstate(this.gameObject));

        StartCoroutine(wait());
    }


   private void Update()
    {
        this.statemachine.ExecuteStateUpdate();
       



    }
    IEnumerator wait()
    {
        
        yield return new WaitForSeconds(5);
        this.statemachine.ChangeState(new SearchFor(this.layermask, this.gameObject, this.viewrange, this.tagtofind, this.navmesh));

       
    }

}




    