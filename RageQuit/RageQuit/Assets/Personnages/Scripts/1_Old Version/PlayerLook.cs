using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour {

    public Transform player;
    public float mouseSensitivity;
    float xAxisClamp = 0.0f;

	// Use this for initialization
	void Awake ()
    {
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update ()
    {
        RotateCamera();
	}

    void RotateCamera()
    {
        //On recupère les mouvement de la souris
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        //On créer les valeurs de rotations
        float rotAmountX = mouseX * mouseSensitivity;
        float rotAmountY = mouseY * mouseSensitivity;
        
        //Evite les probleme de bornes
        xAxisClamp -= rotAmountY;

        // Création des Vectors3 (Euler va de 0 à 360 contrairement à normal de 180 à -180)
        Vector3 targetRotCam = transform.rotation.eulerAngles;
        Vector3 targetRotplayer = player.rotation.eulerAngles;

        //On affect les valeurs de rotations 
        targetRotCam.x -= rotAmountY;
        targetRotCam.z = 0;
        targetRotplayer.y += rotAmountX;

        //Evite les probleme de bornes
        if (xAxisClamp > 90)
        {
            xAxisClamp = 90;
            targetRotCam.x = 90;
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            targetRotCam.x = 270;
        }

        // Modifie les paramètres du joueur sur l'axe y et tout pour la cam
        transform.rotation = Quaternion.Euler(targetRotCam);
        player.rotation = Quaternion.Euler(targetRotplayer);

    }
}
