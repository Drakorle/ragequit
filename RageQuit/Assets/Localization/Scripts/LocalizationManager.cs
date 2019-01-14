using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LocalizationManager : MonoBehaviour {

    public static LocalizationManager instance;

    private Dictionary<string, string> localizedText;
    private string missingText = "Missing text";

	void Awake ()
    {
		if (instance == null)
        {
            instance = this;
        }
        //to keep unicity of our instance
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        LoadLocalizedText();
	}
	
	public void LoadLocalizedText()
    {
        localizedText = new Dictionary<string, string>();

        string scene = SceneManager.GetActiveScene().name;
        string language = PlayerPrefs.GetString("localization");
        string filePath = Application.streamingAssetsPath + "/Localizations/" + scene + "/" + language + ".json";

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);

            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(jsonData);

            foreach (LocalizationItem item in loadedData.items)
            {
                localizedText.Add(item.key, item.value);
            }
        }
        else
        {
            Debug.LogError("Error: Cannot find file! " + filePath );
        }
    }

    public string GetLocalizedText(string key)
    {
        string text;

        try
        {
            text = localizedText[key];
        }
        catch (System.Exception)
        {
            text = missingText;
        }

        return text;
    }
}
