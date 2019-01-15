using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {

    public CharacterController CharControl;
    public static CharacterController CharControlStatic;
    public Transform Player;
    public static Transform PlayerStatic;
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
    void Start()
    {
        Player.position.Normalize();

    }

    // Update is called once per frame
    void Update()
    {
        if (BumpNow)
        {
            if (FirstCall)
            {
                if (CharControlStatic != null)
                {
                    CharControl = CharControlStatic;
                    Player = PlayerStatic;
                }
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
            Debug.LogWarning((BurstBump / BumpTime * bumpForce));

        }
        BumpNow = BumpTime < BumpTimeEnd;
        BumpTime += 1;
    }

    void SetPointToBump()
    {

        bumpX = (2 * Player.position.x - Monstre.position.x) / BumpTimeEnd;
        bumpY = (2 * Player.position.y - Monstre.position.y) / BumpTimeEnd;
        bumpZ = (2 * Player.position.z - Monstre.position.z) / BumpTimeEnd;

    }
}