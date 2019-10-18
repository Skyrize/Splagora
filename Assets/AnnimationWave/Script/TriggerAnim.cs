using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnim : MonoBehaviour
{
    public int NombreAnim;
    public Animator AnimTransition;
    // Start is called before the first frame update
    public void Awake()
    {
        AnimTransition = GetComponent<Animator>();
    }
    public void Start()
    {
        Transition();
    }
    public void Transition()
    {
        if (AnimTransition.GetInteger("indexAnim") == 0 && AnimTransition.GetCurrentAnimatorClipInfo(0).ToString() != "idleTransition")
        {
            int indexAnim = Random.Range(1, NombreAnim + 1);
            AnimTransition.SetInteger("indexAnim", indexAnim);
            StartCoroutine(ResetAnim());
        }

    }

    public IEnumerator ResetAnim()
    {
        yield return new WaitForSeconds(0.2f);
        AnimTransition.SetInteger("indexAnim", 0);
    }
}
