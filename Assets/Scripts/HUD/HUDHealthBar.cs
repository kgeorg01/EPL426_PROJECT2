using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// The healthbar of the player on screen
public class HUDHealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    // Define max health of healthbar on slider
    public void SetMaxHealth (int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    // Display correct health on screen
    public void SetHealth(int health) {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
