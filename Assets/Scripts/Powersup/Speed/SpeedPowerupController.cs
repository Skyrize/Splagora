using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerupController : MonoBehaviour
{
    public GameObject powerupPrefab;
    public GameObject Origin;

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
            SpawnPowerup(powerups[0], new Vector3(-1.925f, 2.646f, 1.75f)) ;
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

    public GameObject SpawnPowerup(SpeedPowerup powerup, Vector3 position)
    {
        try
        {
            Quaternion rotation = Quaternion.Euler(0,0,0);
            GameObject powerupGameObject = Instantiate(Origin, position, rotation);
            return powerupGameObject;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
