using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charmove : MonoBehaviour {

	[SerializeField]
	float movespeed=10f;

	Vector3 forward,right;
	// Use this for initialization
	void Start () {
		forward = Camera.main.transform.forward;
		forward.y = 0;
		forward = Vector3.Normalize (forward);
		right= Quaternion.Euler(new Vector3(0,90,0))*forward;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey)
		{
			move();
		}
	}
		void move()
		{
			//Vector3 direction=new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
			Vector3 rightmovement=right*movespeed*Time.deltaTime*Input.GetAxis("Horizontal");
			Vector3 upmove=forward*movespeed*Time.deltaTime*Input.GetAxis("Vertical");

			Vector3 heading=Vector3.Normalize(rightmovement+upmove);
			transform.forward=heading;
			transform.position+=rightmovement;
			transform.position+=upmove;
		}



	
}
