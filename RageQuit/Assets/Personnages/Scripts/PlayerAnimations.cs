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
    private bool wascrouch = true;
    private bool crouched = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Pour Grounded / AirVelocity voir PlayerMove

        //Horizontal direction :
        horiz = Input.GetAxis("Horizontal");
        anim.SetFloat("Horizontal", horiz);

        //Run/Walk
        run = Input.GetAxis("Run");
        anim.SetFloat("Run", run);

        //Vertical direction :
        vert = Input.GetAxis("Vertical");
        anim.SetFloat("Vertical", vert);

        //AnyDirectionalKey
        any = Mathf.Abs(horiz) + Mathf.Abs(vert);
        anim.SetFloat("AnyDirectionalKey", any);
        
    }
}
