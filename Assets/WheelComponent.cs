using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelComponent : MonoBehaviour
{
    public float speed = 3;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * speed));
    }
}
