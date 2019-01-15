using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocalizedText : MonoBehaviour {

    public string key;
    public static List<LocalizedText> sceneTexts;

    static LocalizedText()
    {
        sceneTexts = new List<LocalizedText>();
    }

	// Use this for initialization
	void Start ()
    {
        sceneTexts.Add(this);
        UpdateText();
	}

    private void UpdateText()
    {
        TMP_Text text = GetComponent<TMP_Text>();
        if (text != null)
            text.text = LocalizationManager.instance.GetLocalizedText(key);
        else
        {
            Text txt = GetComponent<Text>();
            txt.text = LocalizationManager.instance.GetLocalizedText(key);
        }

        AddText followingTextWaiting = this.GetComponent<AddText>();

        if (followingTextWaiting != null)
            followingTextWaiting.AddFollowingText();
    }

    public static void UpdateTexts()
    {
        foreach(LocalizedText text in sceneTexts)
        {
            text.UpdateText();
        }
    }
}
