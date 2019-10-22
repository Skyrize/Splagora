using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerupActions : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private MovementComponent movementComponent;

    public void HightSpeedStartAction()
    {
        movementComponent.speed *= 2;
    }

    public void HightSpeedEndAction()
    {
        movementComponent.speed = 8;//movementComponent.defaultSpeed;
    }
}