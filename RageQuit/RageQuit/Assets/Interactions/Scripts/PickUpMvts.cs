using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PickUpMvts: MonoBehaviour {

    private void Start()
    {
    }
    // Update is called once per frame
    void Update ()
    {
        transform.Rotate(new Vector3(30, 60, 90) * Time.deltaTime);
	}

    

}
