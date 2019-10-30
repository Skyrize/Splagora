﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    public float ForcePropulse;
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
        //Debug.Log(name + " has velocity = " + movement.velocity);
        if (hit.gameObject.GetComponent<MovementComponent>().velocity > movement.velocity) {
            //Debug.Log("TU ME POUSSE");

            //Vector3 dir = transform.position - hit.transform.position;
            //hit.gameObject.GetComponent<MovementComponent>().Propulse(dir * ForcePropulse);

        } else if (hit.gameObject.GetComponent<MovementComponent>().velocity < movement.velocity) {
            //Debug.Log("JE TE POUSSE");

            Vector3 dir =  transform.position- hit.gameObject.transform.position ;
            hit.gameObject.GetComponent<MovementComponent>().Propulse((-dir+Vector3.up) * ForcePropulse);
            GetComponent<MovementComponent>().Propulse((-dir + Vector3.up) * ForcePropulse/3);


        }
        else {

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
