using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lintest : MonoBehaviour {
    public Transform target;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	
        
        
        void Update()
        {
            if (Physics.Linecast(transform.position, target.position))
            {
                Debug.Log("blocked");
            }
        }
   
}

