using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemaBruitage : MonoBehaviour {

    public GameObject ship;
    private bool Done = false;
    private bool Done2 = false;
    private bool Done3 = false;
    private bool Hardcode1 = false;
    private bool Hardcode2 = false;
    private bool Hardcode3 = false;
    private bool Hardcode4 = false;
    private bool Hardcode5 = false;
    private bool Hardcode6 = false;
    private bool Hardcode7 = false;
    private bool Hardcode8 = false;
    // Update is called once per frame
    void Update () {

        if (!Done3)
        {
            FindObjectOfType<AudioManager>().Play("Cin1_elekt");
            Done3 = true;
        }
        //Hardcode

        if(ship.transform.position.x  >=- 35.55461 && !Hardcode1)
        {
            FindObjectOfType<AudioManager>().Play("Cin1_elekt");
            Hardcode1 = true;
        }
        if (ship.transform.position.x >= - 9.055937 && !Hardcode2)
        {
            FindObjectOfType<AudioManager>().Play("Cin1_elekt");
            Hardcode2 = true;
        }
        if (ship.transform.position.x >= 9.7 && !Hardcode3)
        {
            FindObjectOfType<AudioManager>().Play("Cin1_elekt");
            Hardcode3 = true;
        }
        if (ship.transform.position.x >= 10 && !Hardcode5)
        {
            FindObjectOfType<AudioManager>().Play("Cin1_elekt");
            Hardcode5 = true;
        }
        if (ship.transform.position.x >= 15 && !Hardcode6)
        {
            FindObjectOfType<AudioManager>().Play("Cin1_elekt");
            Hardcode6 = true;
        }
        if (ship.transform.position.x >= 19 && !Hardcode7)
        {
            FindObjectOfType<AudioManager>().Play("Cin1_elekt");
            Hardcode7 = true;
        }
        if (ship.transform.position.x >= 24 && !Hardcode8)
        {
            FindObjectOfType<AudioManager>().Play("Cin1_elekt");
            Hardcode8 = true;
        }


        if (ship.transform.position.x >= -30.4308 && !Done)
        {
            FindObjectOfType<AudioManager>().Play("Cin1_thrusters");
            Done = true;

        }
        else if(ship.transform.localScale.x < 27)
        {
            FindObjectOfType<AudioManager>().Stop("Cin1_thrusters");
        }
        if (ship.transform.position.x >= 9.80 && !Done2)
        {
            FindObjectOfType<AudioManager>().Play("Cin1_Warnning");
            FindObjectOfType<AudioManager>().Play("Cin1_IaWarnning");
            Done2 = true;

        }
        else if (ship.transform.localScale.x < 27)
        {
            FindObjectOfType<AudioManager>().Stop("Cin1_Warnning");
            FindObjectOfType<AudioManager>().Stop("Cin1_IaWarnning");
            FindObjectOfType<AudioManager>().Stop("Cin1_elekt");
        }
    }
}
