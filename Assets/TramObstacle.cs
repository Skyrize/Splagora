using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TramObstacle : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update

    public void Start()
    {
        Destroy(gameObject, time + 1);
    }
    public void SetDirection(float OffSetDirection)
    {
        transform.DOMove(new Vector3(OffSetDirection*20, 0, 1.5f), time);
    }

    
}
