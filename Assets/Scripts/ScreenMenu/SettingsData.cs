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
    public float Musics;
    public float Effects;
   
    
    

    public SettingsData()
    {
        Gamma = 0.5f;
        Contrast = 0.5f;
        Brightness = 0.5f;
        Musics = 0.5f;
        Effects = 0.5f;
    }
}
