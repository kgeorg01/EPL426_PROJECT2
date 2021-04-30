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

    // Enter the game
    public void PlayGame () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Based on options change game settings
    private void LoadOptionPreferences()
    {
        OptionsMenu[] optionMenus = GameObject.FindObjectsOfType<OptionsMenu>(true);
        OptionsMenu optionMenu = optionMenus[0];

        // set volume of game
        try{
            optionMenu.setVolume(PlayerPrefs.GetFloat("volume"));
        }
        catch (Exception e){
            Debug.LogWarning("Cant load preferences volume");
        }

        // set backgroun music of game
        try{
            optionMenu.setMusic(PlayerPrefs.GetFloat("music"));
        }
        catch (Exception e) {
            Debug.LogWarning("Cant load preferences music");
        }

        // Set graphics of game
        try {
            optionMenu.setQuality(PlayerPrefs.GetInt("quality"));
        }
        catch (Exception e) {
            Debug.LogWarning("Cant load preferences quality");
        }

        // Enter fullscreen
        try {
            if (PlayerPrefs.GetInt("fullscreen") == 1) optionMenu.setFulllscreen(true);
            else optionMenu.setFulllscreen(false);
        }
        catch (Exception e) {
            Debug.LogWarning("Cant load preferences fullscreen");
        }

        // Set resolution of game
        try {
        optionMenu.setResolution(PlayerPrefs.GetInt("resolution"));
        }
        catch (Exception e) {
          Debug.LogWarning("Cant load preferences resolution");
        }
    }

    // Load options menu
    public void OptionMenu() {
        mainMenuUI.SetActive(false);
        optionMenuUI.SetActive(true);
    }

    // Load main menu
    public void LoadMenu () {
        mainMenuUI.SetActive(false);
        loadMenuUI.SetActive(true);
        Debug.Log("Load");
    }

    // Exit game
    public void QuitGame() {
        Debug.Log("QUIT");
        Application.Quit();

    }
}
