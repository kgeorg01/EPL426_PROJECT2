using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool gamePaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionMenuUI;
    public GameObject saveMenuUI;
    public GameObject loadMenuUI;
    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                resume();
            } else
            {
                pause();
            }

        }

    }

   public void resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

   void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void loadMenu () // MAIN MENU
    {
        Debug.Log("MainMenu");
        Time.timeScale = 1f;
        gamePaused = false;
        SceneManager.LoadScene("MainMenu");

    }
    public void OptionMenu()
    {
        
        pauseMenuUI.SetActive(false);
        optionMenuUI.SetActive(true);
        //PlayerPrefs.SetString("optionSceneCaller", SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene("OptionMenu" );

    }

    public void SaveMenu()
    {

        pauseMenuUI.SetActive(false);
        saveMenuUI.SetActive(true);
        
    }

    public void LoadMenu() //Load Game Menu
    {
        pauseMenuUI.SetActive(false);
        loadMenuUI.SetActive(true);
    }

    public void quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
