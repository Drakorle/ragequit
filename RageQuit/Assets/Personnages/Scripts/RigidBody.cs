using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBody : MonoBehaviour {

    public Transform Body;
    public Transform Player;

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
        Body.position = Player.position;
        Body.rotation = new Quaternion(0, 0, 0, 0);
	}
}
