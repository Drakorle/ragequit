using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript;

public class MenuManager : MonoBehaviour {

    public GameObject MenuHolder;
    public GameObject CameraBase;

    private KeyCode menuAccessKey = KeyCode.Escape;
    private bool menuDisplayed = false;

	public void DisplayMenu()
    {
        MenuHolder.SetActive(true);
        CameraBase.GetComponent<CameraFollower>().IsInMenu(true);
    }

    public void HideMenu()
    {
        MenuHolder.SetActive(false);
        CameraBase.GetComponent<CameraFollower>().IsInMenu(false);
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
