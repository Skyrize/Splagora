using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpComponent : MonoBehaviour
{
    public float speed = 10;
    public float duration = 2;
    private bool taken = false;
    private float oldSpeed = 0;
    private GameObject player;

    public AudioClip SoundPickUp;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") == true) {
            SoundManager.Instance.PowerUpSound(SoundPickUp);
            taken = true;
            player = other.gameObject;
            oldSpeed = player.GetComponent<MovementComponent>().speed;
            player.GetComponent<MovementComponent>().speed = speed;
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (taken) {
            if (duration > 0) {
                duration -= Time.deltaTime;
            } else {
                player.GetComponent<MovementComponent>().speed = oldSpeed;
                Destroy(this.gameObject);
            }
        }
    }
}
