using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    private Animator Anim;

	// Use this for initialization
	void Start () {
        Anim = GameObject.Find("Porte").GetComponent<Animator>();
	}
	void OnTriggerEnter()
    {
        Anim.SetBool("Open", true);
    }
    // Update is called once per frame
    void OnTriggerExit()
    {
        Anim.SetBool("Open", false);
    }
}
