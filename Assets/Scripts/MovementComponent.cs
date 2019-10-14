using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [Header("Attributes")]
    public float speed = 1;
    public bool canMove = true;

    private InputComponent controller;
    private Rigidbody rb;
    private Vector3 vel = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<InputComponent>();
        if (!controller)
            throw new MissingComponentException("No InputComponent on " + name);
        rb = GetComponent<Rigidbody>();
        if (!rb)
            throw new MissingComponentException("No Rigidbody on " + name);
    }

    private void Move()
    {
        if (canMove) {
            transform.LookAt(transform.position + controller.direction);
            rb.MovePosition(transform.position + controller.direction * speed * Time.fixedDeltaTime);
        }
    }

    private void FixedUpdate() {
        Move();
    }

}
