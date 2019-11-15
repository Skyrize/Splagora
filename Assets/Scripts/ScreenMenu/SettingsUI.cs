using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class SettingsUI : MonoBehaviour
{
    ColorGrading colorGrading;
    public Slider contrastSlider;        
    public Slider BrightnessSlider;
    public Slider GammaSlider;
    public Slider EffectsSlider;
    public Slider MusicsSlider;

    public Canvas mycanvasmenu;

    float MultiplicateurGamma = 3;
    float MultiplicateurContraste = 100;
    float RGBvalue = 0.5f;
    PostProcessVolume myProcess;

    private void Awake()
    {
        myProcess = Camera.main.GetComponent<PostProcessVolume>();
        myProcess.profile.TryGetSettings(out colorGrading);
        if (!colorGrading)
            Debug.Log("null");

    }

    void Start()
    {
    }
    
    public void SaveSettings()
    {
        Debug.Log("save");
        SaveSystem.SaveSettings(this);
    }   

    public void LoadMenuSettings()
    {
        SettingsData data = SaveSystem.LoadData();       

        contrastSlider.value = data.Contrast;
        BrightnessSlider.value = data.Brightness;
        GammaSlider.value = data.Gamma;
        MusicsSlider.value = data.Musics;
        EffectsSlider.value = data.Effects;
    }

    public void LoadGameSettings()
    {
        SettingsData data = SaveSystem.LoadData();

        RenderSettings.ambientLight = new Color(data.Brightness, data.Brightness, data.Brightness, 1);
        colorGrading.toneCurveGamma.value = data.Gamma;
        colorGrading.contrast.value =
            data.Contrast * MultiplicateurContraste;       
    }

    public void ModifyContrast(float ContrastValue)
    {
        ContrastValue = contrastSlider.value;
        if (!colorGrading)
            Debug.Log("allo");
        colorGrading.contrast.value = 
            ContrastValue * MultiplicateurContraste;
    }   

    public void ModifyBright()
    {
        RGBvalue = BrightnessSlider.value;
        RenderSettings.ambientLight = new Color(RGBvalue, RGBvalue, RGBvalue, 1);
    }       

    public void ModifyGamma(float GammaValue)
    {
        GammaValue = GammaSlider.value * MultiplicateurGamma;
        colorGrading.toneCurveGamma.value = GammaValue;
    }    
}
