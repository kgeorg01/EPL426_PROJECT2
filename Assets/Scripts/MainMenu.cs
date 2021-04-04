using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenuUI;
    public GameObject optionMenuUI;
    public void PlayGame () {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OptionMenu()
    {
        mainMenuUI.SetActive(false);
        optionMenuUI.SetActive(true);
        //PlayerPrefs.SetString("optionSceneCaller", SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene("OptionMenu");

    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();

    }

}
