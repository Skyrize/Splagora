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

    public void AddForce(Vector3 force)
    {
        externalForce += force;
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

    private void Move()
    {
        transform.LookAt(transform.position + input.direction);
        motion = input.direction * speed;
        gravity += Physics.gravity.y * Time.fixedDeltaTime * gravityScale;
        controller.Move(motion * Time.fixedDeltaTime + Vector3.up * gravity);
        if (controller.isGrounded)
            gravity = 0;
    }

    private void ApplyExternalForces()
    {
        if (externalForce.magnitude > 0.2f) {
            controller.Move((externalForce + Physics.gravity) * Time.fixedDeltaTime);
            externalForce = Vector3.Lerp(externalForce, Vector3.zero, resistance * Time.fixedDeltaTime);
        }
    }

    private void FixedUpdate() {
        Move();
        ApplyExternalForces();
    }

}
