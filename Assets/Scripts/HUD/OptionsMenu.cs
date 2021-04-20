using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class OptionsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public AudioMixer musicMixer;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    public Slider volumeSlider;
    public Slider musicSlider;
    public Toggle fullscreenToggle;
    public GameObject callerUI;
    public GameObject optionMenuUI;

    Resolution[] res;

    void Start()
    {

       // callerUI.SetActive(false);
       //optionMenuUI.SetActive(false);

        res = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> opts = new List<string>();
        int currentResIndx = 0;
        for (int i =0; i<res.Length;i++)
        {
            string opt = res[i].width + " x " + res[i].height;
            opts.Add(opt);

            if (res[i].width == Screen.width && res[i].height==Screen.height)
            {
                currentResIndx = i;
            }

        }

        resolutionDropdown.AddOptions(opts);
       // resolutionDropdown.value = currentResIndx;
        //resolutionDropdown.RefreshShownValue();

    }

    //public void OnEnabled()
    //{
    //    LoadOptionPreferences();
    //}

    private void LoadOptionPreferences()
    {
        //GameObject [] optionMenuUI = GameObject.FindGameObjectsWithTag("OptionMenu");
        OptionsMenu[] optionMenus = GameObject.FindObjectsOfType<OptionsMenu>(true);
        Debug.Log("Options");
        Debug.Log(optionMenus.Length);
        OptionsMenu optionMenu = optionMenus[0];

        try{
            optionMenu.setVolume(PlayerPrefs.GetFloat("volume"));
        }
        catch (Exception e) {
            Debug.LogWarning("Cant load preferences volume");
        }

        try{
            optionMenu.setMusic(PlayerPrefs.GetFloat("music"));
        }
        catch (Exception e){
            Debug.LogWarning("Cant load preferences music");
        }

        try {
            optionMenu.setQuality(PlayerPrefs.GetInt("quality"));
        }
        catch (Exception e) {
            Debug.LogWarning("Cant load preferences quality");
        }

        try{
            if (PlayerPrefs.GetInt("fullscreen") == 1) optionMenu.setFulllscreen(true);
            else optionMenu.setFulllscreen(false);
        }
        catch (Exception e){
            Debug.LogWarning("Cant load preferences fullscreen");
        }

       // try{
            optionMenu.setResolution( PlayerPrefs.GetInt("resolution"));
       // }
       // catch (Exception e){
        //    Debug.LogWarning("Cant load preferences resolution");
       // } 

    }

    public void setVolume (float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        volumeSlider.value = volume;
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }
    public void setMusic(float volume)
    {
        PlayerPrefs.SetFloat("music", volume);
        musicSlider.value = volume;
        musicMixer.SetFloat("music", Mathf.Log10(volume) * 20);
    }
    public void setQuality (int qualityIndex)
    {
        PlayerPrefs.SetInt("quality", qualityIndex);
        qualityDropdown.value = qualityIndex;
        qualityDropdown.RefreshShownValue();
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void setFulllscreen (bool isFullscreen)
    {
        if (isFullscreen) PlayerPrefs.SetInt("fullscreen", 1);
        else PlayerPrefs.SetInt("fullscreen", 0);
        fullscreenToggle.isOn = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }

    public void setResolution (int resIndx)
    {
        PlayerPrefs.SetInt("resolution", resIndx);
        Debug.Log(resIndx);
        if (res == null) Start();
        Resolution resolution = res[resIndx];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        resolutionDropdown.value = resIndx;
        resolutionDropdown.RefreshShownValue();
    }

    public void Back ()
    {
        optionMenuUI.SetActive(false);
        callerUI.SetActive(true);
        
        //string sceneName = PlayerPrefs.GetString("optionSceneCaller");
        //SceneManager.LoadScene(sceneName);
    }

}
