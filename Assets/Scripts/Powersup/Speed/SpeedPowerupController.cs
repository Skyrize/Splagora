using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpeedPowerupController : MonoBehaviour
{
    public GameObject powerupPrefab;
    public SpeedPowerupController Origin;
    public List<SpeedPowerup> powerups;
    public int duration;
    public Dictionary<SpeedPowerup, float> activePowerups = new Dictionary<SpeedPowerup, float>();
    private List<SpeedPowerup> keys = new List<SpeedPowerup>();


    // Update is called once per frame
    void Update()
    {
        HandeActivePowerups();
        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnPowerup() ;
        }
    }

    public void HandeActivePowerups()
    {
        bool changed = false;
        if (activePowerups.Count > 0)
        {
            foreach (SpeedPowerup powerup in keys)
            {
                if (activePowerups[powerup] > 0)
                {
                    activePowerups[powerup] -= Time.deltaTime;
                }
                else
                {
                    changed = true;
                    activePowerups.Remove(powerup);
                    powerup.End();
                }
            }
        }
        if (changed)
        {
            keys = new List<SpeedPowerup>(activePowerups.Keys);
        }
    }

    public void ActivatePowerup(SpeedPowerup powerup)
    {
        if (!activePowerups.ContainsKey(powerup))
        {
            powerup.Start();
            activePowerups.Add(powerup, duration);
        }
        else
        {
            activePowerups[powerup] += duration;
        }
        keys = new List<SpeedPowerup>(activePowerups.Keys);
    }

    public void SpawnPowerup()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        Vector3 position = getRandomPosition();
        Instantiate(Origin, position, rotation);
    }

    private Vector3 getRandomPosition()
    {
        float minX = -4f;
        float maxX = 4.60f;
        float badMinX = -0.601f;
        float badMaxX = 1.32f;
        float minY = 0f;
        float maxY = 4f;
        float badMaxY = 3.26f;
        float badMinY = 2;
        Vector3 vector = new Vector3();
        vector.z = 1.75f;
        do
        {
            vector.x = Random.Range(minX, maxX);
        } while (vector.x >= badMinX && vector.x <= badMaxX);
        do
        {
            vector.y = Random.Range(minY, maxY);
        } while (vector.y >= badMinY && vector.y <= badMaxY);

        return vector;
    }
}
