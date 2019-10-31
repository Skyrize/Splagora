using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerupSpawn : MonoBehaviour
{
    public float TimeShowFeedBack;

    float timePast = 0;
    float nextSpawneTime = 3;
    bool isSpawning;
    public float MinIntervaleSpawn, MaxIntervaleSpawn;
    int indexSide;
    Transform currentSpawn;
    public SpeedPowerupController controller;
    void Start()
    {
        StartSpawning();
    }

    // Update is called once per frame
    void Update()
    {
        timePast += Time.deltaTime;
        if (timePast > nextSpawneTime && !isSpawning)
        {
            isSpawning = true;
            controller.SpawnPowerup();
            isSpawning = false;
            timePast = 0;
            StartSpawning();
        }

    }

    void StartSpawning()
    {
        nextSpawneTime = Random.Range(MinIntervaleSpawn, MaxIntervaleSpawn);


    }
}
