using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider X_axis_sensitivity;
    public Slider Y_axis_sensitivity;

    public void Start()
    {
        if (!PlayerPrefs.HasKey("X_axis_sensitivity"))
            PlayerPrefs.SetFloat("X_axis_sensitivity", X_axis_sensitivity.value);
        if (!PlayerPrefs.HasKey("Y_axis_sensitivity"))
            PlayerPrefs.SetFloat("Y_axis_sensitivity", Y_axis_sensitivity.value);
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
