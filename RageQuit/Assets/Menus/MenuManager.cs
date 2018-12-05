using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript;

public class MenuManager : MonoBehaviour {

    public GameObject MenuHolder;
    public GameObject CameraBase;
    public GameObject FirstPerson;
    public GameObject ThirdPerson;

    private KeyCode menuAccessKey = KeyCode.Escape;
    private bool menuDisplayed = false;

	public void DisplayMenu()
    {
        MenuHolder.SetActive(true);
        CameraBase.GetComponent<CameraFollower>().enabled = false;
        FirstPerson.GetComponent<FirstPersonView>().enabled = false;
        ThirdPerson.GetComponent<ThirdPersonView>().enabled = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideMenu()
    {
        MenuHolder.SetActive(false);
        CameraBase.GetComponent<CameraFollower>().enabled = true;
        FirstPerson.GetComponent<FirstPersonView>().enabled = true;
        ThirdPerson.GetComponent<ThirdPersonView>().enabled = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(menuAccessKey))
        {
            if (!menuDisplayed)
                DisplayMenu();
            else
                HideMenu();
            menuDisplayed = !menuDisplayed;
        }
    }
}
