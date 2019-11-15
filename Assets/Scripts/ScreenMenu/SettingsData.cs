using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SettingsData 
{
    public float Gamma;
    public float Contrast;
    public float Brightness;
   
    
    public SettingsData(SettingsUI settingslight)
    {
        
        Gamma = settingslight.GammaSlider.value;
        Contrast = settingslight.contrastSlider.value;
        Brightness = settingslight.BrightnessSlider.value;
    }

    public SettingsData()
    {
        Gamma = 0.5f;
        Contrast = 0.5f;
        Brightness = 0.5f;
    }
}
