using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [Header("Attributes")]
    public float speed = 1;
    public float resistance = 1;
    public float gravityScale = 0.5f;

    private InputComponent input;
    private CharacterController controller;
    private Vector3 motion = Vector3.zero;
    private Vector3 externalForce = Vector3.zero;
    private float gravity = 0;

    public void Propulse(Vector3 force)
    {
        externalForce = force;
    }

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<InputComponent>();
        if (!input)
            throw new MissingComponentException("No InputComponent on " + name);
        controller = GetComponent<CharacterController>();
        if (!controller)
            throw new MissingComponentException("No CharacterController on " + name);
    }

    private void ApplyGravity()
    {
        gravity += Physics.gravity.y * gravityScale;
        if (controller.isGrounded)
            gravity = Physics.gravity.y * gravityScale;
        controller.Move(Vector3.up * gravity* Time.fixedDeltaTime);
    }

    private void Turn()
    {
        transform.LookAt(transform.position + input.direction);
    }

    private void Move()
    {
        motion = input.direction * speed;
        controller.Move(motion * Time.fixedDeltaTime);
    }

    private void ApplyExternalForces()
    {
        if (externalForce.magnitude > 0.1f) {
            if (controller.isGrounded) {
                Debug.Log("grounded and force = 0");
                externalForce = Vector3.zero;
            }
            controller.Move(externalForce * Time.fixedDeltaTime);
            externalForce = Vector3.Lerp(externalForce, Vector3.zero, resistance * Time.fixedDeltaTime);
        } else {
            externalForce = Vector3.zero;
        }
    }

    private void FixedUpdate() {
        ApplyGravity();
        ApplyExternalForces();
        Move();
        Turn();
    }

}
