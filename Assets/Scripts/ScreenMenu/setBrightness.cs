using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setBrightness : MonoBehaviour
{
    public float RGBvalue;    
    public Slider mainSlider;

    public void SettingsBright()
    {
        RGBvalue = mainSlider.value;
        RenderSettings.ambientLight = new Color(RGBvalue, RGBvalue, RGBvalue, 1);
    }
}
