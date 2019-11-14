using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TramObstacle : MonoBehaviour
{
    public float speed = 20;
    public float time;
    public GameObject Light;
    public float directionX = 0;
    public Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update

    public void Start()
    {
        Destroy(gameObject, time + 1);
        if (FindObjectOfType<GameManager>().Turn > 1)
        {
            return;
        }
        else
        {
            Light.SetActive(false);
        }
    }
    public void SetDirection(float OffSetDirection)
    {
        directionX = OffSetDirection;
        velocity = Vector3.right * speed * directionX;
        transform.DOMove(new Vector3(OffSetDirection*speed, 0, 1.5f), time);
    }

    
}
