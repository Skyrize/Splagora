using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerupActions : MonoBehaviour
{
    private MovementComponent movementComponent;

    public void SetMovmentComponent(MovementComponent movementComponent)
    {
        this.movementComponent = movementComponent;
    }

    public void HightSpeedStartAction()
    {
        Debug.Log("Start SpeedPowerUp");
        movementComponent.speed *= 2;
    }

    public void HightSpeedEndAction()
    {
        Debug.Log("End SpeedPowerUp "  + this.movementComponent.tag);
        movementComponent.speed = 8;
    }
}