using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpComponent : MonoBehaviour
{
    [Header("Attributes")]
    public float ejectionForce = 10;

    // private void OnTriggerEnter(Collider other) {
        
    // }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag.CompareTo("Player") == 0) {
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * ejectionForce);
        }
    }
}
