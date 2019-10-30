using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter.Sample;
using Es.InkPainter;

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

    private void Start()
    {
        painter = GameObject.FindGameObjectWithTag("Painter").GetComponent<MousePainter>();
    }

    private void Update()
    {
        if (isTriggered) {
            if (isP1 == painter.turnP1) {
                ink.Paint(brush, hitInfo);
                Destroy(gameObject);
            }
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
            } else {
                Debug.LogError("SplashBomb couldn't raycast");
            }
        }
    }
}
