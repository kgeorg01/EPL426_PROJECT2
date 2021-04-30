using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Display amount of potions on screen
public class HUDPotion : MonoBehaviour {
    public TMP_Text text;

    public void SetPotion(int potion) {
        text.text = potion.ToString();
    }
}