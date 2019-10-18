using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatformComponent : MonoBehaviour
{
    private Collider parentCollider;

    private void Start() {
        parentCollider = transform.parent.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player")) {
            Physics.IgnoreCollision(parentCollider, other.GetComponent<CharacterController>());
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player")) {
            Physics.IgnoreCollision(parentCollider, other.GetComponent<CharacterController>(), false);
        }
    }
}
