using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;
using Es.InkPainter.Sample;

public class WaterBombComponent : MonoBehaviour
{
    public float SplashSize = 2;
    [SerializeField]
    private MousePainter painter;
    private Brush brush;
    private RaycastHit hitInfo;
    private InkCanvas ink;
    bool isP1;
    bool isTriggered = false;

    private GameObject Player1;
    private GameObject Player2;

    private void Start()
    {
        painter = GameObject.FindGameObjectWithTag("Painter").GetComponent<MousePainter>();
        Player1 = GameObject.Find("Player1 Garçon");
        Player2 = GameObject.Find("Player2 Fille");
    }

    private void Update()
    {
        if (isTriggered) {
            if (isP1 == painter.turnP1) {
                ink.Erase(brush, hitInfo);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player1) {
            if (Physics.Raycast(Player2.transform.position, Vector3.forward, out hitInfo)) {
                //Send on Player2
                isP1 = false;
                ink = hitInfo.transform.GetComponent<InkCanvas>();
                brush = painter.GetPlayerBrush(Player2.name).Clone() as Brush;
                brush.Scale = SplashSize;
                isTriggered = true;
            } else {
                Debug.LogError("WaterBomb couldn't raycast");
            }
        } else if (other.gameObject == Player2) {
            if (Physics.Raycast(Player1.transform.position, Vector3.forward, out hitInfo)) {
                //Send on Player1
                isP1 = true;
                ink = hitInfo.transform.GetComponent<InkCanvas>();
                brush = painter.GetPlayerBrush(Player1.name).Clone() as Brush;
                brush.Scale = SplashSize;
                isTriggered = true;
            } else {
                Debug.LogError("WaterBomb couldn't raycast");
            }
        }
    }

}
