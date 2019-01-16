using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickMoney : MonoBehaviour {

    public Text ScoreText;
    private int Score;
    public int StartHydro;
    public static int Hydro;

	// Use this for initialization
	void Start ()
    {
        Score = 0;
        Hydro = StartHydro;
        SetScore();
    }
	
	// Update is called once per frame
	void Update () {

        SetScore();
        if(Hydro > 15)
        {
            Hydro = 15;
        }
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            Score += 1;
            

        }

        if (other.gameObject.CompareTag("PickUpHydro"))
        {
            other.gameObject.SetActive(false);
            Hydro += 5;


        }
    }

    void SetScore()
    {
        ScoreText.text = ": " + Score;
    }
}
