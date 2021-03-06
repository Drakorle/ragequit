﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour {

    public View CurrentView = View.ThirdPerson; //default view can be specified with this parameter
    public GameObject FirstPersonObj;
    public GameObject ThirdPersonObj;

    private KeyCode ViewSwitchKey;
    private bool keyPreviouslyReleased;
	
    void Start()
    {
        ViewSwitchKey = MainMenu.GetKeyCode("ViewSwitchKey");

        //disable all script by default, and enable only CurrenView script
        for (int i = 0; i < Enum.GetValues(typeof(View)).Length; i++)
        {
            GetViewObject((View)i).SetActive(false);
        }
        GetViewObject(CurrentView).SetActive(true);
        keyPreviouslyReleased = true;
    }

	// Update is called once per frame
	void Update ()
    {
        bool switchKeyPressed = Input.GetKey(ViewSwitchKey);
        if (switchKeyPressed && keyPreviouslyReleased)
        {
            CurrentView = (View)(((int)CurrentView + 1) % 2); // 2 = count of View values
            UpdateView();
            keyPreviouslyReleased = false;
        }
        else if (!switchKeyPressed && !keyPreviouslyReleased)
            keyPreviouslyReleased = true;
    }

    private void UpdateView()
    {
        View oldView = CurrentView - 1;
        GetViewObject(oldView).SetActive(false);
        GetViewObject(CurrentView).SetActive(true);
    }

    private GameObject GetViewObject(View view)
    {
        switch (view)
        {
            case View.FirstPerson:
                return FirstPersonObj;
            default:
                return ThirdPersonObj;
        }
    }

    public enum View
    {
        FirstPerson = 0,
        ThirdPerson = 1
    }
}
