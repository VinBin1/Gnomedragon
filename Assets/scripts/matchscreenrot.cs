using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//matches UI health bars to screen rot// exp
public class matchscreenrot : MonoBehaviour {
    Transform playertransform;

	// Use this for initialization
	void Start () {
        playertransform = transform.root;

    }
	
	// Update is called once per frame
	void Update () {
        this.transform.eulerAngles = new Vector3(-30,40, 0);
    }
}
