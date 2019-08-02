using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//updates score UI
public class score : MonoBehaviour {
	public int player1scr,player2scr; 
	Text p1text,p2text;
    Image thegoldbar;

	// Use this for initialization
	void Start () {
		p1text = GameObject.Find ("p1score").GetComponentInChildren<Text>();
		p2text = GameObject.Find ("p2score").GetComponentInChildren<Text>();
        thegoldbar = GameObject.Find("goldbar").GetComponentInChildren<Image>();
        updategoldbar();
    }
    //single scores??
	public GameObject updatescore(GameObject playername)
	{
		//print ("update score "+playername.name);
		if (playername.name == "PLAYER1") {
			player1scr++;
           

            p1text.text = "" + player1scr;
		}
		if (playername.name == "PLAYER2") {
			player2scr++;

			p2text.text = "" + player2scr;
		}
        updategoldbar();//eitherplayer update call
        return playername;
	}
    //singleplayer score!!!
    void updategoldbar()
    {
        //single player update
        thegoldbar.fillAmount = (player1scr+player2scr) / 150.0f;//fill from both sides TODO
        //print(" " + player1scr / 150.0f);
    }

}
