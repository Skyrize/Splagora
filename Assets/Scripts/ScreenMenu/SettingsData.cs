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
}
