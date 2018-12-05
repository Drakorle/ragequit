using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

    private Animator anim;
    private float horiz;
    private float vert;
    private float run;
    private float any;
    private float crouch ;
    

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Pour Grounded / AirVelocity voir PlayerMove

        //Horizontal direction :
        PlayerMove.GetAxis(ref horiz, "RightKey", "LeftKey", 3);
        anim.SetFloat("Horizontal", horiz);

        //Run/Walk
        //run = Input.GetAxis("Run");
        PlayerMove.GetAction(ref run, "SprintKey", 1000);
        anim.SetFloat("Run", run);

        //Vertical direction :
        PlayerMove.GetAxis(ref vert, "ForwardKey", "BackKey", 3);
        anim.SetFloat("Vertical", vert);

        //AnyDirectionalKey
        any = Mathf.Abs(horiz) + Mathf.Abs(vert);
        anim.SetFloat("AnyDirectionalKey", any);
        
    }
}
