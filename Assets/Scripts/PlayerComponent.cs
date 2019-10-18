using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    private MovementComponent movement;
    private CharacterController controller;
    private GameObject previousPlatform = null;
    private GameObject platform = null;

    private void Start()
    {
        movement = GetComponent<MovementComponent>();
        if (!movement)
            throw new MissingComponentException("No MovementComponent on " + name);
        controller = GetComponent<CharacterController>();
        if (!controller)
            throw new MissingComponentException("No CharacterController on " + name);
    }

    private void HandlePlatformCollision(ControllerColliderHit hit)
    {
        if (!platform) {
            platform = hit.gameObject;
        } else if (platform != hit.gameObject) {
            Physics.IgnoreCollision(platform.GetComponent<Collider>(), controller, false);
            platform = hit.gameObject;
        }
    }

    private void HandlePlayerCollision(ControllerColliderHit hit)
    {
        Debug.Log(name + " has velocity = " + movement.velocity);
        if (hit.gameObject.GetComponent<MovementComponent>().velocity > movement.velocity) {

        } else if (hit.gameObject.GetComponent<MovementComponent>().velocity < movement.velocity) {

        } else {

        }
    }

    private void HandleOtherCollision(ControllerColliderHit hit)
    {
        if (platform) {
            Physics.IgnoreCollision(platform.GetComponent<Collider>(), controller, false);
            platform = null;
        }
    }



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag.Equals("Platform"))
            HandlePlatformCollision(hit);
        else if (hit.gameObject.tag.Equals("Player"))
            HandlePlayerCollision(hit);
        else
            HandleOtherCollision(hit);
    }

    public void GetDown()
    {
        if (platform) {
            Physics.IgnoreCollision(platform.GetComponent<Collider>(), GetComponent<CharacterController>());
            previousPlatform = platform;
        }
    }

}
