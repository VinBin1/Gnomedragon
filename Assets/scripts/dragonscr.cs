using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Dragon obj script, controls rotation, animation,  player detection and fire and attacks


public class dragonscr : MonoBehaviour {
    Quaternion dragonrot;
    Animator dragon_anocon;
    bool islooking = true; //is seeking enemys
    GameObject particleobj;
    public bool fireon = false;
    int rannum;
    bool waiting, canfire = false;
    GameObject dragonobj;
    GameObject hitfello;
    public Transform firefrompoint;
    public string state = "noenemys";//initialise state to noenemys
    public float lastdistance;
    float maxdistancetomove = 7.0f;
    public ParticleSystem thefire;
    float speed = 2.0f;
    float rotspeed = 0.1f;
    float MoveSpeed = 1.0f;
    Vector3 lasttargetpos;
    Vector3 dummynull = new Vector3(0, 0, 0);
    Quaternion leftq;
    GameObject tailcolobj;
    GameObject player1;
    LayerMask viewMask;
    ParticleSystem.EmissionModule theemission;
    GameObject currenttarget;
    String targDir;
    public Text dracatatus;
    AudioSource audioData;
    public int seekcount;
    bool noseesplayer1 = true;
    Material thismat;
    float dragonsoundmax = 30;
    float currentnoiselevel=0;
    public Transform turnpivot;
    public Text anglediag;
    public GameObject atestbone;
    int randnnum;

    void Awake()
    {
        particleobj = GameObject.Find("Particle System");
        thefire = particleobj.GetComponent<ParticleSystem>();

        if (thefire != null)
        {
            theemission = thefire.emission;
        }
    }


    // Use this for initialization
    void Start()
    {
        audioData = GetComponent<AudioSource>();
       
        dragonobj = GameObject.Find("dragon_moreanos");
        if (gameObject.name == "dragon_test")
        {
            dragonobj = gameObject;
        }
        dragon_anocon = dragonobj.GetComponent<Animator>();
        firefrompoint = GameObject.Find("castpoint").GetComponentInChildren<Transform>();
        tailcolobj = GameObject.Find("tail");
        player1 = GameObject.Find("PLAYER1");//TODO other players un hard code!!!
        thismat = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material;
    }  
	
	// Update is called once per frame
	void FixedUpdate () {
        // RotateTowardGnome(player1);
        sendangle();


        
           // domouth(atestbone);
                        
        
        
        // ienumerator switch?? TODO PROFILE
        switch (state)
        {
            case "noenemys":
                if (CanSeeaGnome())
                {
                    state = "seek";
                    print("CAN see gnome..");
                }
                else
                {
                    //pickrandom rotation and move
                    //  print("humm");
                    //print("cantttt see gnome..");
                    StartCoroutine(wait(2.0f));
                    //rotateright();

                    //move around, no visable enemy
                    rotateleft();
                    dragon_anocon.SetBool("turnleft", true);
                    LookAheadNow();
                    break;

                }
                break;


            case "seek":
                // dragon_anocon.SetBool("rightwing", false);
                dragon_anocon.SetBool("turnleft", false);
                dragon_anocon.enabled = true;
                LookAheadNow();
                dragon_anocon.SetBool("moveforward", false);

                fireoffnow();
                seekcount++;

                if(seekcount>400)
                {
                   state = "noenemys";
                  seekcount = 0;
               }
                break;


            case "sees"://dragon attacks or fires
                dragon_anocon.SetBool("dotailnow", false);
                //print(" Dragon sees Gnome");

               // rotleftright();

                //getdistance method
                lastdistance = Vector3.Distance(hitfello.transform.position, firefrompoint.transform.position);
                float anangle = getangle2d(hitfello, firefrompoint.transform.gameObject);
                if(anangle<0)
                {
                    print("on left");
                    rotateleft();
                }
                else
                {
                    print("on right");
                    rotateright();
                }

                if (lastdistance > 0)
                {

                    if (lastdistance < maxdistancetomove)
                    {
                        //move/attack??
                        movedragon();
                    }
                    else
                    {
                        //fire
                        fireonnow();
                        dragon_anocon.SetBool("moveforward", false);
                       
                    }


                }
                break;


            case "movingforward":
                  {
                    break;
                    }

            case "attack":
                {
                    changecolour();
                    StartCoroutine(waitfor(1.0f));
                      
                    state = "seek";
                    break;
                }

            case "rightwing":
                {
                    state = "rightwing";
                    dragon_anocon.SetBool("rightwing", true);
                    //print("RIGHT WING:" + Time.deltaTime);
                    StartCoroutine(wait(1.0f));
                    state = "seek";
                   // print("right wing wait:"+Time.deltaTime);
                   
                    break;
                }

            case "leftwing":
                {
                    dragon_anocon.SetBool("leftwing", true);
                    StartCoroutine(wait(1.0f));
                    
                    state = "seek";
                    break;
                }

            case "tail":
                {
                    print("tail called");
                    rotateleft();
                    StartCoroutine(waitfor(3.0f));
                
                    //state = "seek";
                    break;
                }

            default:
                print("eh?");
                break;
                     


        }

        
        dracatatus.text = ""+ state;
       
     


    }

