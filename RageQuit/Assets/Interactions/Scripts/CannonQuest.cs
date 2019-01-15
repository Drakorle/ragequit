using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonQuest : MonoBehaviour {

    public bool Done = false;
    public GameObject ballPrefab;
    public Transform ballSpawn;
    public Transform fin;
    public float time = 20.0f;
    public float current_time = 0.0f;
    public int step;
    public bool fired = false;
    public GameObject island;
    public GameObject islandDamaged;
    public GameObject islandRealyDamaged;

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !Done)
        {
            
           
            var ball = (GameObject)Instantiate(
            ballPrefab,
            ballSpawn.position,
            ballSpawn.rotation
            );

            float x = (fin.position.x - ball.transform.position.x);
            float y = (fin.position.y - ball.transform.position.y);
            float z = (fin.position.z - ball.transform.position.z);

            Vector3 mvmts = new Vector3(x, y, z);
            ball.GetComponent<Rigidbody>().velocity = mvmts;

            Destroy(ball, time);

            var collider = this.GetComponent<Collider>();
            collider.enabled = false;
            IslandDestroy.fired = true;
            IslandDestroy.time = time;
            Done = true;
            



        }
        

    }

}
