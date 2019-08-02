using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// cause color change on collision with fire(only particle sys) !!!
public class burnalert : MonoBehaviour
{
    Material thismat;
    // Use this for initialization
    void Start()
    {
        Ensure(thismat = gameObject.GetComponentInChildren<Renderer>().material;)
    }

    // Update is called once per frame
    void Update()
    {


    }
    void OnParticleCollision(GameObject other)
    {
        //("burny sheild");
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
