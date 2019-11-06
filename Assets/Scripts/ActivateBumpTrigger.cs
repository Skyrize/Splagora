using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBumpTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("BumpTrigger")) {
            other.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
