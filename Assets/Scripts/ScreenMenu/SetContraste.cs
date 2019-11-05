using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class SetContraste : MonoBehaviour
{        
    public Slider contrastSlider;
    ColorGrading colorGrading;

    void Start()
    {
        PostProcessVolume myProcess = Camera.main.GetComponent<PostProcessVolume>();
        myProcess.profile.TryGetSettings(out colorGrading);
    }      

    public void ModifyContrast(float ContrastValue)
    {
        ContrastValue = contrastSlider.value;
        colorGrading.contrast.value = ContrastValue * 100;       
    }
}
