using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation2 : MonoBehaviour {
    public float speedx = 0;
    public float speedy = 0;
    public float speedz = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(speedx, speedy, speedz); 
		
	}
}
