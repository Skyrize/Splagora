using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerComponent : MonoBehaviour
{
    public float ForcePropulse;
    public UnityEvent collidePlayerEvent = new UnityEvent();
    private MovementComponent movement;
    private CharacterController controller;
    private GameObject previousPlatform = null;
    private GameObject platform = null;
    private Animator Anim;
    private Animator AnimP2;
    private Vector3 tramSpeed = Vector3.zero;

    public AudioClip PlayersSoundOnCollision;

    private void Start()
    {
        movement = GetComponent<MovementComponent>();
        if (!movement)
            throw new MissingComponentException("No MovementComponent on " + name);
        controller = GetComponent<CharacterController>();
        if (!controller)
            throw new MissingComponentException("No CharacterController on " + name);

        Anim = GetComponentInChildren<Animator>();
    }

    private void HandlePlatformCollision(ControllerColliderHit hit)
    {
        if (!platform) {
            platform = hit.gameObject;
        } else if (platform != hit.gameObject) {
            Physics.IgnoreCollision(platform.GetComponent<Collider>(), controller, false);
            platform = hit.gameObject;
        }
    }

    private void HandlePlayerCollision(ControllerColliderHit hit)
    {

        //Debug.Log("JE TE POUSSE");
        if (AnimP2 == null)
        {
            AnimP2 = hit.gameObject.GetComponentInChildren<Animator>();
        }
        AnimP2.SetBool("Bump", true);
        Anim.SetBool("Punch", true);
        SoundManager.Instance.SoundPlayersCollision(PlayersSoundOnCollision);
            StartCoroutine(ResetAnim());
            Vector3 dir =  transform.position- hit.gameObject.transform.position ;
            hit.gameObject.GetComponent<MovementComponent>().Propulse((-dir+Vector3.up) * ForcePropulse);
            GetComponent<MovementComponent>().Propulse((dir + Vector3.up) * ForcePropulse);
            collidePlayerEvent.Invoke();


        
    }

    private void HandleOtherCollision(ControllerColliderHit hit)
    {
        if (platform) {
            Physics.IgnoreCollision(platform.GetComponent<Collider>(), controller, false);
            platform = null;
        }
    }

    private void HandleTramCollision(Collider other)
    {
        //movement.AddMotion(hit.transform.parent.GetComponent<TramObstacle>().velocity);
        //Debug.Log("hit tram");
        //Debug.Log(hit.transform.parent.GetComponent<TramObstacle>().velocity);
        controller.Move(other.transform.parent.GetComponent<TramObstacle>().velocity * Time.deltaTime);
        //movement.Propulse(new Vector3(hit.transform.parent.GetComponent<TramObstacle>().speed * hit.transform.parent.GetComponent<TramObstacle>().directionX, 12f));
        //transform.parent = hit.transform;
        //Debug.Log(hit.transform.name);
        //this.transform.position = new Vector3(hit.transform.parent.transform.position.x, transform.position.y, transform.position.z);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Tram"))
            HandleTramCollision(other);

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag.Equals("Platform"))
            HandlePlatformCollision(hit);
        else if (hit.gameObject.tag.Equals("Player"))
            HandlePlayerCollision(hit);
        else
            HandleOtherCollision(hit);
    }

    public void GetDown()
    {
        if (platform) {
            Physics.IgnoreCollision(platform.GetComponent<Collider>(), GetComponent<CharacterController>());
            previousPlatform = platform;
        }
    }

    public IEnumerator ResetAnim()
    {
        yield return new WaitForSeconds(0.1f);
        AnimP2.SetBool("Bump", false);
        Anim.SetBool("Punch", false);
    }

}
