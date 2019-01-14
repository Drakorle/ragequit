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
        text.text = LocalizationManager.instance.GetLocalizedText(key);
    }

    public static void UpdateTexts()
    {
        foreach(LocalizedText text in sceneTexts)
        {
            text.UpdateText();
        }
    }
}
