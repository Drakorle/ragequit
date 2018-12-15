using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRespawn : MonoBehaviour {
    public float DeathZone;
    public float SpawnX;
    public float SpawnY;
    public float SpawnZ;
    public RigidBody Rigi;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.y <= DeathZone)
        {
            this.transform.position = new Vector3(
                SpawnX,
                SpawnY,
                SpawnZ
                );
            Rigi.transform.position = new Vector3(
                SpawnX,
                SpawnY,
                SpawnZ
                );
        }

    }
}
