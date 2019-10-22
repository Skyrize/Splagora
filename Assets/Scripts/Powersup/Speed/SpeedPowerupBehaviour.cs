using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerupBehaviour : MonoBehaviour
{
    public SpeedPowerupController controller;

    [SerializeField]
    private SpeedPowerup powerup;

    private Renderer renderer_;

    private Transform transform_;

    public Material PowerupMaterial
    {
        get { return renderer_.material; }
        set { renderer_.material = value; }
    }

    private int rotationPerSecond = 180;

    private void Awake()
    {
        transform_ = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ActivatePowerup();
            gameObject.SetActive(false);
        }
    }


    private void ActivatePowerup()
    {
        controller.ActivatePowerup(powerup);
    }

    public void SetPowerup(SpeedPowerup powerup)
    {
        this.powerup = powerup;
        gameObject.name = powerup.name;
    }
}