    private static void domouth(GameObject abone)
    {
        //get angle
        if(abone.transform.rotation.x>-30)
        {
            abone.transform.Rotate(1,0, 0);
        }
        else
        {
            if((abone.transform.rotation.x <20))
            abone.transform.Rotate(1, 0, 0);
        }
    }

    private void rotleftright()
    {
        if (targDir != null)
        {
            if (targDir == "left")
            { rotateleft(); }
            else { rotateright(); }
        }
    }

    private void rotateright()
    {
        //print("rotcalled..right");
        transform.Rotate(Vector3.up * Time.deltaTime * 7.0f);
    }

    private void movedragon()
    {
        dragon_anocon.SetBool("moveforward", true);
        transform.position -= transform.right * MoveSpeed * Time.deltaTime;
    }

    GameObject LookAheadNow()
    {
        Vector3 forwarddown = new Vector3(-1, 0, 0);
        RaycastHit hit;
        Debug.DrawRay(firefrompoint.position, firefrompoint.transform.TransformDirection(Vector3.up) * 25.0f, Color.red);

        if (Physics.Raycast(firefrompoint.position, firefrompoint.transform.TransformDirection(Vector3.up), out hit, 50.0f))
        {

            float dist = Vector3.Distance(hit.transform.position, transform.position);

            if  ((hit.collider.gameObject.tag == "HeldSheild") ||( hit.collider.gameObject.tag == "Player"))
            {

                //print(" hit  " + hit.collider.gameObject.name);
                state = "sees";
                hitfello = hit.collider.gameObject;
                currenttarget = hit.collider.gameObject;
                return hitfello;
            }
            else
            {
                //print("no target");
                //state = "seek";
                if (state == "noenemys")
               { state = "noenemys"; }
                else
                { state = "seek"; }
            }


        }
        return null;
    }

    public void fireonnow()
    {
        audioData.enabled = true;
        if (!audioData.isPlaying)
        {
        audioData.Play(0);
        }
        fireon = true;
        //print("calling fire");
        dragon_anocon.enabled = false;
       
        theemission.enabled = true;
       
        StartCoroutine(wait(2.0f));
       


    }
    int  d6()
    {
        randnnum = UnityEngine.Random.Range(1, 7);
        return randnnum;
    }
    public void fireoffnow()
    {
        // var theemission2 = thefire.emission;
        theemission.enabled =  false;
        fireon = false;
    }

    IEnumerator wait(float somesec)
    {
        //print("Waiting...");
        yield return new WaitForSeconds(somesec);
        waiting = false;

        dragon_anocon.SetBool("rightwing", false);
        dragon_anocon.SetBool("leftwing", false);
        if (state == "sees")
        {
            
            state = "seek";
        }
        dragon_anocon.enabled = true;
    }

