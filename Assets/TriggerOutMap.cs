using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOutMap : MonoBehaviour
{
    public GameObject P1, P2;
    public Transform targetP1, targetP2;
    public bool Enter;

    public void OnTriggerExit(Collider other)
    {
        if (!Enter)
        {
            if (other.gameObject == P1)
            {
                P1.GetComponent<CharacterController>().enabled = false;
                P1.transform.position = targetP1.position;
                P1.GetComponent<CharacterController>().enabled = true;

            }
            if (other.gameObject == P2)
            {
                P2.GetComponent<CharacterController>().enabled = false;
                P2.transform.position = targetP2.position;
                P2.GetComponent<CharacterController>().enabled = true;


            }
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (Enter)
        {
            if (other.gameObject == P1)
            {
                P1.GetComponent<CharacterController>().enabled = false;
                P1.transform.position = targetP1.position;
                P1.GetComponent<CharacterController>().enabled = true;

            }
            if (other.gameObject == P2)
            {
                P2.GetComponent<CharacterController>().enabled = false;
                P2.transform.position = targetP2.position;
                P2.GetComponent<CharacterController>().enabled = true;


            }
        }

    }
}
