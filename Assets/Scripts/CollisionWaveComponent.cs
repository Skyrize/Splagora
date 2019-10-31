using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWaveComponent : MonoBehaviour
{
    public float startingRadius = 0.5f;
    public float endingRadius = 3;
    public float duration = 2;
    private SphereCollider col;
    private float remaining;

    private void Start() {
        col = GetComponent<SphereCollider>();
        col.radius = startingRadius;
        remaining = duration;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (remaining > 0) {
            col.radius = Mathf.Lerp(endingRadius, startingRadius, remaining/duration);
            remaining -= Time.deltaTime;
        } else {
            Destroy(this.gameObject);
            // remaining = duration;
        }
    }
}
