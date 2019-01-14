using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLanguagePref : MonoBehaviour
{

    public Dropdown languageList;

    void Start ()
    {
        languageList.value = PlayerPrefs.GetInt("localizationDropdownID");
    }

	public void SetPref()
    {
        string selectedLanguage = languageList.options[languageList.value].text;
        string pref;

        switch (selectedLanguage)
        {
            case "Français":
                pref = "fr";
                break;
            default:
                pref = "en";
                break;
        }

        PlayerPrefs.SetString("localization", pref);
        PlayerPrefs.SetInt("localizationDropdownID", languageList.value);

        PlayerPrefs.Save();

        LocalizationManager.instance.LoadLocalizedText();
        LocalizedText.UpdateTexts();
    }
}
