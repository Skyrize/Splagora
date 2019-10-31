using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setBrightness : MonoBehaviour
{
    float RGBvalue = 0.5f;    
    public Slider mainSlider;

    public void SettingsBright()
    {
        RGBvalue = mainSlider.value;
        RenderSettings.ambientLight = new Color(RGBvalue, RGBvalue, RGBvalue, 1);
    }
}
