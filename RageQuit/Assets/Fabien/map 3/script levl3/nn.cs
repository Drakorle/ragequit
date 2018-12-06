using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nn : MonoBehaviour {
    private Animator Anim;
	// Use this for initialization
	void Start () {
        Anim = GameObject.Find("p").GetComponent<Animator>();
		
	}
    void OnTriggerEnter()
    {
        Anim.SetBool("isbool", true);
    }
    void OnTriggerExit()
    {
        Anim.SetBool("isbool", false);
    }

}
