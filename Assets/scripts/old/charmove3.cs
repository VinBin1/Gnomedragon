using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charmove3 : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	
		
		var x2 = Input.GetAxis("Horizontal2") * Time.deltaTime * 250.0f;
		var z2 = Input.GetAxis("Vertical2") * Time.deltaTime * 3.0f;

		transform.Rotate(0, x2, 0);
		transform.Translate(0, 0, z2);
	
		}



	
}
