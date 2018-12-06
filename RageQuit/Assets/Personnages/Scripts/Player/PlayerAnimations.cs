using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAnimations : NetworkBehaviour {

    private Animator anim;
    private float horiz;
    private float vert;
    private float run;
    
    private float crouch ;
    

    

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        else {
            // Pour Grounded / AirVelocity voir PlayerMove

            
        }
        
        
    }
}
