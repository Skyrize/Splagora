using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setBrightness : MonoBehaviour
{
    public Slider mainSlider;
    public float GammaCorrection;
   
    void Update() {
        if (GameObject.FindGameObjectWithTag ("brightness")) {
            mainSlider = (Slider) FindObjectOfType(typeof (Slider));
        }
        GammaCorrection = mainSlider.value;
        Debug.Log(GammaCorrection);
        RenderSettings.ambientLight = new Color(mainSlider.value, mainSlider.value, mainSlider.value, 1.0f);
       
    }
   
}
