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
    public SOTriggerAnim AnimSetting;

    public AudioClip SoundWave;

    private void Start() {
        col = GetComponent<SphereCollider>();
        col.radius = startingRadius;
        remaining = duration;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (remaining > 0) {
            remaining -= Time.deltaTime;
            col.radius = Mathf.Lerp(endingRadius, startingRadius, remaining/duration);
        } else {
            Destroy(this.gameObject);
            // remaining = duration;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.CompareTo("Bloc") == 0)
        {
            other.GetComponent<BlockWallAnimation>().AnimationBlock(AnimSetting);
            SoundManager.Instance.WaveSoundEffect(SoundWave);
        }
    }
}
