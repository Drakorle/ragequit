using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiPlayerHUD : NetworkBehaviour
{


	void Update () {

        if (isLocalPlayer)
        {
            gameObject.SetActive(false);

        }

    }
}
