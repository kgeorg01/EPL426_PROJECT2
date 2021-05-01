using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;

public class SaveMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject saveMenuUI;
    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    public playerVariables pv;

    void Start()
    {
        saveMenuUI.SetActive(false);

        for (int i = 1; i < 4; i++)
        {

            string saveName = "player" + i + ".save";
            string path = Path.Combine(Application.persistentDataPath, saveName);
            if (File.Exists(path))
            {
                if (i == 1) text1.text = File.GetLastWriteTime(path).ToString();
                else if (i == 2) text2.text = File.GetLastWriteTime(path).ToString();
                else if (i == 3) text3.text = File.GetLastWriteTime(path).ToString();
            }
        }
    }

    // Update the time on saved slots every time the player open the save menu
    public void OnEnable()
    {
        for (int i = 1; i < 4; i++)
        {

            string saveName = "player" + i + ".save";
            string path = Path.Combine(Application.persistentDataPath, saveName);
            if (File.Exists(path))
            {
                if (i == 1) text1.text = File.GetLastWriteTime(path).ToString();
                else if (i == 2) text2.text = File.GetLastWriteTime(path).ToString();
                else if (i == 3) text3.text = File.GetLastWriteTime(path).ToString();
            }
        }
    }

    //Save on the first slot
    public void OnSlot1Click ()
    {
        text1.text = DateTime.Now.ToString();
        pv.SavePlayer(1);
    }
    //Save on the second slot
    public void OnSlot2Click()
    {
        text2.text = DateTime.Now.ToString();
        pv.SavePlayer(2);
    }
    //Save on the third slot
    public void OnSlot3Click()
    {
        text3.text = DateTime.Now.ToString();
        pv.SavePlayer(3);
    }

    //back to main menu
    public void Back()
    {
        saveMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

}