    IEnumerator waitfor(float somesec)
    {
        print("Waiting...");
        dotail();
        yield return new WaitForSeconds(somesec);
        stoptail();
        rotateleft();
        yield return new WaitForSeconds(somesec);
        //waiting = false;
        state = "seek";

    }
    public void Resetgame()
    {
        SceneManager.LoadScene(0);
    }
    public void dotail()
    {
        dragon_anocon.SetBool("dotailnow",true);

    }
    public void stoptail()
    {
        dragon_anocon.SetBool("dotailnow", false);

    }

    float getangle2d(GameObject one,GameObject two)
    {
        Vector3 direction = (one.transform.position - two.transform.position);
        Vector3 dir3d = direction;
        direction.y = 0;
        float theangle= Vector3.SignedAngle(-transform.right, direction, Vector3.up);

        return theangle;
    }

    void sendangle()
    {
        Vector3 dirToPlayer = (player1.transform.position - firefrompoint.transform.position);
        Vector3 dirToPlayer3d = dirToPlayer;
        dirToPlayer.y = 0;//2d angle only
        float angleBetweenDragonAndPlayer = Vector3.SignedAngle(-transform.right, dirToPlayer,Vector3.up);
        anglediag.text = "" + angleBetweenDragonAndPlayer;
    }
    bool CanSeeaGnome()
    {

        float viewAngle = 150.0f;
            Vector3 dirToPlayer = (player1.transform.position - firefrompoint.transform.position);
        Vector3 dirToPlayer3d = dirToPlayer;
        dirToPlayer.y = 0;//2d angle only
            float angleBetweenDragonAndPlayer = Vector3.Angle(-transform.right, dirToPlayer);
        RaycastHit hit;
        //print("check cansee a gnome angle: "+ angleBetweenDragonAndPlayer);
        Debug.DrawRay(firefrompoint.transform.position, dirToPlayer3d * 15.0f, Color.blue);
       

        if (angleBetweenDragonAndPlayer < viewAngle / 2.0f)
            {
            print("yep gnomes withinangle!");
            noseesplayer1 = false;
            if (Physics.Raycast(firefrompoint.transform.position, dirToPlayer3d,out hit))
            {
                print("Ray out "+ hit.transform.gameObject.tag);
                noseesplayer1 = true;
               if((hit.transform.gameObject.tag=="gnome")|| (hit.transform.gameObject.tag == "hand") || (hit.transform.gameObject.tag == "Player")||(hit.transform.gameObject.tag == "HeldSheild"))
                  {
                  print("player visiable");
                   return true;
                   }
                else
                {
                   
                    //print("player1 behind something");
                }
                //if(angleBetweenDragonAndPlayer < 0)
                //{
                // targDir = "right";

                // }
                // else { targDir = "left"; }
               
                
            }


            noseesplayer1 = false;


        }
        return false;
       

    }

    private void rotateleft()
    {
       // print("rotcalled..left");
        turnpivot.Rotate(Vector3.up*Time.deltaTime*-10.0f);
    }

    void RotateTowardGnome( GameObject thatgnome)
    {
        print("" + thatgnome.name);
        Vector3 offset = new Vector3(0, 0.5f, 0);
        float step =0.2f * Time.deltaTime;

        Vector3 Direction = (thatgnome.transform.position+offset)-transform.position ;
        Direction.y = 0;
        Vector3 newdir = Vector3.RotateTowards(transform.forward, Direction, step, 0.0f);

        Debug.DrawRay(transform.position, newdir, Color.green);
       //newdir = Quaternion.Euler(0, 0,90)* newdir;
        transform.localRotation= Quaternion.LookRotation(newdir);
       
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

    public void soundmade(GameObject asoundobj)
    {
        var heading = asoundobj.transform.position - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;

        //print("distance= " + distance + "Direction = " + direction);
        float thisnoise = 10000 / (distance * distance*4*3.142f);
        currentnoiselevel = currentnoiselevel + thisnoise;
        //print("thisnoise vol : " + thisnoise+" curentnoise level"+ currentnoiselevel);
        
        if (currentnoiselevel>dragonsoundmax)
        {
            print("EHHHHH!");
        }
    }
}
