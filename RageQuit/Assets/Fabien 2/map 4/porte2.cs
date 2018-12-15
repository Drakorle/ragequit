using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class porte2 : MonoBehaviour {

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
            Anim.SetBool("bo", true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Anim.SetBool("bo", false);
        }
    }
}

