using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//distrubutes coins randomly to world at startup

public class tresure : MonoBehaviour {
	public GameObject prefabgold;
	// Use this for initialization
	float x,z;
	float range=9.0f;
    int amountofgold=150;

	void Start () {
        
		
		
		for (int i = 0; i < amountofgold; i++)
		{
            x = transform.position.x;
            z = transform.position.y;


            x+= Random.Range (-range, range);
			z+= Random.Range (-range, range);
			Instantiate(prefabgold, new Vector3(x, 0.55f, z), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
