using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class LoadSettingsMenu : MonoBehaviour
{
    public SettingsUI dataUI;
    public AudioMixer Mixer;
    public Slider GeneralSound;
    public Slider MusicSound;
    public Slider EffetSound;


    void Start()
    {
        Debug.Log(PlayerPrefs.GetFloat("VGeneral"));
        if(PlayerPrefs.GetFloat("VGeneral")!=0)
        Mixer.SetFloat("VGeneral", PlayerPrefs.GetFloat("VGeneral"));

        if (PlayerPrefs.GetFloat("VMusic") != 0)
            Mixer.SetFloat("VMusic", PlayerPrefs.GetFloat("VMusic"));

        if (PlayerPrefs.GetFloat("VEffet") != 0)
            Mixer.SetFloat("VEffet", PlayerPrefs.GetFloat("VEffet"));

        SetSlider();
    }

    // Update is called once per frame
    public void VolumeGeneral()
    {
        float DBGeneral = -60 + (GeneralSound.value * 100);

        Mixer.SetFloat("VGeneral", DBGeneral);
        if (DBGeneral != -10)
        PlayerPrefs.SetFloat("VGeneral", DBGeneral);

    }
    public void VolumeMusique()
    {
        float DBMusic = -60 + (MusicSound.value * 100);

        Mixer.SetFloat("VMusic", DBMusic);
        if (DBMusic != -10)
            PlayerPrefs.SetFloat("VMusic", DBMusic);
    }
    public void VolumeEffet()
    {
        float DBEffet = -60 + (EffetSound.value * 100);

        Mixer.SetFloat("VEffet", DBEffet);
        if (DBEffet != -10)
            PlayerPrefs.SetFloat("VEffet", DBEffet);
    }

    public void SetSlider()
    {
        if (PlayerPrefs.GetFloat("VGeneral") != 0) 
        GeneralSound.value = (PlayerPrefs.GetFloat("VGeneral") + 60) / 100;

        if (PlayerPrefs.GetFloat("VMusic") != 0)
            MusicSound.value = (PlayerPrefs.GetFloat("VMusic") + 60) / 100;

        if (PlayerPrefs.GetFloat("VEffet") != 0)
            EffetSound.value = (PlayerPrefs.GetFloat("VEffet") + 60) / 100;

    }

}
