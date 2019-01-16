using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonView : MonoBehaviour {

    public GameObject PlayerView;
    public float DistanceFromPlayer = 0.3f;

	// Use this for initialization
	void OnEnable ()
    {
        PlayerView.transform.localPosition = new Vector3(PlayerView.transform.localPosition.x, PlayerView.transform.localPosition.y, DistanceFromPlayer);
    }
}
