using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    private MovementComponent movement;

    public void Jump()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<MovementComponent>();
        if (!movement)
            throw new MissingComponentException("No MovementComponent on " + name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
