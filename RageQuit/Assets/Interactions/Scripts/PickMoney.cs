using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickMoney : MonoBehaviour {

    public Text ScoreText;
    private int Score;

	// Use this for initialization
	void Start ()
    {
        Score = 0;
	}
	
	// Update is called once per frame
	void Update () {

        SetScore();
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            Score += 1;
            

        }
    }

    void SetScore()
    {
        ScoreText.text = "Score: " + Score;
    }
}
