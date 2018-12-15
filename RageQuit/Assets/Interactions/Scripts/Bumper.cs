using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {

    public CharacterController CharControl;
    public Transform Player;
    public Transform Monstre;
    public float bumpForce;
    public float BurstBump;
    public static bool BumpNow = false;
    private bool FirstCall = true;
    public float BumpTimeEnd = 25f;
    private float BumpTime;
    private float bumpX;
    private float bumpY;
    private float bumpZ;


    // Use this for initialization
    void Start () {
        Player.position.Normalize();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (BumpNow)
        {
            if (FirstCall)
            {
                FirstCall = false;
                SetPointToBump();
                BumpTime = 0f;
            }
            Bump();
        }
        else
        {
            BumpTime = BumpTimeEnd;
            FirstCall = true;
        }

    }

    void Bump()
    {
        if (BumpTime < BumpTimeEnd)
        {

            Vector3 moveDirUp = new Vector3(
                 bumpX * (BurstBump / BumpTime * bumpForce),
                 bumpY * (BurstBump / BumpTime * bumpForce),
                 bumpZ * (BurstBump / BumpTime * bumpForce)
                );
            CharControl.Move(moveDirUp * Time.deltaTime);
            
        }
        BumpNow = BumpTime < BumpTimeEnd;
        BumpTime += 1;
    }

    void SetPointToBump()
    {
        
        Player.rotation  = new Quaternion(Player.rotation.x,Monstre.rotation.y,Player.rotation.z,Player.rotation.w );
        bumpX =-(Monstre.position.x - Player.position.x );
        bumpY = -(Monstre.position.y - Player.position.y );
        bumpZ = -(Monstre.position.z - Player.position.z) ;
        
    }
}
