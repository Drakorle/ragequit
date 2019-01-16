using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : MonoBehaviour {

    public GameObject[] ToDisable;
    public GameObject[] ToLoad;
    static GameObject[] sToDisable;
    static GameObject[] sToLoad;

    static MenuManager LocalMenuManager;

    void Start()
    {
        sToDisable = ToDisable;
        sToLoad = ToLoad;

        LocalMenuManager = transform.GetComponent<MenuManager>();
    }

	public static void DisplayMenu()
    {
        foreach(GameObject go in sToDisable)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in sToLoad)
        {
            go.SetActive(true);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        LocalMenuManager.CameraBase.GetComponent<CameraFollower>().enabled = false;
        LocalMenuManager.FirstPerson.GetComponent<FirstPersonView>().enabled = false;
        LocalMenuManager.ThirdPerson.GetComponent<ThirdPersonView>().enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
            DisplayMenu();
    }
}
