using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
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
        resolutionDropdown.value = currentResIndx;
        resolutionDropdown.RefreshShownValue();
    }

    public void setVolume (float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void setQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void setFulllscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void setResolution (int resIndx)
    {
        Resolution resolution = res[resIndx];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);


    }

    public void Back ()
    {
        optionMenuUI.SetActive(false);
        callerUI.SetActive(true);
        
        //string sceneName = PlayerPrefs.GetString("optionSceneCaller");
        //SceneManager.LoadScene(sceneName);
    }

}
