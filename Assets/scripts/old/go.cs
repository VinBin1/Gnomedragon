using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class go : MonoBehaviour {
    NavMeshAgent navo;
    public Transform target;
	// Use this for initialization
	void Start () {
        navo = GetComponent<NavMeshAgent>();


    }
	
	// Update is called once per frame
	void Update () {
        navo.SetDestination(target.position);

    }
}
