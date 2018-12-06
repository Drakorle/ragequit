using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    //Appel aux Objets du jeu
    public GameObject Player;
    public CharacterController CharControl;
    public Transform Body;
    public Animator anim;
    public GameObject SuperTrail;
    public Transform DJumpTrail;
    private GameObject Trail;
    public GameObject SpawnPoint;
    public int DeathZonePos;

    //Basics
    public float WalkSpeed = 4 ;
    public float RunSpeed = 6;
    
    //Crouch
    private float Slide = 0;
    public float SlideTime = 15;
    bool crouched = false;
    private float WaitingTime = 10;
    public float CrouchSpeed = 2;

    //Jump
    private bool TrailExist = false;
    private bool DJumpDone = false;
    public float jumpforce = 10f;
    private float jumpVelocity;
    public float gravity = 30f;

    //move axis
    private float vert = 0;
    private float horiz = 0;
    private float run = 0;

    // Use this for initialization
    void Awake()
    {
        CharControl = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Crouch();
        MovePlayer();
        Jump();
        DeathZone();
        
	}
    void MovePlayer()
    {
        //Recupération des touches 
        //float run = Input.GetAxis("Run");

        GetAction(ref run, "SprintKey", 1000);

        GetAxis(ref horiz, "RightKey", "LeftKey", 3);
        GetAxis(ref vert, "ForwardKey", "BackKey", 3);

        //Déclaration des Vectors3
        Vector3 moveDirSide, moveDireForward;
        
        //Switch entre crouch et normal + décalage du Slide
        if (CharControl.isGrounded && crouched && (Slide == SlideTime || run <= 0))
        {
            moveDirSide = transform.right * horiz * CrouchSpeed;
            moveDireForward = transform.forward * vert * CrouchSpeed;
        }
        else
        {
            moveDirSide = transform.right * horiz * (WalkSpeed + run * RunSpeed);
            moveDireForward = transform.forward * vert * (WalkSpeed + run * RunSpeed);
        }
        //Conditions de Jump et création des forces
        // Au sol
        if (CharControl.isGrounded)
        {
            if (TrailExist)
            {
                Destroy(Trail);
                TrailExist = false;
            }
            jumpVelocity = -gravity * Time.deltaTime;
            //si appuyer sur space
            if (Input.GetKeyDown(MainMenu.GetKeyCode("JumpKey")))
            {
                jumpVelocity = jumpforce;
                DJumpDone = false;

            }
        }
        else
        {
            if (Input.GetKeyDown(MainMenu.GetKeyCode("JumpKey")) && DJumpDone == false && PickMoney.Hydro > 0)
            {
                // Création des particules du double saut
                Trail = (GameObject)Instantiate(
                    SuperTrail,
                    Vector3.zero,
                    Quaternion.identity);
                Trail.transform.SetParent(GameObject.FindGameObjectWithTag("Trail").transform, false);

                TrailExist = true;

                jumpVelocity = jumpforce;
                DJumpDone = true;
                PickMoney.Hydro -= 1;

            }
            jumpVelocity -= gravity * Time.deltaTime;
        }

        // Slide time
        if (Slide < SlideTime && crouched)
        {
            Slide += 0.5f;
            
        }
        else if (Slide == SlideTime && !crouched)
        {
            Slide = 0;
        }

        Vector3 moveDirUp = Vector3.zero ;

        //Affect les forces au joueurs
        moveDirUp.x = moveDirSide.x + moveDireForward.x;
        moveDirUp.y = jumpVelocity + moveDirSide.y + moveDireForward.y;
        moveDirUp.z = moveDireForward.z + moveDirSide.z;
        CharControl.Move(moveDirUp *Time.deltaTime);

    }
    //Paramètres Animation Jump
    void Jump()
    {
        anim.SetBool("Grounded", CharControl.isGrounded);
        anim.SetFloat("AirVelocity", jumpVelocity);
    }


    //Move Crouch
    void Crouch()
    {
        //Modification de la taille du personnage et reposition du corp graphic

        if (Input.GetKeyDown(MainMenu.GetKeyCode("CrouchKey")) && WaitingTime >= SlideTime)
        {
            if (!crouched)
            {
                CharControl.height = 1;
                crouched = true;
                WaitingTime = 0;
                Body.transform.position += new Vector3(0, 0.55f, 0);
            }
            else
            {
                crouched = false;
                WaitingTime = 0;
                Body.transform.position += new Vector3(0, -0.55f, 0);
            }
        }

        // Temps d'attente entre deux crouch
        // Slide
        if (WaitingTime < SlideTime )
        {
            WaitingTime += 0.5f;

                
        }
        //Non Slide
        PlayerMove.GetAction(ref run, "SprintKey", 1000);
        PlayerMove.GetAxis(ref vert, "ForwardKey", "BackKey", 3);
        if (run <= 0 || vert == 0)
        {
            WaitingTime += 0.5f;
        }
        //Transition de Crouch vers Normal
        if (!crouched && CharControl.height < 2f)
        {
            CharControl.height += 0.1f;
        }
        if(CharControl.height == 2)
        {
            crouched = false;
        }
        
        
        //Paramètre d'animation Crouch

        crouched = CharControl.height == 1 ;
        anim.SetBool("Crouched", crouched && CharControl.isGrounded);
    }
    void DeathZone()
    {
        if (Player.transform.position.y < DeathZonePos)
        {
            Player.transform.position = SpawnPoint.transform.position;
            PickMoney.Hydro = 15;
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("PickUpHydro");

            foreach (GameObject go in gameObjectArray)
            {
                
                Debug.LogWarning(go.name + " Activé !");
                go.SetActive(true);
                if (!go.transform.GetChild(0).gameObject.active)
                    go.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

    }

    public static void GetAxis(ref float axis, string prefIncrease, string prefDecrease, int sensitivity)
    {
        KeyCode increase = MainMenu.GetKeyCode(prefIncrease);
        KeyCode decrease = MainMenu.GetKeyCode(prefDecrease);

        bool moving = false;

        if (Input.GetKey(increase))
        {
            axis = Mathf.Clamp(axis + sensitivity * Time.deltaTime, -1f, 1f);
            moving = true;
        }
        if (Input.GetKey(decrease))
        {
            axis = Mathf.Clamp(axis - sensitivity * Time.deltaTime, -1f, 1f);
            moving = true;
        }

        if (!moving)
        {
            if (axis > 0.05)
            {
                axis -= sensitivity * 2 * Time.deltaTime;
            }
            else if (axis < -0.05)
            {
                axis += sensitivity * 2 * Time.deltaTime;
            }
            else
            {
                axis = 0;
            }
        }
    }

    public static void GetAction(ref float axis, string prefIncrease, int sensitivity)
    {
        KeyCode increase = MainMenu.GetKeyCode(prefIncrease);

        bool moving = false;

        if (Input.GetKey(increase))
        {
            axis = Mathf.Clamp(axis + sensitivity * Time.deltaTime, -1f, 1f);
            moving = true;
        }

        if (!moving)
        {
            if (axis > 0.05)
            {
                axis -= sensitivity * 2 * Time.deltaTime;
            }
            else
            {
                axis = 0;
            }
        }
    }

}
