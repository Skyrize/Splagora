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
    private GameObject particle;

    private void Start()
    {
        painter = GameObject.FindGameObjectWithTag("Painter").GetComponent<MousePainter>();
        particle = transform.GetChild(0).GetChild(0).gameObject;
    }

    private void Update()
    {
        if (isTriggered) {
            if (isP1 == painter.turnP1 && particle.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().time == 3) {
                ink.Paint(brush, hitInfo);
                GetComponent<MeshRenderer>().enabled = false;
                particle.transform.parent.GetComponent<ParticleSystem>().Stop();
            }
            if (isP1 == painter.turnP1 && particle.GetComponent<ParticleSystem>().time == 1.5)
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") == true) {
            if (Physics.Raycast(transform.position, Vector3.forward, out hitInfo)) {
                if (other.gameObject.name == "Player1 Garçon") {
                    isP1 = true;
                } else {
                    isP1 = false;
                }
                ink = hitInfo.transform.GetComponent<InkCanvas>();
                brush = painter.GetPlayerBrush(other.name).Clone() as Brush;
                brush.Scale = SplashSize;
                isTriggered = true;
                particle.SetActive(true);
            } else {
                Debug.LogError("SplashBomb couldn't raycast");
            }
        }
    }
}
