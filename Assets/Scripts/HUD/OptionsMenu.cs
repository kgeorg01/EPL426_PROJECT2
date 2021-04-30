using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

// Option menu of our game
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

    void Start(){
        // Define resolution
        if (res == null){
            res = Screen.resolutions;
            resolutionDropdown.ClearOptions();

            List<string> opts = new List<string>();
            int currentResIndx = 0;
            for (int i = 0; i < res.Length; i++) {
                string opt = res[i].width + " x " + res[i].height;
                opts.Add(opt);
                if (res[i].width == Screen.width && res[i].height == Screen.height) {
                    currentResIndx = i;
                }
            }
            resolutionDropdown.AddOptions(opts);
            resolutionDropdown.value = currentResIndx;
            resolutionDropdown.RefreshShownValue();
        }
    }

    // Set the volume of our game
    public void setVolume (float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        volumeSlider.value = volume;
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    // Set the volume of our background music
    public void setMusic(float volume) {
        PlayerPrefs.SetFloat("music", volume);
        musicSlider.value = volume;
        musicMixer.SetFloat("music", Mathf.Log10(volume) * 20);
    }

    // Set the graphics of the game
    public void setQuality (int qualityIndex)
    {
        PlayerPrefs.SetInt("quality", qualityIndex);
        qualityDropdown.value = qualityIndex;
        qualityDropdown.RefreshShownValue();
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    // Option to go on fullscreen
    public void setFulllscreen (bool isFullscreen)  {
        if (isFullscreen) PlayerPrefs.SetInt("fullscreen", 1);
        else PlayerPrefs.SetInt("fullscreen", 0);
        fullscreenToggle.isOn = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }

    // Set the resolution of our game
    public void setResolution (int resIndx) {
        PlayerPrefs.SetInt("resolution", resIndx);
        if (res == null) Start();
        Resolution resolution = res[resIndx];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        resolutionDropdown.value = resIndx;
        resolutionDropdown.RefreshShownValue();
    }

    // Go back to the previous screen
    public void Back () {
        optionMenuUI.SetActive(false);
        callerUI.SetActive(true);
    }
}
