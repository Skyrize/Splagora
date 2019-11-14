using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;
using Es.InkPainter.Sample;

public class SplashBombComponent : MonoBehaviour
{
    public float SplashSize = 2;
    [SerializeField]
    private MousePainter painter;
    private Brush brush;
    private RaycastHit hitInfo;
    private InkCanvas ink;
    bool isP1;
    bool isTriggered = false;
    bool hasExploded = false;
    bool willDestroy= false;
    private GameObject particle;
    bool picked=false;

    private void Start()
    {
        painter = GameObject.FindGameObjectWithTag("Painter").GetComponent<MousePainter>();
        particle = transform.GetChild(0).GetChild(0).gameObject;
    }

    private void Update()
    {
        if (isTriggered) {
            if (hasExploded == false && isP1 == painter.turnP1 && particle.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().time == 3) {
                //le son pour bombe 
                ink.Paint(brush, hitInfo);
                GetComponent<MeshRenderer>().enabled = false;
                particle.transform.parent.GetComponent<ParticleSystem>().Stop();
                hasExploded = true;
                
            }
            if (hasExploded && !willDestroy)
            {
                willDestroy = true;
                Debug.Log("DETRUIRE BOMBE");
                Destroy(gameObject,1f);
            }
        }
    }
    public AudioClip SoundPickUp;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") == true && picked==false) {
            if (Physics.Raycast(transform.position, Vector3.forward, out hitInfo)) {
                if (other.gameObject.name == "Player1 Garçon") {
                    isP1 = true;
                } else {
                    isP1 = false;
                }
                ink = hitInfo.transform.GetComponent<InkCanvas>();
                Debug.Log("trying to get ink from" + hitInfo.transform.gameObject.name);
                brush = painter.GetPlayerBrush(other.name).Clone() as Brush;
                Debug.Log("bursh is " + brush);
                brush.Scale = SplashSize;
                isTriggered = true;
                particle.SetActive(true);
                picked = true;
                SoundManager.Instance.PowerUpSound(SoundPickUp);
            } else {
                Debug.LogError("SplashBomb couldn't raycast");
            }
        }
    }
}
