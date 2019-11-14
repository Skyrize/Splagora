using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOverTimeComponent : MonoBehaviour
{
    public float timeToFade = 1;
    public float fadeTo = 0.4f;

    private bool activated = false;

    public void Fade()
    {
        activated = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated) {
            if (timeToFade < 0) {
                GetComponent<Light>().intensity = Mathf.Lerp(fadeTo, 1, timeToFade);
                timeToFade -= Time.deltaTime;
            } else {
                activated = false;
            }
        }
    }
}
