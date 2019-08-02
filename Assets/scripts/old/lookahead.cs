using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookahead : MonoBehaviour {

    // Use this for initialization
    GameObject particleobj;
    

    void Start() {
        particleobj=GameObject.Find("Particle System");
        
    }

    // Update is called once per frame
    void Update() {
        LookAheadNow();
    }

    GameObject LookAheadNow()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 50.0f, Color.yellow);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 50.0f ))
        {
            
               

                if (hit.collider.gameObject.tag == "sheild")
            {
                
                print(" hit  " + hit.collider.gameObject.name);
               // particleobj.GetComponent<particlecon>().fireonnow();
            }
           


        }
        return null;
    }






}
