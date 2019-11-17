using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer Mixer;
    public AudioSource[] EffectsSounds;
    public AudioSource[] Musics;

    public AudioSource SoundPowerUp;
    public AudioSource WaveSound;
    public AudioSource BumperSound;
    public AudioSource timerGong;
    public AudioSource playerSoundCollision;
    public AudioSource MusicPhaseSource1;
    public AudioSource MusicPhaseSource2;
    public AudioSource MusicPhaseSource3;
    //public AudioSource MusicMenu;
    //public AudioSource UIButtonSound;

    SettingsData musicData;   

    public static SoundManager Instance = null;

    public float gongReduce = 1.2f;

    //public Transform[] spawnersTram;   

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
    private void Start() {
      
    }

    public void PowerUpSound(AudioClip clip)
    {
        SoundPowerUp.clip = clip;
        SoundPowerUp.Play();
    }

    public void ModifyVolumeGeneral(float value)
    {
        Mixer.SetFloat("Master", value);
    }

    public void ModifyVolumeEffects()
    {
        SoundPowerUp.volume = musicData.Effects;
        WaveSound.volume = musicData.Effects;
        BumperSound.volume = musicData.Effects;
        timerGong.volume = musicData.Effects;
        playerSoundCollision.volume = musicData.Effects;
    }
    /*
    public void TramSoundComing(AudioClip clip, int SideTram)
    {
        TramSound.clip = clip;
        AudioSource.PlayClipAtPoint(clip, spawnersTram[SideTram].position);
        Debug.Log("Position Sound" + spawnersTram[SideTram].position);
    }*/

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

        
    }

    public void SoundPlayersCollision(AudioClip clip)
    {
        playerSoundCollision.clip = clip;
        playerSoundCollision.Play();
    }

    public void PlaySceneMusic1(AudioClip clip)
    {
        MusicPhaseSource1.clip = clip;
        
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
        
        MusicPhaseSource3.Play();
    }

    public void StopSceneMusic3(AudioClip clip)
    {        
        MusicPhaseSource3.clip = clip;
        MusicPhaseSource3.Stop();
    }   
}
