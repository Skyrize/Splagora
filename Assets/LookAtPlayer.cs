using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public GameObject TargetPlayer,Target;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Target.transform.position = Vector3.Lerp(Target.transform.position, TargetPlayer.transform.position, Time.deltaTime* speed);
        transform.LookAt(Target.transform.position, Vector3.up);
    }
}
