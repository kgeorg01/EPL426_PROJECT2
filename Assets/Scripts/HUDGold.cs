using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDGold : MonoBehaviour
{

    public TMP_Text text;

    public void SetGold (int gold)
    {
        text.text = gold.ToString(); 
    }
    
}
