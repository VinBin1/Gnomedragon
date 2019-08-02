using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charmove2 : MonoBehaviour {

	[SerializeField]
	float movespeed=15.0f;

	[SerializeField]
	float health=100;

	Image playerhealthbar;
    
	Text playerhealthtxt;
	Animator thisanocon;
	string horzstring="Horizontal";
	string vertstring="Vertical";
	string leftstring="pickupleft";
    string xbuttonstring = "JoystickButton1";
    string b_buttonstring = "JoystickButton2";
    string l1_buttonstring = "JoystickButton5";
    string r1_buttonstring = "JoystickButton4";
    public bool hassheild = true;
    public GameObject sheildhand;
    public bool inrange = false;
    GameObject target, thearmature, gnome;
    Rigidbody[] ragdollrb;
    bool gnomeisalive=true;
    public GameObject UIgameoverobj;
    public Rigidbody GnomeRb;
    public GameObject thedragobj;

    public float z;
	// Use this for initialization
	void Start () {
        thearmature = GameObject.Find("Armature");

        //ragdoll set
        ragdollrb = GetComponentsInChildren<Rigidbody>();
        gnome = GameObject.Find("gnomeano4");
        for (int i = 0; i < ragdollrb.Length; i++)
        {
            ragdollrb[i].isKinematic = true;//rigidbody is not kinamatic

        }


        if (gameObject.name == "PLAYER1")
		{
            //print("player 1 controls set");
            playerhealthtxt = GameObject.Find ("p1health").GetComponentInChildren<Text> ();
			playerhealthbar = GameObject.Find ("healthbarimg").GetComponentInChildren<Image> ();
           
        } else {
            //print("player 2 controls set");
            playerhealthtxt = GameObject.Find ("p2health").GetComponentInChildren<Text> ();
			playerhealthbar = GameObject.Find ("p2healthbarimg").GetComponentInChildren<Image> ();
			vertstring="Vertical2";
			horzstring="Horizontal2";
             xbuttonstring = "p2JoystickButton1";
             b_buttonstring = "p2JoystickButton2";
             l1_buttonstring = "p2JoystickButton5";
             r1_buttonstring = "p2JoystickButton4";

        }
        thisanocon = GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

      

        if (gnomeisalive == true)
            { move(); }

        //sheild controls

        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            if (hassheild == true)
            {
                print("DROP sheild");
                dropsheild();

            }
            else {

                if (hassheild == false)
                {
                    searchsheild();
                    if (inrange == true)
                    {
                        
                        if (target != null)
                        {
                            addsheild(target);
                            hassheild = true;
                        }
                    }
                }
            }
            
        }


    }
		void move()
		{
            var x = Input.GetAxis(horzstring) * Time.deltaTime * movespeed*20.0f; ;
            z = Input.GetAxis(vertstring) * Time.deltaTime * movespeed;
            //* Time.deltaTime * 3.0f;
			var lshift=  Time.deltaTime* 1.0f;
            // print("zed is" + z);

            //x and b aniamion controls

             if (Input.GetButton(xbuttonstring))
            {
            thisanocon.SetTrigger("right");
            //thisanocon.SetBool("walking", false);
            
            }

            if (Input.GetButton(b_buttonstring))
            {
            thisanocon.SetTrigger("left");
           // thisanocon.SetBool("walking", false);
            
            }


        //bumpers straft controls
            if (Input.GetButton(r1_buttonstring) ||Input.GetButton(l1_buttonstring))
			{

			if(Input.GetButton(l1_buttonstring))
				{
					transform.Translate (lshift/ 1.5f, 0, 0);
										thisanocon.SetBool ("strafleft", true);
					
				}

			if(Input.GetButton(r1_buttonstring))
					{
						transform.Translate (-lshift/1.5f, 0, 0);
										thisanocon.SetBool ("strafleft", true);

					}
				}
				else {
									transform.Rotate(0, x, 0);
                        thisanocon.SetBool("strafleft", false);
        }






			transform.Translate(0, 0, z * Time.deltaTime * movespeed);

		if (z > 0.1f)
            {
			//thisanocon.SetBool ("left", false);
			thisanocon.SetBool ("walking", true);
			
			} else 
		        {
			        if (z < -0.1f)
			        {
			        	thisanocon.SetBool ("walkback", true);
                z = z / 2;
               
				
			        }
                    
                }
        if ((z >= -0.1f) &&( z <= 0.1f))
        {
            thisanocon.SetBool("walking", false);
            thisanocon.SetBool("walkback", false);
        }

        //animate roation
        if (x > 0.01 || x < -0.01)
        {
            thisanocon.SetBool("walking", true);//to roate animation????
        }
    }

	void OnParticleCollision(GameObject other)
	{
		//print("burny player"); TODO remove
		takefiredamage();
	}

	void takefiredamage()
		{
		health--;
		playerhealthbar.fillAmount = (health / 100);
		playerhealthtxt.text = "Health: " + health;
        print("dameage player");
        CheckPlayerAlive();
        pushback(thedragobj,1);
    }

   public void TakeRandomDamage()
    {
        health = health - Random.Range(5, 20);
        playerhealthbar.fillAmount = (health / 100);
        playerhealthtxt.text = "Health: " + health;
        print("random damage player");
        CheckPlayerAlive();
    }

    void CheckPlayerAlive()
    {
        if (gnomeisalive == true)
        {
            if (health < 1)
            {
                playerdeath();
            }
        }
    }
    void playerdeath()
    {
        gnomeisalive = false;

        //screen message
        UIgameoverobj.SetActive(true);
        //disable healthbar TODO

        //diable animator
        thisanocon.enabled = false;

        //roate90 to face up/down
        dropsheild();

        //TODO trigger death animation, start reincarnation routine TODO

        transform.Rotate(0, 0, -90);
        //disable control is alive

    }
    public void dropsheild()
    {
        movespeed = 15f;
        if(hassheild)
        {
            sheildhand.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
            sheildhand.transform.GetChild(0).tag = "sheild";
            sheildhand.transform.GetChild(0).parent = null;
            hassheild = false;
            inrange = false;
        }
        
        

    }
    void addsheild(GameObject obj )
    {
        movespeed = 10;
        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.transform.SetParent(sheildhand.transform);
        obj.transform.localPosition = new Vector3(0, 0, 0);
        obj.transform.localRotation = new Quaternion();
        obj.tag = "HeldSheild";
        
        return;
    }

    void searchsheild()
    {
        RaycastHit hit;
        Vector3 forwarddown = new Vector3(0, -0.35f,0.0f);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward+ forwarddown) * 10.0f, Color.yellow);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward + forwarddown), out hit, 50.0f))
        {

            //print("hit " + hit.collider.gameObject.name);

            if (hit.collider.gameObject.tag == "sheild")
            {

               // print(" Gnome sees sheild" + hit.collider.gameObject.name);
                if(hit.distance<1.5f)
                {
                   inrange = true;
                    //print("sheild within range");
                    target = hit.collider.gameObject;//target this sheild

                   
                   
                  
                }
                else { inrange = false; target = null; }
            }
            else { inrange = false; target = null; }


        }
       
    }
    void undoragdoll()
    {
      
        for (int i = 0; i < ragdollrb.Length; i++)
        {
            ragdollrb[i].isKinematic = true;//rigidbody is not kinamatic

        }
        this.transform.localRotation = Quaternion.Euler(0, 259.0f, 0);
        gnome.transform.position = new Vector3(0.16f, 0.7f, 0.12f);
        thearmature.transform.position = new Vector3(0, -1.0f, 0);
        thearmature.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        thisanocon.enabled = true;
    }

    public void pushback(GameObject pusher,float strenght)
    {
        //print("pushback called"+ pusher.name);
        Vector3 backdir = ( transform.position- pusher.transform.position );
        backdir = Vector3.Normalize(backdir);
        backdir.y = 0;
       
        Debug.DrawRay(transform.position, backdir * 15.0f, Color.black);
      
        transform.Translate(backdir * Time.deltaTime*strenght, Space.World);
    }

    void doragdoll()
    {
    
        //disable animator TODO
     
        // make bones unkinematic
        for (int i = 0; i < ragdollrb.Length; i++)
        {
            ragdollrb[i].isKinematic = false;//rigidbody is not kinamatic
            
        }
        thisanocon.enabled = false;


        //wait for effect.

    }

    private void OnTriggerEnter(Collider trigerer)
    {
        if (trigerer.gameObject.name == "Dragon")
        {
           // doragdoll();
           // StartCoroutine(waitsec(0.7f));
        }
       
        
    }

    IEnumerator waitsec(float secs)
    { 

        yield return new WaitForSeconds(secs);
        undoragdoll();


    }
}
