using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.EventSystems;

public class SettingsUI : MonoBehaviour
{
    ColorGrading colorGrading;
    public Slider contrastSlider;        
    public Slider BrightnessSlider;
    public Slider GammaSlider;
    public bool inGame;

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
        
        if (PlayerPrefs.GetFloat("Contrast") != 0)
        {
            colorGrading.contrast.value = PlayerPrefs.GetFloat("Contrast");
            contrastSlider.value = PlayerPrefs.GetFloat("Contrast") / MultiplicateurContraste;
        }

        if (PlayerPrefs.GetFloat("BrightValue") != 0)
        {
            RenderSettings.ambientLight = new Color(PlayerPrefs.GetFloat("BrightValue"), PlayerPrefs.GetFloat("BrightValue"), PlayerPrefs.GetFloat("BrightValue"), 1);
            BrightnessSlider.value = PlayerPrefs.GetFloat("BrightValue");
        }

        if (PlayerPrefs.GetFloat("Gamma") != 0)
        {
            colorGrading.toneCurveGamma.value = PlayerPrefs.GetFloat("Gamma");
            GammaSlider.value= PlayerPrefs.GetFloat("Gamma")/MultiplicateurGamma;
        }
    }
    public void Update()
    {
        if(Input.GetMouseButtonUp(0) && inGame)
        {
            EventSystem.current.SetSelectedGameObject(null);

        }
    }
    public void OnMouseUp()
    {
       
    }



    public void ModifyContrast()
    {
        float ContrastValue = contrastSlider.value;
        colorGrading.contrast.value = ContrastValue * MultiplicateurContraste;

        PlayerPrefs.SetFloat("Contrast", ContrastValue*MultiplicateurContraste);

    }

    public void ModifyBright()
    {
        RGBvalue = BrightnessSlider.value;
        RenderSettings.ambientLight = new Color(RGBvalue, RGBvalue, RGBvalue, 1);
        
        PlayerPrefs.SetFloat("BrightValue", RGBvalue);
    }       

    public void ModifyGamma()
    {
        float GammaValue = GammaSlider.value * MultiplicateurGamma;
        colorGrading.toneCurveGamma.value = GammaValue;

        PlayerPrefs.SetFloat("Gamma", GammaValue);

    }

    public void ResetValue()
    {
        colorGrading.toneCurveGamma.value = 1;
        colorGrading.contrast.value = 20;

        PlayerPrefs.SetFloat("Contrast", 20);
        PlayerPrefs.SetFloat("Gamma", 1);

        contrastSlider.value = PlayerPrefs.GetFloat("Contrast") / MultiplicateurContraste;
        GammaSlider.value = PlayerPrefs.GetFloat("Gamma") / MultiplicateurGamma;


    }
}
