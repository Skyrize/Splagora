﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnerComponent : MonoBehaviour
{
    public float minimumTime = 2;
    public float maximumeTime = 3;
    public GameObject[] powerUps;
    public SpawnPointComponent[] points;
    [SerializeField]
    private float timer = 0;

    /*
        Ce code il est pas beau faut pas le regarder dans les yeux franchement c'est pas bien fait j'avais la flemme hihi
    */
    [SerializeField]
    public GameObject theOnlyOne;

    private void Awake()
    {
        points = new SpawnPointComponent[transform.childCount];
        for (int i = 0; i != transform.childCount; i++)
            points[i] = transform.GetChild(i).GetComponent<SpawnPointComponent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(minimumTime, maximumeTime);
    }

    SpawnPointComponent GetRandomPoint()
    {
        SpawnPointComponent point = points[Random.Range(0, points.Length)];

        while (point.isDenied == true || point.isHolding == true)
            point = points[Random.Range(0, points.Length)];
        return point;
    }

    bool CanSpawn()
    {
        foreach (SpawnPointComponent point in points)
            if (point.isHolding == false && point.isDenied == false)
                return true;
        return false;
    }

    void SpawnRandomPowerUp()
    {
        int index = Random.Range(0, powerUps.Length);
        //int index = 1;
        SpawnPointComponent point = GetRandomPoint();
        theOnlyOne = Instantiate(powerUps[index], point.transform.position, point.transform.rotation);
        point.isHolding = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!theOnlyOne) {
            if (timer > 0) {
                timer -= Time.deltaTime;
            } else {
                timer = Random.Range(minimumTime, maximumeTime);
                if (CanSpawn())
                    SpawnRandomPowerUp();
            }
        }
    }
}