using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TramObstacle : MonoBehaviour
{
    public float time;
    public GameObject Light;
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
        transform.DOMove(new Vector3(OffSetDirection*20, 0, 1.5f), time);
    }

    
}
