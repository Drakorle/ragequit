using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider X_axis_sensitivity;
    public float Default_X_axis_sensitivity = 1f;
    public float Min_X_axis_sensitivity = 0.1f;
    public float Max_X_axis_sensitivity = 2f;
    public Slider Y_axis_sensitivity;
    public float Default_Y_axis_sensitivity = 1f;
    public float Min_Y_axis_sensitivity = 0.1f;
    public float Max_Y_axis_sensitivity = 2f;

    public void Start()
    {
        if (!PlayerPrefs.HasKey("X_axis_sensitivity"))
            PlayerPrefs.SetFloat("X_axis_sensitivity", Default_X_axis_sensitivity);
        if (!PlayerPrefs.HasKey("Y_axis_sensitivity"))
            PlayerPrefs.SetFloat("Y_axis_sensitivity", Default_Y_axis_sensitivity);

        X_axis_sensitivity.maxValue = Max_X_axis_sensitivity;
        X_axis_sensitivity.minValue = Min_X_axis_sensitivity;

        Y_axis_sensitivity.maxValue = Max_Y_axis_sensitivity;
        Y_axis_sensitivity.minValue = Min_Y_axis_sensitivity;

        X_axis_sensitivity.value = PlayerPrefs.GetFloat("X_axis_sensitivity");
        Y_axis_sensitivity.value = PlayerPrefs.GetFloat("Y_axis_sensitivity");
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
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
        }
    }
}
