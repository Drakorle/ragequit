using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanJump : MonoBehaviour {

    public static bool Jumpable;
    public Rigidbody Player;
    private bool Appel1 = true;
    private bool Appel2 = false;
    private float float1;
    private float float2;
    private bool bool1;
    private bool bool2;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.LogWarning(Player.velocity.y);
        Debug.LogWarning(Jumpable);
        Jumpable = Player.velocity.y <= 0.001 && Player.velocity.y >= -0.001;
		
	}
    
}
