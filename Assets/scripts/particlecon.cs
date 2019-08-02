using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlecon : MonoBehaviour {
	int rannum;
	bool waiting,canfire=false;
	 bool fireon=false;
    // Use this for initialization
    Animator draganocon;
    GameObject dragonobj;

    void Start () {
        dragonobj = GameObject.Find("firedragonbase2");
        draganocon = dragonobj.GetComponent<Animator>();
    }
    public void fireonnow()
    { fireon = true;
        print("calling fire");
        draganocon.enabled = false;
       // gameObject.GetComponent<ParticleSystem>().enableEmission = true;
        StartCoroutine(wait(2.0f));
     


    }

    public void fireoffnow()
    { fireon = false; }




    // Update is called once per frame
    void Update () {
		
		
/*
		if (fireon == true) {
			gameObject.GetComponent<ParticleSystem> ().enableEmission = true;
		} else {
			gameObject.GetComponent<ParticleSystem> ().enableEmission = false;
		}
*/
      

	}

	IEnumerator wait(float somesec)
	{
        print("Waiting...");
		yield return new WaitForSeconds (somesec);
		waiting = false;
        //gameObject.GetComponent<ParticleSystem>().enableEmission = false;
        draganocon.enabled = true;
    }
}
