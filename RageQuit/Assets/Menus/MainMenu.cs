using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject MessageObject;

    //CONTROLS

    //movements
    public Slider X_axis_sensitivity;
    public float Default_X_axis_sensitivity = 1f;
    public float Min_X_axis_sensitivity = 0.1f;
    public float Max_X_axis_sensitivity = 2f;
    public Slider Y_axis_sensitivity;
    public float Default_Y_axis_sensitivity = 1f;
    public float Min_Y_axis_sensitivity = 0.1f;
    public float Max_Y_axis_sensitivity = 2f;
    public KeyCode DefaultForwardKey;
    public KeyCode DefaultBackKey;
    public KeyCode DefaultLeftKey;
    public KeyCode DefaultRightKey;
    public KeyCode DefaultJumpKey;
    public KeyCode DefaultSprintKey;
    public KeyCode DefaultCrouchKey;

    //camera
    public KeyCode DefaultViewSwitchKey;
    public KeyCode DefaultFreeViewKey;

    //VOLUME

    public Slider Main_Volume;
    public float Default_Main_Volume = 1f;
    public float Min_Main_Volume = 0.1f;
    public float Max_Main_Volume = 2f;

    public Slider Music_Volume;
    public float Default_Music_Volume = 1f;
    public float Min_Music_Volume = 0.1f;
    public float Max_Music_Volume = 2f;

    public Slider Environment_Volume;
    public float Default_Environment_Volume = 1f;
    public float Min_Environment_Volume = 0.1f;
    public float Max_Environment_Volume = 2f;

    //GRAPHICS
    public Slider Refresh_Rate;
    public int Default_Refresh_Rate = 60;

    public void Start()
    {
        LoadControlOptions();
        LoadGraphicOptions();
        LoadSoundOptions();
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadControlOptions()
    {
        if (!PlayerPrefs.HasKey("X_axis_sensitivity"))
            PlayerPrefs.SetFloat("X_axis_sensitivity", Default_X_axis_sensitivity);
        if (!PlayerPrefs.HasKey("Y_axis_sensitivity"))
            PlayerPrefs.SetFloat("Y_axis_sensitivity", Default_Y_axis_sensitivity);

        if (!PlayerPrefs.HasKey("ForwardKey"))
            PlayerPrefs.SetString("ForwardKey", DefaultForwardKey.ToString());
        if (!PlayerPrefs.HasKey("BackKey"))
            PlayerPrefs.SetString("BackKey", DefaultBackKey.ToString());
        if (!PlayerPrefs.HasKey("LeftKey"))
            PlayerPrefs.SetString("LeftKey", DefaultLeftKey.ToString());
        if (!PlayerPrefs.HasKey("RightKey"))
            PlayerPrefs.SetString("RightKey", DefaultRightKey.ToString());
        if (!PlayerPrefs.HasKey("JumpKey"))
            PlayerPrefs.SetString("JumpKey", DefaultJumpKey.ToString());
        if (!PlayerPrefs.HasKey("SprintKey"))
            PlayerPrefs.SetString("SprintKey", DefaultSprintKey.ToString());
        if (!PlayerPrefs.HasKey("CrouchKey"))
            PlayerPrefs.SetString("CrouchKey", DefaultCrouchKey.ToString());

        if (!PlayerPrefs.HasKey("ViewSwitchKey"))
            PlayerPrefs.SetString("ViewSwitchKey", DefaultViewSwitchKey.ToString());
        if (!PlayerPrefs.HasKey("FreeViewKey"))
            PlayerPrefs.SetString("FreeViewKey", DefaultFreeViewKey.ToString());
            
        X_axis_sensitivity.maxValue = Max_X_axis_sensitivity;
        X_axis_sensitivity.minValue = Min_X_axis_sensitivity;

        Y_axis_sensitivity.maxValue = Max_Y_axis_sensitivity;
        Y_axis_sensitivity.minValue = Min_Y_axis_sensitivity;

        X_axis_sensitivity.value = PlayerPrefs.GetFloat("X_axis_sensitivity");
        Y_axis_sensitivity.value = PlayerPrefs.GetFloat("Y_axis_sensitivity");
    }
    public void LoadGraphicOptions()
    {
        if (!PlayerPrefs.HasKey("Screen_Refresh_Rate"))
            PlayerPrefs.SetInt("Screen_Refresh_Rate", Default_Refresh_Rate);

        PlayerPrefs.Save();
    }
    public void LoadSoundOptions()
    {
        if (!PlayerPrefs.HasKey("Main_Volume"))
            PlayerPrefs.SetFloat("Main_Volume", Default_Main_Volume);

        Main_Volume.maxValue = Max_Main_Volume;
        Main_Volume.minValue = Min_Main_Volume;
        Main_Volume.value = PlayerPrefs.GetFloat("Main_Volume");

        if (!PlayerPrefs.HasKey("Music_Volume"))
            PlayerPrefs.SetFloat("Music_Volume", Default_Music_Volume);

        Music_Volume.maxValue = Max_Music_Volume;
        Music_Volume.minValue = Min_Music_Volume;
        Music_Volume.value = PlayerPrefs.GetFloat("Music_Volume");

        if (!PlayerPrefs.HasKey("Environment_Volume"))
            PlayerPrefs.SetFloat("Environment_Volume", Default_Environment_Volume);

        Environment_Volume.maxValue = Max_Environment_Volume;
        Environment_Volume.minValue = Min_Environment_Volume;
        Environment_Volume.value = PlayerPrefs.GetFloat("Environment_Volume");
    }

    public void UpdateOption(string option)
    {
        switch(option)
        {
            case "X_axis_sensitivity":
                PlayerPrefs.SetFloat(option, X_axis_sensitivity.value);
                break;
            case "Y_axis_sensitivity":
                PlayerPrefs.SetFloat(option, Y_axis_sensitivity.value);
                break;
            case "Main_Volume":
                PlayerPrefs.SetFloat(option, Main_Volume.value);
                break;
            case "Music_Volume":
                PlayerPrefs.SetFloat(option, Music_Volume.value);
                break;
            case "Environment_Volume":
                PlayerPrefs.SetFloat(option, Environment_Volume.value);
                break;
        }
        PlayerPrefs.Save();
    }

    public static void UpdateOption(string option, string value)
    {
        PlayerPrefs.SetString(option, value);
        PlayerPrefs.Save();
    }


    public void ResetAll()
    {
        foreach(string pref in ControlKeySelection.PlayerPrefNames)
        {
            PlayerPrefs.DeleteKey(pref);
        }
        LoadControlOptions();
    }

    public void Message(string message)
    {
        MessageObject.SetActive(true);
        Text t = MessageObject.transform.Find("Text").GetComponent<Text>();
        t.text = message;
    }

    public static KeyCode GetKeyCode(string prefName)
    {
        return (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(prefName));
    }
}
