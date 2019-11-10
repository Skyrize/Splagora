using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource SoundPowerUp;
    public AudioSource MusicPhasesSource;

    public static SoundManager Instance = null;

    public float lowPitchRange = .95f;               
    public float highPitchRange = 1.05f;

    private void Awake()
    {
        Debug.Log("music awake");
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }   
    /*
    public void EffectsSoundsPlay(AudioClip clip)
    {
        SoundPowerUp.clip = clip;
        SoundPowerUp.Play();
    }*/

    public void PlaySceneMusic(AudioClip clip)
    {
        Debug.Log("La musique se joue");
        MusicPhasesSource.clip = clip;
        MusicPhasesSource.Play();       
    }

}
