using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter.Sample;
using UnityEngine.UI;

public class PreGameManager : MonoBehaviour
{
    public float timer = 5;
    public GameObject GameManager, TramSpawner, PlayerPaint, PowerUpSpawner;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.SetActive(false);
        TramSpawner.SetActive(false);
        PowerUpSpawner.SetActive(false);
        PlayerPaint.GetComponent<MousePainter>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
            text.text = Mathf.Ceil(timer).ToString();
        } else if (timer > -1) {
            timer -= Time.deltaTime;
            text.text = "GO !";
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
