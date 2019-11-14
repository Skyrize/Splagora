using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsData 
{
    public Slider GammaSlider;
    public Slider ContrastSlider;
    public Slider BrightnessSlider;

    public void GammaUI(int amount)
    {
        GammaSlider.value += amount;
    }

    public void ContrasteUI (int amount)
    {
        ContrastSlider.value += amount;
    }

    public void BrightnessUI(int amount)
    {
        BrightnessSlider.value += amount;
    }
    
    public SettingsData(SettingsUI settingslight)
    {

    }
}
