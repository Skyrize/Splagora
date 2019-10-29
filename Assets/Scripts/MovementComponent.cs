using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [Header("Attributes")]
    public float speed = 1;
    public float resistance = 1;
    public float gravityScale = 0.5f;
    public float airControl = 2;

    private InputComponent input;
    private CharacterController controller;
    private Vector3 motion = Vector3.zero;
    private Vector3 externalForce = Vector3.zero;
    private Vector3 gravity = Vector3.zero;

    private Animator anim;

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

        anim = GetComponentInChildren<Animator>();

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
        if (controller.isGrounded) {
            controller.Move((motion + gravity + externalForce) * Time.fixedDeltaTime);
        } else {
            //externalForce += motion * Time.fixedDeltaTime * airControl;
            //externalForce.x = Mathf.Lerp(externalForce.x, 0, motion.magnitude);
            //(Vector3.Lerp(externalForce, motion, motion.magnitude)
            controller.Move((externalForce + gravity + motion*airControl) * Time.fixedDeltaTime);
        }
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
    public void AnimationPlayer()
    {
        //Debug.Log(controller.velocity.magnitude);
        if (controller.isGrounded)
        {
            anim.SetFloat("Velocity", controller.velocity.magnitude);
            anim.SetBool("IsJump", false);
        }
        else
        {
            anim.SetFloat("Velocity", 0);
            anim.SetBool("IsJump", true);
        }
    }

    private void FixedUpdate() {
        Turn();
        ApplyGravity();
        Move();
        ReduceExternalForces();
        AnimationPlayer();
    }

}
