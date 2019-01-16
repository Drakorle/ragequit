using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Load1fromCine1 : MonoBehaviour {

    public Image image;
	// Update is called once per frame
	void Update () {
		if(image.color.a == 1)
        {
            SceneManager.LoadScene("level1");
        }
	}
}
