using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatMoveComponent : MonoBehaviour
{
    public float rotationSpeed = 75;
    public float amplitudeY = 0.1f;
    public float speedY = 1f;
    
    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position;        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * rotationSpeed, 0));
        tempPos = posOffset;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * speedY) * amplitudeY;
 
        transform.position = tempPos;
    }
}
