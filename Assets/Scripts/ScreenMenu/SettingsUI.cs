using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class SettingsUI : MonoBehaviour
{
    public Slider contrastSlider;
    ColorGrading colorGrading;
    float MultiplicateurContraste = 100;

    void Start()
    {
        PostProcessVolume myProcess = Camera.main.GetComponent<PostProcessVolume>();
        myProcess.profile.TryGetSettings(out colorGrading);
    }

    public void ModifyContrast(float ContrastValue)
    {
        ContrastValue = contrastSlider.value;
        colorGrading.contrast.value = ContrastValue * MultiplicateurContraste;
    }

    float RGBvalue = 0.5f;
    public Slider BrightnessSlider;

    public void ModifyBright()
    {
        RGBvalue = BrightnessSlider.value;
        RenderSettings.ambientLight = new Color(RGBvalue, RGBvalue, RGBvalue, 1);
    }

    public Slider GammaSlider;    
    float MultiplicateurGamma = 3;   

    public void ModifyGamma(float GammaValue)
    {
        GammaValue = GammaSlider.value * MultiplicateurGamma;
        colorGrading.toneCurveGamma.value = GammaValue;
    }
}
