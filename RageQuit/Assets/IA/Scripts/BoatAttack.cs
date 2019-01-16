using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatAttack : MonoBehaviour
{

    public Transform player;
    public Transform boat;
    public GameObject ballPrefab;
    public Transform ballSpawn;

    public float time = 2.0f;
    public float timeBeforeFire = 2.0f;
    private float currentTime = 0f;
    private bool fired = false;

    void Update()
    {
        float l = player.position.x - boat.position.x;
        float L = player.position.z - boat.position.z;
        float a = Mathf.Atan(l / L) / 2;

        boat.rotation = new Quaternion(boat.rotation.x, a, boat.rotation.z, boat.rotation.w);

        if(currentTime >= timeBeforeFire)
        {
            Fire();
            currentTime = 0.0f;
        }
        else
        {
            currentTime += 0.005f;
        }

    }

    void Fire()
    {
        FindObjectOfType<AudioManager>().Play("CannonFire");

        var ball = (GameObject)Instantiate(
            ballPrefab,
            ballSpawn.position,
            ballSpawn.rotation
            );

        float x = (player.position.x - ball.transform.position.x);
        float y = (player.position.y - ball.transform.position.y);
        float z = (player.position.z - ball.transform.position.z);

        Vector3 mvmts = new Vector3(x, y, z);
        ball.GetComponent<Rigidbody>().velocity = mvmts;

        Destroy(ball, time);
    }
}
        
