using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [Header("Attributes")]
    public float speed = 1;
    public bool canMove = true;

    private PlayerController controller;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        if (!controller)
            throw new MissingComponentException("No PlayerController on " + name);
        rb = GetComponent<Rigidbody>();
        if (!rb)
            throw new MissingComponentException("No Rigidbody on " + name);
    }

    private void Move()
    {
        if (canMove) {
            transform.LookAt(transform.position + controller.direction);
            rb.velocity = controller.direction * speed + new Vector3(0, rb.velocity.y, 0);
        }
    }

    private void FixedUpdate() {
        Move();
    }

}
