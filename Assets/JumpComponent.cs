using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpComponent : MonoBehaviour
{
    [Header("Attributes")]
    public float jumpForce = 7;
    // public LayerMask groundLayers;

    // [Space]
    // [Header("Events")]
    // public UnityEvent onJump = new UnityEvent();
    // public UnityEvent onLanding = new UnityEvent();

    private CharacterController controller;
    private MovementComponent movement;
    // private bool wasInAir = false;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (!controller)
            throw new MissingComponentException("No CharacterController on " + name);
        movement = GetComponent<MovementComponent>();
        if (!movement)
            throw new MissingComponentException("No MovementComponent on " + name);
    }

    public void Jump(Vector3 direction)
    {
            Debug.Log("Can't jump .. because " + controller.isGrounded);
        if (controller.isGrounded) {
            Debug.Log("JUUUUMP");
            movement.Propulse(direction * jumpForce);
        }
    }

    public void QuickJump()
    {
        Jump(transform.up);
    }

    public void LongJump()
    {
        Jump(transform.up + transform.forward * 0.5f);
    }
}
