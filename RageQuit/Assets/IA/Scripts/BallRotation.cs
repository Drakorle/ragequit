using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRotation : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(-60, -120, -180) * Time.deltaTime);
    }
}
