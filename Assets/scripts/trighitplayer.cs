using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trighitplayer : MonoBehaviour {

	// causes damage on collision with player
	void Start () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("bashed with "+this.gameObject.name);
            charmove2 Char_con_scr = other.gameObject.GetComponent<charmove2>();
            if (Char_con_scr.hassheild==true)
            {
                Char_con_scr.dropsheild();
            }
            Char_con_scr.TakeRandomDamage();
            Char_con_scr.pushback(gameObject, 80);
        }
       
    }
}
