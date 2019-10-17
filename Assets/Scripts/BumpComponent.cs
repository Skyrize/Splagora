using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpComponent : MonoBehaviour
{
    [Header("Attributes")]
    public float ejectionForce = 1;

	public ParticleSystem bumpParticles;

    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.CompareTo("Player") == 0) {
            Debug.Log("Boing");
            other.gameObject.GetComponent<MovementComponent>().AddForce(transform.up * ejectionForce);
			bumpParticles.Play();
        }
        Debug.Log(other.gameObject.name);
    }
}
