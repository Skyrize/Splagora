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

    [Header("Particules")]
    public GameObject StopParticule;

    public float velocity {
        get {
            return Mathf.Abs(motion.x + externalForce.x);
        }
    }

    public void Propulse(Vector3 force)
    {
        //Debug.Log("Propulse");
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
        Debug.Log(input.direction + "Direction");
        if (input.direction.x > 0)
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
        if (input.direction.x < 0)
        {
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
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
            //anim.SetFloat("Velocity", 0);
            anim.SetFloat("Velocity", controller.velocity.magnitude);

            anim.SetBool("IsJump", true);

            
        }
    }
    public void ParticuleSpawn()
    {
        if (controller.velocity.magnitude < 0.5f )
        {
            if(StopParticule!=null)
            Instantiate(StopParticule, transform.position, transform.rotation);
        }
    }

    private void FixedUpdate() {
        Turn();
        ApplyGravity();
        Move();
        ReduceExternalForces();
        AnimationPlayer();
        ParticuleSpawn();
        //Freeze z axis
        transform.position = new Vector3(transform.position.x, transform.position.y, 1.5f);
    }



}
