using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTheme : MonoBehaviour {

	void Start () {
        FindObjectOfType<AudioManager>().Play("MenuTheme");
        FindObjectOfType<AudioManager>().Stop("Theme");
    }

}
