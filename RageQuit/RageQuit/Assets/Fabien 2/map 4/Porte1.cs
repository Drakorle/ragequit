using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte1 : MonoBehaviour {

    public Animator Anim;
    private bool fonctionne;
    void Start()
    {
        fonctionne = false;
    }
    private void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Anim.SetBool("isbo", true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Anim.SetBool("isbo", false);
        }
    }
}
