using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove_mp : NetworkBehaviour
{

    //Appel aux Objets du jeu
    public GameObject PlayerMP;
    public CharacterController CharControlMP;
    public Transform BodyMP;
    public Animator animMP;
    public GameObject SuperTrailMP;
    public Transform DJumpTrailMP;
    private GameObject TrailMP;
    public int DeathZonePosMP;
    public Camera camMP;
    public NetworkStartPosition[] spawnPointsMP = new NetworkStartPosition[2];

    //Basics
    public float WalkSpeedMP = 4;
    public float RunSpeedMP = 6;
    public float SpawnXMP;
    public float SpawnYMP;
    public float SpawnZMP;

    //Crouch
    private float SlideMP = 0;
    public float SlideTimeMP = 15;
    bool crouchedMP = false;
    private float WaitingTimeMP = 10;
    public float CrouchSpeedMP = 2;
    private float anyMP;

    //Jump
    private bool TrailExistMP = false;
    private bool DJumpDoneMP = false;
    public float jumpforceMP = 10f;
    private float jumpVelocityMP;
    public float gravityMP = 30f;

    //move axis
    private float vertMP = 0;
    private float horizMP = 0;
    private float runMP = 0;

    // Use this for initialization
    void Awake()
    {
        CharControlMP = GetComponent<CharacterController>();
        spawnPointsMP = FindObjectsOfType<NetworkStartPosition>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            camMP.enabled = false;
            return;
            
        }

        
        CrouchMP();
        MovePlayerMP();
        JumpMP();
        AnimationMP();
        DeathZoneMP();


    }
    void MovePlayerMP()
    {
        //Recupération des touches 
        //float run = Input.GetAxis("Run");

        GetActionMP(ref runMP, "SprintKey", 1000);

        GetAxisMP(ref horizMP, "RightKey", "LeftKey", 3);
        GetAxisMP(ref vertMP, "ForwardKey", "BackKey", 3);

        //Déclaration des Vectors3
        Vector3 moveDirSideMP, moveDireForwardMP;

        //Switch entre crouch et normal + décalage du Slide
        if (CanJump.Jumpable && crouchedMP && (SlideMP == SlideTimeMP || runMP <= 0))
        {
            moveDirSideMP = transform.right * horizMP * CrouchSpeedMP;
            moveDireForwardMP = transform.forward * vertMP * CrouchSpeedMP;
        }
        else
        {
            moveDirSideMP = transform.right * horizMP * (WalkSpeedMP + runMP * RunSpeedMP);
            moveDireForwardMP = transform.forward * vertMP * (WalkSpeedMP + runMP * RunSpeedMP);
        }
        //Conditions de Jump et création des forces
        // Au sol
        if (CharControlMP.isGrounded)
        {
            if (TrailExistMP)
            {
                Destroy(TrailMP);
                TrailExistMP = false;
            }

            jumpVelocityMP = -gravityMP * Time.deltaTime;
            //si appuyer sur space
            if (Input.GetKeyDown(MainMenu.GetKeyCode("JumpKey")))
            {
                jumpVelocityMP = jumpforceMP;
                DJumpDoneMP = false;

            }
        }
        else
        {
            //if (Input.GetKeyDown(MainMenu.GetKeyCode("JumpKey")) && DJumpDoneMP == false && PickMoney.Hydro > 0)
            if (Input.GetKeyDown(MainMenu.GetKeyCode("JumpKey")) && DJumpDoneMP == false && PickMoney.Hydro > 0)
            {
                // Création des particules du double saut
                TrailMP = (GameObject)Instantiate(
                    SuperTrailMP,
                    Vector3.zero,
                    Quaternion.identity);
                TrailMP.transform.SetParent(GameObject.FindGameObjectWithTag("Trail").transform, false);

                TrailExistMP = true;

                jumpVelocityMP = jumpforceMP;
                DJumpDoneMP = true;
                PickMoney.Hydro -= 1;

                

            }
            jumpVelocityMP -= gravityMP * Time.deltaTime;
        }

        // Slide time
        if (SlideMP < SlideTimeMP && crouchedMP)
        {
            SlideMP += 0.5f;

        }
        else if (SlideMP == SlideTimeMP && !crouchedMP)
        {
            SlideMP = 0;
        }

        Vector3 moveDirUp = Vector3.zero;

        //Affect les forces au joueurs
        moveDirUp.x = moveDirSideMP.x + moveDireForwardMP.x;
        moveDirUp.y = jumpVelocityMP + moveDirSideMP.y + moveDireForwardMP.y;
        moveDirUp.z = moveDireForwardMP.z + moveDirSideMP.z;
        CharControlMP.Move(moveDirUp * Time.deltaTime);

    }
    //Paramètres Animation Jump
    void JumpMP()
    {
        animMP.SetBool("Grounded", CharControlMP.isGrounded);
        animMP.SetFloat("AirVelocity", jumpVelocityMP);
    }


    //Move Crouch
    void CrouchMP()
    {
        //Modification de la taille du personnage et reposition du corp graphic

        if (Input.GetKeyDown(MainMenu.GetKeyCode("CrouchKey")) && WaitingTimeMP >= SlideTimeMP)
        {
            if (!crouchedMP)
            {
                CharControlMP.height = 1;
                crouchedMP = true;
                WaitingTimeMP = 0;
                BodyMP.transform.position += new Vector3(0, 0.55f, 0);
            }
            else
            {
                crouchedMP = false;
                WaitingTimeMP = 0;
                BodyMP.transform.position += new Vector3(0, -0.55f, 0);
            }
        }

        // Temps d'attente entre deux crouch
        // Slide
        if (WaitingTimeMP < SlideTimeMP)
        {
            WaitingTimeMP += 0.5f;


        }
        //Non Slide
        PlayerMove.GetAction(ref runMP, "SprintKey", 1000);
        PlayerMove.GetAxis(ref vertMP, "ForwardKey", "BackKey", 3);
        if (runMP <= 0 || vertMP == 0)
        {
            WaitingTimeMP += 0.5f;
        }
        //Transition de Crouch vers Normal
        if (!crouchedMP && CharControlMP.height < 2f)
        {
            CharControlMP.height += 0.1f;
        }
        if (CharControlMP.height == 2)
        {
            crouchedMP = false;
        }


        //Paramètre d'animation Crouch

        crouchedMP = CharControlMP.height == 1;
        animMP.SetBool("Crouched", crouchedMP && CharControlMP.isGrounded);
    }
    void DeathZoneMP()
    {
        if (PlayerMP.transform.position.y < DeathZonePosMP)
        {
            PlayerMP.transform.position = new Vector3(SpawnXMP, SpawnYMP, SpawnZMP);
            PickMoney.Hydro = 15;
            GameObject[] gameObjectArrayMP = GameObject.FindGameObjectsWithTag("PickUpHydro");

            foreach (GameObject go in gameObjectArrayMP)
            {

                Debug.LogWarning(go.name + " Activé !");
                go.SetActive(true);
                if (!go.transform.GetChild(0).gameObject.active)
                    go.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

    }

    public static void GetAxisMP(ref float axis, string prefIncrease, string prefDecrease, int sensitivity)
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

    public static void GetActionMP(ref float axis, string prefIncrease, int sensitivity)
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
    void AnimationMP()
    {
        //Horizontal direction :
        PlayerMove_mp.GetAxisMP(ref horizMP, "RightKey", "LeftKey", 3);
        animMP.SetFloat("Horizontal", horizMP);

        //Run/Walk
        //run = Input.GetAxis("Run");
        PlayerMove_mp.GetActionMP(ref runMP, "SprintKey", 1000);
        animMP.SetFloat("Run", runMP);

        //Vertical direction :
        PlayerMove_mp.GetAxisMP(ref vertMP, "ForwardKey", "BackKey", 3);
        animMP.SetFloat("Vertical", vertMP);

        //AnyDirectionalKey
        anyMP = Mathf.Abs(horizMP) + Mathf.Abs(vertMP);
        animMP.SetFloat("AnyDirectionalKey", anyMP);
    }
}
