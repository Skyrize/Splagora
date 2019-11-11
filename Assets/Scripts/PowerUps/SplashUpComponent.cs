using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;
using Es.InkPainter.Sample;

public class SplashUpComponent : MonoBehaviour
{
    public float scale = 0.4f;
    public float duration = 2;
    private bool taken = false;
    private float oldScale = 0;
    private MousePainter painter;
    private Brush brush;

    private void Start()
    {
        painter = GameObject.FindGameObjectWithTag("Painter").GetComponent<MousePainter>();
    }
    public AudioClip SoundPickUp;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") == true) {
            SoundManager.Instance.PowerUpSound(SoundPickUp);
            taken = true;
            brush = painter.GetPlayerBrush(other.gameObject.name);
            oldScale = brush.Scale;
            brush.Scale = scale;
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (taken) {
            if (duration > 0) {
                duration -= Time.deltaTime;
            } else {
                brush.Scale = oldScale;
                Destroy(this.gameObject);
            }
        }
    }
}
