using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointComponent : MonoBehaviour
{
    public bool isHolding = false;
    public bool isDenied = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isHolding = false;
            isDenied = true;
        }       
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            isHolding = false;
            isDenied = true;           
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            isDenied = false;           
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
