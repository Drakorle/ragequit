using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAttack : MonoBehaviour {

    public Animator BirdAnim;
    public Transform bird;
    public Transform IniBird;
    public Transform PlayerPos;
    public Transform Nid;
    public float speed = 1f;
    public float maxTime = 25f;
    public bool Attacking = false;
    private bool AttackEnd = true;
    private bool Reset = true;
    private float x;
    private float y;
    private float z;
    private float EndX;
    private float EndY;
    private float EndZ;
    private float time = 0f;

    // Use this for initialization
    void Start () {

       

    }
	
	// Update is called once per frame
	void Update () {
        if (Reset)
        {
            Reset = false;
            bird.position = IniBird.position;
            bird.rotation = IniBird.rotation;
            Debug.LogWarning("Reseted");
        }
        if (Attacking)
        {
            Attack(time);
            time += 0.1f;
        }
        else
        {
            time = 0f;
            if (!AttackEnd)
            {
                AttackEnding();
            }
        }

	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && AttackEnd)
        {
            BirdAnim.SetBool("Attacking", true);
            Attacking = true;
        }
    }

    void Attack(float time)
    {
        
        x = (PlayerPos.position.x - bird.position.x)/75 ;
        y = (PlayerPos.position.y - bird.position.y)/75 ;
        z = (PlayerPos.position.z - bird.position.z)/75 ;
        if ((Mathf.Abs(x) * 75f <= 1f && Mathf.Abs(y) * 75f <= 1f && Mathf.Abs(z) * 75f <= 1f))
        {
            Attacking = false;
            AttackEnd = false;
            SetEndMove();
            Bumper.BumpNow = true;
            Debug.LogWarning("ça touche");
        }
        else if (time > maxTime)
        {
            Attacking = false;
            AttackEnd = false;
            SetEndMove();
            Debug.LogWarning("A cause du temps");
        }
        else
        {

            float movementX = x * speed * Time.deltaTime;
            float movementY = y * speed * Time.deltaTime;
            float movementZ = z * speed * Time.deltaTime;

            bird.position = new Vector3(
                bird.position.x + movementX,
                bird.position.y + movementY,
                bird.position.z + movementZ);

            Vector3 direction = (PlayerPos.position - bird.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            bird.rotation = Quaternion.Slerp(bird.rotation, lookRotation, Time.deltaTime * 5f);

        }

        
    }

    void AttackEnding()
    {

        if (AttackEnd || (Mathf.Abs(bird.position.x - Nid.position.x) * 75f <= 10f && Mathf.Abs(bird.position.y - Nid.position.y) * 75f <= 10f && Mathf.Abs(bird.position.z - Nid.position.z) * 75f <= 10f))
        {
            BirdAnim.SetBool("Attacking", false);
            Attacking = false;
            AttackEnd = true;
            Reset = true;
           
        }
        else
        {

            float movementX = EndX * speed * Time.deltaTime;
            float movementY = EndY * speed * Time.deltaTime;
            float movementZ = EndZ * speed * Time.deltaTime;

            bird.position = new Vector3(
                bird.position.x + movementX,
                bird.position.y + movementY,
                bird.position.z + movementZ);

            Vector3 direction = (Nid.position - bird.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            bird.rotation = Quaternion.Slerp(bird.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void SetEndMove()
    {

        EndX = (Nid.position.x - bird.position.x) / 150;
        EndY = (Nid.position.y - bird.position.y) / 150;
        EndZ = (Nid.position.z - bird.position.z) / 150;
    }

        
}
