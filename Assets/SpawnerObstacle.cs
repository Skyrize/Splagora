using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SpawnerObstacle : MonoBehaviour
{
    public GameObject prefabTram;
    public List<Transform> Spawners = new List<Transform>();
    public float MinIntervaleSpawn, MaxIntervaleSpawn;
    public List<Image> FeedbackSpawners = new List<Image>();
    public float TimeShowFeedBack;

    float timePast=0;
    float nextSpawneTime=3;
    bool isSpawning;
    int indexSide;
    Transform currentSpawn;
    Image currentImage;
    // Start is called before the first frame update
    void Start()
    {
        StartSpawning();
    }

    // Update is called once per frame
    void Update()
    {
        timePast += Time.deltaTime;
        if (timePast > nextSpawneTime && !isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnObstacle());
        }

    }

    void StartSpawning()
    {
        nextSpawneTime = Random.Range(MinIntervaleSpawn, MaxIntervaleSpawn);
        

    }
    private void GetRandomSide()
    {
        indexSide = Random.Range(0, 2);
        currentSpawn = Spawners[indexSide];
        currentImage = FeedbackSpawners[indexSide];

    }
    public IEnumerator SpawnObstacle()
    {
        GetRandomSide();
        //Create Animation for feedBack
        Sequence feedBackObstacle = DOTween.Sequence();
        feedBackObstacle.Append(currentImage.DOFade(1, TimeShowFeedBack / 6));
        feedBackObstacle.Append(currentImage.DOFade(0, TimeShowFeedBack / 6));
        feedBackObstacle.Append(currentImage.DOFade(1, TimeShowFeedBack / 6));
        feedBackObstacle.Append(currentImage.DOFade(0, TimeShowFeedBack / 6));
        feedBackObstacle.Append(currentImage.DOFade(1, TimeShowFeedBack / 6));
        feedBackObstacle.Append(currentImage.DOFade(0, TimeShowFeedBack / 6));
        feedBackObstacle.Append(currentImage.DOFade(1, TimeShowFeedBack / 6));
        feedBackObstacle.Append(currentImage.DOFade(0, TimeShowFeedBack / 6));
        feedBackObstacle.Insert(0,currentImage.transform.DOShakePosition(TimeShowFeedBack, 10, 10, 90, false,false));
        feedBackObstacle.Play();

        yield return new WaitForSeconds(TimeShowFeedBack);

        GameObject obstacle=Instantiate(prefabTram, currentSpawn.position, Quaternion.identity);
        //RightSpawn
        if (indexSide == 0)
        {
            obstacle.GetComponent<TramObstacle>().SetDirection(-1);

        }
        else
        {
            obstacle.GetComponent<TramObstacle>().SetDirection(1);

        }
        isSpawning = false;
        timePast = 0;
        StartSpawning();
    }
}
