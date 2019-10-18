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
    private Vector3 gravity = Vector3.zero;

    public float velocity {
        get {
            return Mathf.Abs(motion.x + externalForce.x);
        }
    }

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
        gravity += Physics.gravity * gravityScale;
        if (controller.isGrounded)
            gravity = Physics.gravity * gravityScale;
    }

    private void Turn()
    {
        transform.LookAt(transform.position + input.direction);
    }

    private void Move()
    {
        motion = input.direction * speed;
        controller.Move((motion + gravity + externalForce) * Time.fixedDeltaTime);
    }

    private void ReduceExternalForces()
    {
        if (externalForce.magnitude > 0.1f) {
            externalForce = Vector3.Lerp(externalForce, Vector3.zero, resistance * Time.fixedDeltaTime);
            if (controller.isGrounded) {
                externalForce = Vector3.zero;
            }
        } else {
            externalForce = Vector3.zero;
        }
    }

    private void FixedUpdate() {
        Turn();
        ApplyGravity();
        Move();
        ReduceExternalForces();
    }

}
