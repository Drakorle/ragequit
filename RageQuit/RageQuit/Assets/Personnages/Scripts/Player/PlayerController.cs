using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Animator anim;
    private float forward;
    private float backward;
    private float run1;
    private float walkR;
    private float walkL;
    private float jump;
    private float Idle;



    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // jump :

        jump = Input.GetAxis("Jump");
        anim.SetFloat("jump", jump);

        //walk right :

        walkR = Input.GetAxis("Horizontal");
        if (walkR < 0)
        {
            walkR = 0;
        }
        anim.SetFloat("walkR", walkR);

        //walk left :

        walkL = Input.GetAxis("Horizontal");
        if (walkL > 0)
        {
            walkL = 0;
        }
        anim.SetFloat("walkL", walkL);

        // run :

        run1 = Input.GetAxis("Run");
        run1 *= forward;
        anim.SetFloat("run", run1);

        //forward :

        forward = Input.GetAxis("Vertical");
        if (walkL < 0 || walkR > 0)
        {
            forward = 0;
        }
        anim.SetFloat("forward", forward);

        //fin Idle :
        Idle = jump + forward + walkR - walkL + run1;
        anim.SetFloat("Idle", Idle);
    }
}
