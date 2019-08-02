using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shildscr : MonoBehaviour {
    Material thismat;
	// Use this for initialization
	void Start () {
        thismat = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
       

    }
    void OnParticleCollision(GameObject other)
    {
        //print("burny gnome");
        changecolour();
    }
    

    void changecolour()
    {
        thismat.color = Color.red;
        StartCoroutine(colourback());
    }

    IEnumerator colourback()
    {
         yield return new WaitForSeconds(1);
        thismat.color = Color.white;
    }

}
