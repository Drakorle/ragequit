using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlKeySelection : MonoBehaviour {

    public string PlayerPrefName;
    public static string[] PlayerPrefNames = new string[]
    {
        "ForwardKey",
        "BackKey",
        "LeftKey",
        "RightKey",
        "JumpKey",
        "SprintKey",
        "CrouchKey",
        "ViewSwitchKey",
        "FreeViewKey"
    };

    private bool detectKey;

	// Use this for initialization
	void Start () {
        detectKey = false;
        ChangeKey(PlayerPrefs.GetString(PlayerPrefName));
    }

    public void StartDetection()
    {
        detectKey = true;
        ChangeKey("Press a key");
    }
    
    void OnGUI()
    {
        if (detectKey)
        {
            Event e = Event.current;

            if (e.isKey || e.isMouse)
            {
                if (e.isKey)
                {
                    bool notUsedYet = true;
                    foreach (string pref in PlayerPrefNames)
                    {
                        if (PlayerPrefs.GetString(pref) == e.keyCode.ToString())
                        {
                            if (PlayerPrefs.GetString(PlayerPrefName) != e.keyCode.ToString())
                            {
                                notUsedYet = false;
                                string errorMessage = "Key " + e.keyCode + " already bind to '" + pref + "'";
                                transform.parent.parent.parent.parent.parent.parent.Find("MainMenu").GetComponent<MainMenu>().Message(errorMessage);
                                break;
                            }
                        }
                    }
                    if (notUsedYet)
                        MainMenu.UpdateOption(PlayerPrefName, e.keyCode.ToString());
                }

                detectKey = false;
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        if (!detectKey)
            ChangeKey(PlayerPrefs.GetString(PlayerPrefName));
    }

    //get & change the textKey
    private void ChangeKey(string newVal)
    {
        Transform child = transform.Find("Key");
        Text t = child.GetComponent<Text>();
        t.text = newVal;
    }
}
