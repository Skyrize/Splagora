using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugLive : MonoBehaviour
{
    [Header("TRAM")]
    public GameObject TramPrefab;
    public InputField SpeedTram;
    public Text CurrentTramSpeed;

    [Header("TRAM SPAWNER")]
    public GameObject TramSpawner;
    public InputField CicleTram;
    public Text CurrentSpeedSpawn;


    [Header("Police Car")]

    public GameObject CarPrefab;
    public InputField SpeedCar;
    public Text CurrentCarSpeed;


    [Header("Manche")]

    public GameObject GameManager;
    public InputField TimeManche;
    public Text CurrentManchepeed;

    [Header("Player")]

    public GameObject Player1,Player2;
    public InputField SpeedPlayer;
    public Text CurrentSpeedPlayer;


    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("TRAMSPEED")!=0)
        TramPrefab.GetComponent<TramObstacle>().time = PlayerPrefs.GetInt("TRAMSPEED");

        if (PlayerPrefs.GetInt("SPAWNSPEED") != 0) {
            TramSpawner.GetComponent<SpawnerObstacle>().MaxIntervaleSpawn = PlayerPrefs.GetInt("SPAWNSPEED")+2;
            TramSpawner.GetComponent<SpawnerObstacle>().MinIntervaleSpawn = PlayerPrefs.GetInt("SPAWNSPEED");
        }


        if (PlayerPrefs.GetInt("CARSPEED") != 0)
            CarPrefab.GetComponent<TramObstacle>().time = PlayerPrefs.GetInt("CARSPEED");

        if (PlayerPrefs.GetInt("MANCHE") != 0)
            GameManager.GetComponent<GameManager>().Chrono = PlayerPrefs.GetInt("MANCHE");

        if (PlayerPrefs.GetInt("SPEEDPLAYER") != 0)
        {
            Player1.GetComponent<MovementComponent>().speed = PlayerPrefs.GetInt("SPEEDPLAYER");
            Player2.GetComponent<MovementComponent>().speed = PlayerPrefs.GetInt("SPEEDPLAYER");
        }

        CurrentTramSpeed.text = "Vitesse Tram= "+TramPrefab.GetComponent<TramObstacle>().time.ToString();
        CurrentCarSpeed.text = "Vitesse Voiture= " + CarPrefab.GetComponent<TramObstacle>().time.ToString();
        CurrentManchepeed.text = "Temps Manche= " + GameManager.GetComponent<GameManager>().Chrono.ToString();
        CurrentSpeedPlayer.text = "Vitesse Player= " + Player1.GetComponent<MovementComponent>().speed.ToString();
        CurrentSpeedSpawn.text = "Apparition Intervale entre " + TramSpawner.GetComponent<SpawnerObstacle>().MinIntervaleSpawn + "et" + TramSpawner.GetComponent<SpawnerObstacle>().MaxIntervaleSpawn;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonTram()
    {
        Debug.Log(int.Parse(SpeedTram.text));
        TramPrefab.GetComponent<TramObstacle>().time =int.Parse(SpeedTram.text);
        PlayerPrefs.SetInt("TRAMSPEED", int.Parse(SpeedTram.text));
        CurrentTramSpeed.text = "Vitesse Tram= " + int.Parse(SpeedTram.text).ToString();
        SpeedTram.text = "";
    }
    public void ButtonCar()
    {
        CarPrefab.GetComponent<TramObstacle>().time = int.Parse(SpeedCar.text);
        PlayerPrefs.SetInt("CARSPEED", int.Parse(SpeedCar.text));
        CurrentCarSpeed.text = "Vitesse Voiture= " + int.Parse(SpeedCar.text).ToString();
        SpeedCar.text = "";

    }
    public void ButtonTimeManche()
    {
        GameManager.GetComponent<GameManager>().Chrono = int.Parse(TimeManche.text);
        PlayerPrefs.SetInt("MANCHE", int.Parse(TimeManche.text));
        CurrentManchepeed.text = "Temps Manche= " + int.Parse(TimeManche.text).ToString();

        TimeManche.text = "";

    }
    public void ButtonSpeedPlayer()
    {
        Player1.GetComponent<MovementComponent>().speed = int.Parse(SpeedPlayer.text);
        Player2.GetComponent<MovementComponent>().speed = int.Parse(SpeedPlayer.text);
        PlayerPrefs.SetInt("SPEEDPLAYER", int.Parse(SpeedPlayer.text));
        CurrentSpeedPlayer.text = "Vitesse Player= " + int.Parse(SpeedPlayer.text).ToString();

        SpeedPlayer.text = "";

    }
    public void ButtonIntervaleSpawn()
    {
        TramSpawner.GetComponent<SpawnerObstacle>().MinIntervaleSpawn = int.Parse(CicleTram.text);
        TramSpawner.GetComponent<SpawnerObstacle>().MaxIntervaleSpawn = int.Parse(CicleTram.text)+2;
        PlayerPrefs.SetInt("SPAWNSPEED", int.Parse(CicleTram.text));
        CurrentSpeedSpawn.text = "Apparition Intervale entre " + TramSpawner.GetComponent<SpawnerObstacle>().MinIntervaleSpawn + "et" + TramSpawner.GetComponent<SpawnerObstacle>().MaxIntervaleSpawn;

        CicleTram.text = "";

    }
}
