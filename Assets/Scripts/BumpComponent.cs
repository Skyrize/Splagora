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
    private Vector3 startScale;
    public float timeAnim;
    public void Start()
    {
        if (render != null)
        {
            startScale = render.transform.localScale;

        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag.CompareTo("Player") == 0) {
            //Debug.Log("Boing");
            other.gameObject.GetComponent<MovementComponent>().Propulse(transform.up * ejectionForce);
            if(particlebump!=null)
            particlebump.Play();
            if (render != null)
            {
                render.transform.DOKill();
                render.transform.localScale = startScale;
                
                render.transform.DOPunchScale(-AnimBumper,timeAnim, 2,1).OnComplete(EndBumpAnim);
            }
        }
    }
    private void EndBumpAnim()
    {
        render.transform.localScale = new Vector3(1, 1, 1);
    }
}
