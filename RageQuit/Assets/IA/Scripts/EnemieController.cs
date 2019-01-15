using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemieController : MonoBehaviour {

    public float lookRadius = 10f;
    public Animator Zoid;
    public Transform ZoidGraphics;
    Transform target;
    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        ZoidGraphics.position = transform.position;
        ZoidGraphics.rotation = transform.rotation;
        float distance = Vector3.Distance(target.position, transform.position); // def la distance joueur iA
        if(distance <= lookRadius)
        {
            Zoid.SetBool("ZoidAttack", true);
            agent.SetDestination( target.position);
            if(distance <= agent.stoppingDistance)
            {
                Debug.LogWarning("ici");
                Bumper.BumpNow = true;
                Facetarget();
            }
        }
        else
        {
            Zoid.SetBool("ZoidAttack", false);
        }

	}
    // quaternion pour la rotation 
    // cette fonction sert a orienté l'enemie vers le joueur
    void Facetarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void OnDrawGizmosSelected() // affiche la zone de détection dans la scène mais pas dans la partie 
    {
        ;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        
    }
}
