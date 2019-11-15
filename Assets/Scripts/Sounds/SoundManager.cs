using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
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

        musicData = SaveSystem.LoadData();

    }
    private void Start() {
        ModifyVolumePhases();
        ModifyVolumeEffects();
        Debug.Log("data = " + musicData.Musics + " " + musicData.Effects);
    }

    public void PowerUpSound(AudioClip clip)
    {
        SoundPowerUp.clip = clip;
        SoundPowerUp.Play();
    }

    public void ModifyVolumePhases()
    {
        MusicPhaseSource1.volume = musicData.Musics;
        
        Debug.Log("data = " + musicData.Musics + " " + musicData.Effects);
        MusicPhaseSource2.volume = musicData.Musics;
        
        Debug.Log("data = " + musicData.Musics + " " + musicData.Effects);
        MusicPhaseSource3.volume = musicData.Musics;
        
        Debug.Log("data = " + musicData.Musics + " " + musicData.Effects);
    }

    public void ModifyVolumeEffects()
    {
        SoundPowerUp.volume = musicData.Effects;
        
        Debug.Log("data = " + musicData.Musics + " " + musicData.Effects);
        WaveSound.volume = musicData.Effects;
        
        Debug.Log("data = " + musicData.Musics + " " + musicData.Effects);
        BumperSound.volume = musicData.Effects;
        
        Debug.Log("data = " + musicData.Musics + " " + musicData.Effects);
        timerGong.volume = musicData.Effects;
        
        Debug.Log("data = " + musicData.Musics + " " + musicData.Effects);
        playerSoundCollision.volume = musicData.Effects;
        
        Debug.Log("data = " + musicData.Musics + " " + musicData.Effects);
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
        MusicPhaseSource1.volume = musicData.Musics / gongReduce;
        
        Debug.Log("data = " + musicData.Musics + " " + musicData.Effects);
        MusicPhaseSource2.volume = musicData.Musics / gongReduce;
        
        Debug.Log("data = " + musicData.Musics + " " + musicData.Effects);
        MusicPhaseSource3.volume = musicData.Musics / gongReduce;
        
        Debug.Log("data = " + musicData.Musics + " " + musicData.Effects);
    }

    public void SoundPlayersCollision(AudioClip clip)
    {
        playerSoundCollision.clip = clip;
        playerSoundCollision.Play();
    }

    public void PlaySceneMusic1(AudioClip clip)
    {
        MusicPhaseSource1.clip = clip;
        MusicPhaseSource2.volume = musicData.Musics;
        
        Debug.Log("data = " + musicData.Musics + " " + musicData.Effects);
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
        MusicPhaseSource2.volume = musicData.Musics;
        
        Debug.Log("data = " + musicData.Musics + " " + musicData.Effects);
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
        MusicPhaseSource3.volume = musicData.Musics;
        
        Debug.Log("data = " + musicData.Musics + " " + musicData.Effects);
        MusicPhaseSource3.Play();
    }

    public void StopSceneMusic3(AudioClip clip)
    {        
        MusicPhaseSource3.clip = clip;
        MusicPhaseSource3.Stop();
    }   
}
