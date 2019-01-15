using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestruction : MonoBehaviour {

    private bool Done = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !Done)
        {
            Debug.LogWarning("ça touche moi");
            Bumper.BumpNow = true;
            Bumper.PlayerStatic = other.GetComponent<Transform>();
            Bumper.CharControlStatic = other.GetComponent<CharacterController>();
            Done = true;



        }
        else if (!Done)
        {
            Debug.LogWarning("ça touche "+ other.name + ", ça détruit " + this.gameObject.name);
            //particles
            var rend = this.GetComponent<Renderer>();
            rend.enabled = false;
            var collider = this.GetComponent<Collider>();
            collider.enabled = false;
            Done = true;
        }
       
    }
}
