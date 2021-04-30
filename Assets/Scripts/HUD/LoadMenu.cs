using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;

public class LoadMenu : MonoBehaviour {
    public GameObject pauseMenuUI;
    public GameObject loadMenuUI;
    public playerVariables pv;
    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;

    // Start is called before the first frame update
    void Start()
    {
        // Present the save slots we have
        loadMenuUI.SetActive(false);
        for (int i = 1; i < 4; i++) {
            string saveName = "player" + i + ".save";
            string path = Path.Combine(Application.persistentDataPath, saveName);
            if (File.Exists(path)) {
                if (i == 1) text1.text = File.GetLastWriteTime(path).ToString();
                else if (i == 2) text2.text = File.GetLastWriteTime(path).ToString();
                else if (i == 3) text3.text = File.GetLastWriteTime(path).ToString();
            }
        }
    }



    public void OnEnable() {
        for (int i = 1; i < 4; i++) {
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

    // Load first save slot
    public void OnSlot1Click() {
        pv.LoadPlayer(1);
    }

    // Load second save slot
    public void OnSlot2Click() {
        pv.LoadPlayer(2);
    }

    // Load third save slot
    public void OnSlot3Click() {
        pv.LoadPlayer(3);
    }

    // Go back to pause menu
    public void Back() {
        loadMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

}
