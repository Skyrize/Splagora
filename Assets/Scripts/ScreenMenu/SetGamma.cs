using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class SetGamma : MonoBehaviour
{
    public Slider GammaSlider;
    ColorGrading colorGrading;
    public float MultiplicateurGamma;

    void Start()
    {      
        PostProcessVolume myProcess = Camera.main.GetComponent<PostProcessVolume>();
        myProcess.profile.TryGetSettings(out colorGrading);       
    }

    public void ModifyGamma(float GammaValue)
    {
        GammaValue = GammaSlider.value * MultiplicateurGamma;        
        colorGrading.toneCurveGamma.value = GammaValue;
        Debug.Log("my Slider value" + GammaSlider.value);
        Debug.Log("my Gamma value" + GammaValue);
        Debug.Log("my Color grading value" + colorGrading.gamma.value);
    }
}
