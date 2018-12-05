using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenOptionsSelector : MonoBehaviour {

    public Dropdown ScreenMode;
    public Dropdown Resolution;

    public TextMeshProUGUI RefreshRateText;
    public Slider RefreshRate;
    public int Min_Refresh_Rate = 15;
    public int Max_Refresh_Rate = 120;

    private int defaultWidth, defaultHeight;


    void Start()
    {
        //Save default size
        defaultWidth = Screen.width;
        defaultHeight = Screen.height;

        Dropdown.OptionData newOption = new Dropdown.OptionData();
        newOption.text = "Default: " +  defaultWidth + " * " + defaultHeight;
        Resolution.options.Add(newOption);
        Resolution.value = Resolution.options.Count - 1;

        //Initialize Dropdown values (not the saved ones !)
        ScreenMode.value = (int) Screen.fullScreenMode - 1;

        //Initialize Slider values (saved ones ^^)
        RefreshRate.maxValue = Max_Refresh_Rate;
        RefreshRate.minValue = Min_Refresh_Rate;
        RefreshRate.value = PlayerPrefs.GetInt("Screen_Refresh_Rate");
        RefreshRateText.text = RefreshRate.value + " Hz";

        //Add listener for when the value of the Dropdown changes, to take action
        ScreenMode.onValueChanged.AddListener(delegate {
            ScreenModeChanged();
        });

        Resolution.onValueChanged.AddListener(delegate {
            ResolutionChanged();
        });

        RefreshRate.onValueChanged.AddListener(delegate {
            RefreshRateChanged();
        });
    }

    //Ouput the new value of the Dropdown into Text
    void DropdownValueChanged(string option, Dropdown dropdown)
    {
        PlayerPrefs.SetInt(option, dropdown.value);
        PlayerPrefs.Save();
    }

    void ScreenModeChanged()
    {
        DropdownValueChanged("Screen_Mode", ScreenMode);

        UpdateScreen();
    }

    void ResolutionChanged()
    {
        DropdownValueChanged("Screen_Resolution", Resolution);

        UpdateScreen();
    }

    void RefreshRateChanged()
    {
        PlayerPrefs.SetInt("Screen_Refresh_Rate", (int) RefreshRate.value);
        RefreshRateText.text = RefreshRate.value + " Hz";

        UpdateScreen();
    }

    public void UpdateScreen()
    {
        //screenMode

        FullScreenMode mode = (FullScreenMode) PlayerPrefs.GetInt("Screen_Mode");

        //resolution
        int width, height;
        IntToResolution(PlayerPrefs.GetInt("Screen_Resolution"), out width, out height);

        //refresh rate
        int refreshRate = PlayerPrefs.GetInt("Screen_Refresh_Rate");

        //update screen
        Screen.fullScreenMode = mode;
        Screen.SetResolution(width, height, (mode == FullScreenMode.ExclusiveFullScreen) ? true : false, refreshRate);
        
    }

    public void IntToResolution (int id, out int width, out int height)
    {
        switch (id)
        {
            case 0:
                width = 1920;
                height = 1080;
                break;
            case 1:
                width = 1680;
                height = 1050;
                break;
            case 2:
                width = 1440;
                height = 900;
                break;
            case 3:
                width = 800;
                height = 600;
                break;
            case 4:
                width = 720;
                height = 480;
                break;
            case 5:
                width = 640;
                height = 480;
                break;
            default:
                width = defaultWidth;
                height = defaultHeight;
                break;
        }
    }

    public void ResolutionToInt (out int id, int width)
    {
        switch (width)
        {
            case 1920:
                id = 0;
                break;
            case 1680:
                id = 1;
                break;
            case 1440:
                id = 2;
                break;
            case 800:
                id = 3;
                break;
            case 720:
                id = 4;
                break;
            case 640:
                id = 5;
                break;
            default:
                id = 6;
                break;
        }
    }
}
