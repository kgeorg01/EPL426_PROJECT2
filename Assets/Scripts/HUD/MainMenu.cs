using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenuUI;
    public GameObject optionMenuUI;
    public GameObject loadMenuUI;

    public void Start()
    {
        LoadOptionPreferences();
    }

    public void PlayGame () {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void LoadOptionPreferences()
    {
        //GameObject [] optionMenuUI = GameObject.FindGameObjectsWithTag("OptionMenu");
        OptionsMenu[] optionMenus = GameObject.FindObjectsOfType<OptionsMenu>(true);
        Debug.Log("Options");
        Debug.Log(optionMenus.Length);
        OptionsMenu optionMenu = optionMenus[0];

        try
        {
            optionMenu.setVolume(PlayerPrefs.GetFloat("volume"));
        }
        catch (Exception e)
        {
            Debug.LogWarning("Cant load preferences volume");
        }

        try
        {
            optionMenu.setMusic(PlayerPrefs.GetFloat("music"));
        }
        catch (Exception e)
        {
            Debug.LogWarning("Cant load preferences music");
        }

        try
        {
            optionMenu.setQuality(PlayerPrefs.GetInt("quality"));
        }
        catch (Exception e)
        {
            Debug.LogWarning("Cant load preferences quality");
        }

        try
        {
            if (PlayerPrefs.GetInt("fullscreen") == 1) optionMenu.setFulllscreen(true);
            else optionMenu.setFulllscreen(false);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Cant load preferences fullscreen");
        }

        try
        {
            optionMenu.setResolution(PlayerPrefs.GetInt("resolution"));
        }
        catch (Exception e)
        {
            Debug.LogWarning("Cant load preferences resolution");
        }

    }
    public void OptionMenu()
    {
        mainMenuUI.SetActive(false);
        optionMenuUI.SetActive(true);
        //PlayerPrefs.SetString("optionSceneCaller", SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene("OptionMenu");

    }

    public void LoadMenu ()
    {
        mainMenuUI.SetActive(false);
        loadMenuUI.SetActive(true);
        Debug.Log("Load");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();

    }

}
