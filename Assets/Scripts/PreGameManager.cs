using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter.Sample;
using UnityEngine.UI;
using DG.Tweening;

public class PreGameManager : MonoBehaviour
{
    public float timer = 3;
    public GameObject GameManager, TramSpawner, PlayerPaint, PowerUpSpawner;
    public Text text;
    public Vector3 maxSize;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.SetActive(false);
        TramSpawner.SetActive(false);
        PowerUpSpawner.SetActive(false);
        PlayerPaint.GetComponent<MousePainter>().enabled = false;

        Sequence pingpong = DOTween.Sequence();
        pingpong.Append(text.transform.DOScale(Vector3.zero, 1f));
        pingpong.Append(text.transform.DOScale(maxSize, 0.5f));
        pingpong.Append(text.transform.DOScale(Vector3.zero, 0.5f));
        pingpong.Append(text.transform.DOScale(maxSize, 0.5f));
        pingpong.Append(text.transform.DOScale(Vector3.zero, 0.5f));
        pingpong.Append(text.transform.DOScale(maxSize, 0.5f));
        pingpong.Append(text.transform.DOScale(Vector3.one, 1f));




    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
            text.text = Mathf.Ceil(timer).ToString();
        } else if (timer > -1) {
            timer -= Time.deltaTime;
            text.text = "GO";
        } else {
            Debug.Log("end");
            GameManager.SetActive(true);
            TramSpawner.SetActive(true);
            PowerUpSpawner.SetActive(true);
            PlayerPaint.GetComponent<MousePainter>().enabled = true;
            Destroy(this.gameObject);
        }
    }
}
