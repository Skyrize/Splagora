using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource SoundPowerUp;
    public AudioSource TramSound;
    public AudioSource WaveSound;
    public AudioSource BumperSound;
    //public AudioSource PoliceSound;
    public AudioSource timerGong;
    public AudioSource playerSoundCollision;
    public AudioSource MusicPhaseSource1;
    public AudioSource MusicPhaseSource2;
    public AudioSource MusicPhaseSource3;

    public static SoundManager Instance = null;

    public Transform[] spawnersTram;

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
    
    public void PowerUpSound(AudioClip clip)
    {
        SoundPowerUp.clip = clip;
        SoundPowerUp.Play();
    }

    public void TramSoundComing(AudioClip clip, int SideTram)
    {
        TramSound.clip = clip;
        AudioSource.PlayClipAtPoint(clip, spawnersTram[SideTram].position);
        Debug.Log("Position Sound" + spawnersTram[SideTram].position);
    }

    public void WaveSoundEffect(AudioClip clip)
    {
        WaveSound.clip = clip;
        WaveSound.Play();
    }

    public void SoundBumper(AudioClip clip)
    {
        BumperSound.clip = clip;
        BumperSound.Play();
    }
    /*
    public void SoundPolice(AudioClip clip)
    {
        PoliceSound.clip = clip;
        PoliceSound.Play();
    }*/

    public void SoundGong(AudioClip clip)
    {
        timerGong.clip = clip;
        timerGong.Play();
        MusicPhaseSource1.volume = 0.3f;
        MusicPhaseSource2.volume = 0.3f;
        MusicPhaseSource3.volume = 0.3f;
    }

    public void SoundPlayersCollision(AudioClip clip)
    {
        playerSoundCollision.clip = clip;
        playerSoundCollision.Play();
    }

    public void PlaySceneMusic1(AudioClip clip)
    {
        MusicPhaseSource1.clip = clip;
        MusicPhaseSource2.volume = 1f;
        MusicPhaseSource1.Play();       
    }

    public void StopSceneMusic1(AudioClip clip)
    {
        MusicPhaseSource1.clip = clip;
        MusicPhaseSource1.Stop();
    }

    public void PlaySceneMusic2(AudioClip clip)
    {
        MusicPhaseSource2.clip = clip;
        MusicPhaseSource2.volume = 1f;
        MusicPhaseSource2.Play();        
    }

    public void StopSceneMusic2(AudioClip clip)
    {
        MusicPhaseSource2.clip = clip;
        MusicPhaseSource2.Stop();
    }

    public void PlaySceneMusic3(AudioClip clip)
    {
        MusicPhaseSource3.clip = clip;
        MusicPhaseSource3.volume = 1f;
        MusicPhaseSource3.Play();
    }

    public void StopSceneMusic3(AudioClip clip)
    {        
        MusicPhaseSource3.clip = clip;
        MusicPhaseSource3.Stop();
    }

}
