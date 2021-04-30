using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// The shield bar displayed on our screen, similar to health bar
public class HUDShieldBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxShield(int maxshield)
    {
        slider.maxValue = maxshield;
        slider.value = maxshield;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    public void SetShield(int shield)
    {
        slider.value = shield;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
