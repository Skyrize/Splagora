using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecessingPlatformComponent : MonoBehaviour
{
    public float offsetZ = 1;
    public float speedZ = 1f;
    private Vector3 initialPos;
    private Vector3 destination;

    private bool recess = false;
    private bool move = false;

    public void Recess()
    {
        move = true;
        recess = true;
    }

    public void Unrecess()
    {
        move = true;
        recess = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        destination = initialPos + Vector3.forward * offsetZ;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BumpTriggerEnd") {
            Debug.Log("UNRECESS ON TRIGGER");
            Unrecess();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (move) {
            if (recess) {
                transform.position = Vector3.MoveTowards(transform.position, destination, speedZ * Time.deltaTime);
                if (Vector3.Distance(destination, transform.position) < 0.001) {
                    move = false;
                }
            } else {
                transform.position = Vector3.MoveTowards(transform.position, initialPos, speedZ * Time.deltaTime);
                if (Vector3.Distance(initialPos, transform.position) < 0.001) {
                    move = false;
                }

            }
        }
    }
}
