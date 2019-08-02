using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tailtrig : MonoBehaviour {

    GameObject dragonscrobj;
    //dragonscr dragonscriptref;
	// Use this for initialization
	void Start () {
        dragonscrobj = GameObject.FindGameObjectWithTag("Dragon");
       // dragonscriptref = dragonscrobj.GetComponent<dragonscr>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        //tell script

        if (other.gameObject.tag == "Player")
        {
            //dragonscrobj.GetComponent<dragonscr>().dotail();
            string thisobj = this.gameObject.name;

            switch (thisobj)
            {
                case "tailcol":


                    dragonscrobj.GetComponent<dragonscr>().state = "tail";
                    print(" trigger taildetect with " + this.gameObject.name);
                    break;

                case "rightwing":

                    dragonscrobj.GetComponent<dragonscr>().state = "rightwing";
                    print(" trigger rightwing with " + this.gameObject.name);

                    break;

                case "leftwing":

                    dragonscrobj.GetComponent<dragonscr>().state = "leftwing";
                    print(" trigger leftwing with " + this.gameObject.name);
                    break;

                case "bite_trig":

                    //call bite state TODO
                    //dragonscrobj.GetComponent<dragonscr>().state = "leftwing";
                    print(" triggered BITE!! with " + this.gameObject.name);
                    break;

                //dragonscrobj.GetComponent<dragonscr>().state = "attack";
                //TODO change for all attacks
                default:
                    print("Trigger error");
                    break;
            }
        }   
    }

    void OnTriggerExit(Collider other)
    {
        //tell script
        if (other.gameObject.tag == "Player")
        {
        print("tail off");
        dragonscrobj.GetComponent<dragonscr>().stoptail();
        }
    }



}
