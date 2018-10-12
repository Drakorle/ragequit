using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portail : MonoBehaviour {
    public bool opening = false;
    public bool closing = false;
    public float speed;
    public float MaxOpenValue;
    public Transform door;
    private float CurrentValue = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (opening) OpenDoor();
        if (closing) CloseDoor();
	}
    private void OnTriggerExit (Collider obj)
    {
        if (obj.transform.name == "Player")
        {
            opening = false;
            closing = true;
        }
    }
    private void OnTriggerEnter(Collider obj)
    {
        if (obj.transform.name == "Player")
        {
            opening = true;
            closing = false;
        }
    }
    void OpenDoor()
    {
        float movement = speed * Time.deltaTime;
        CurrentValue += movement;
        if (CurrentValue <= MaxOpenValue)
        {
            door.position = new Vector3(
                door.position.x + movement,
                door.position.y,
                door.position.z
                );
        }
        else
        {
            opening = false;
        }
    }
    void CloseDoor()
    {
        float movement = speed * Time.deltaTime;
        CurrentValue -= movement;
        if (CurrentValue >= 0)
        {
            door.position = new Vector3(
                door.position.x - movement,
                door.position.y,
                door.position.z
                );
        }
        else
        {
            closing = false;
        }
    }

}
