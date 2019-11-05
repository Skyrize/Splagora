using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BumpComponent : MonoBehaviour
{
    [Header("Attributes")]
    public float ejectionForce = 1;
    public ParticleSystem particlebump;
    [Header("Animation")]

    public GameObject render;
    public Vector3 AnimBumper;
    public float timeAnim;

    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.CompareTo("Player") == 0) {
            //Debug.Log("Boing");
            other.gameObject.GetComponent<MovementComponent>().Propulse(transform.up * ejectionForce);
            if(particlebump!=null)
            particlebump.Play();
            if (render != null)
            {
                
                render.transform.DOPunchScale(-AnimBumper,timeAnim, 2,1).OnComplete(EndBumpAnim);
            }
        }
    }
    private void EndBumpAnim()
    {
        render.transform.localScale = new Vector3(1, 1, 1);
    }
}
