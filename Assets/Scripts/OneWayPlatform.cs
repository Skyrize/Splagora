using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    public BoxCollider platformCollider;

            // Ce script sert à faire des plateformes qu'on peut traverser d'en bas.
            /*
             *  Quand le joueur entre dans le trigger sous la plateforme, la plateforme ignore la collision avec ce personnage. 
             *  Quand il en ressort (et qu'il est en général au dessus de la plateforme) on réactive la collision avec ce personnage pour qu'il se tienne dessus.
             */


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(platformCollider, other.GetComponent<CharacterController>());
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(platformCollider, other.GetComponent<CharacterController>(), false);
        }
    }
}
