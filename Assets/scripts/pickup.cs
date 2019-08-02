using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour {
	score scorep1;
	GameObject player1;
	GameObject player2;
	GameObject scripobj;
    GameObject playerobj;
    AudioSource clip;
    score scorescript;
    GameObject thedragon;
    dragonscr dragonscr_ref;
    // Use this for initialization
    void Awake() 
	{
		
		scripobj=GameObject.Find("aaascriptobj");
       scorescript= scripobj.GetComponent<score>();

        clip = scripobj.GetComponent<AudioSource>();
        //scorep1 = player1.GetComponent<score>();
        thedragon = GameObject.FindGameObjectWithTag("Dragon");
        dragonscr_ref = thedragon.GetComponent<dragonscr>();
    }

	void OnTriggerEnter(Collider other)
	{
		//print("pickup trig");

		if (other.tag == "hand") 
		{
			
            playerobj = other.transform.root.gameObject;
            scorescript.updatescore(playerobj);

            clip.pitch = Random.Range(0.9f, 1.1f);
            clip.Play();
            //dragonscr_ref.soundmade(gameObject);
            Destroy (gameObject);
		}

	}

}
