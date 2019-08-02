using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchFor : IState {


    private LayerMask searchlayer;
    private GameObject ownergameobject;
    private float searchradius;
    private string searchTag;
    private NavMeshAgent navmeshagent;


    public SearchFor(LayerMask searchlayer, GameObject ownergameobject, float searchradius,string searchTag, NavMeshAgent navmeshagent)//constructor
{
        this.searchlayer = searchlayer;
        this.ownergameobject= ownergameobject;

        this.searchradius=searchradius;

        this.searchTag= searchTag;

        this.navmeshagent= navmeshagent;
}



    public void Enter()
    {
    }

    public void Execute()
    {
        var hitobjects = Physics.OverlapSphere(this.ownergameobject.transform.position, searchradius);
        //Debug.Log("overlap searching.."+ this.searchTag);
        for (int i=0; i < hitobjects.Length; i++)
        {
            if (hitobjects[i].CompareTag(this.searchTag))
            { 
            
                this.navmeshagent.SetDestination(hitobjects[i].transform.position);

                break;
            }
            
        }
        

    }

    public void Exit()
    {
    }

   // public class searchresult
    //{
    //    collider[] allhitobjectsinmyserchradious;
    //    public list collider allhitobject with tag;

   // }
         //constructor
//method call to process data







}
