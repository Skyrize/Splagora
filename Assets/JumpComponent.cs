using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpComponent : MonoBehaviour
{
    [Header("Attributes")]
    public float jumpForce = 7;
    public LayerMask groundLayers;

    [Space]
    [Header("Events")]
    public UnityEvent onJump = new UnityEvent();
    public UnityEvent onLanding = new UnityEvent();

    public bool isGrounded
    {
        get {
            return Physics.CheckCapsule(col.bounds.center,
            new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z),
            col.radius * 0.9f, groundLayers);
        }
    }


    private CapsuleCollider col;
    private MovementComponent movement;
    private bool wasInAir = false;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CapsuleCollider>();
        if (!col)
            throw new MissingComponentException("No CapsuleCollider on " + name);
        movement = GetComponent<MovementComponent>();
        if (!movement)
            throw new MissingComponentException("No MovementComponent on " + name);
    }

    private void Update()
    {
        bool tmp = isGrounded;
        if (wasInAir == true && tmp == true) {
            Debug.Log("LAND");
            onLanding.Invoke();
        }
        wasInAir = !tmp;
    }

    public void Jump(Vector3 direction)
    {
        if (isGrounded) {
            movement.AddForce(direction * jumpForce);
            onJump.Invoke();
        }
    }

    public void QuickJump()
    {
        Jump(Vector3.up);
    }

    public void LongJump()
    {
        Jump(transform.up + transform.forward * 0.5f);
    }
}
