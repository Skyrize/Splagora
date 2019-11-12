using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SpawnerObstacle : MonoBehaviour
{
    public GameObject prefabTram, bumperLeft, bumperRight;
    public List<Transform> Spawners = new List<Transform>();
    public float MinIntervaleSpawn, MaxIntervaleSpawn;
    public List<Image> FeedbackSpawners = new List<Image>();
    public List<GameObject> triggerFeedBack = new List<GameObject>();
    public Material currentStyle;
    public float TimeShowFeedBack;

    public GameObject targetWallAnim;

    float timePast=0;
    float nextSpawneTime=3;
    bool isSpawning;
    int indexSide;
    Transform currentSpawn;
    Image currentImage;
    GameObject targetTrigger;
    public AudioClip TramSoundSpawn;
    // Start is called before the first frame update
    void Start()
    {
        StartSpawning();
        FindObjectOfType<GameManager>().SetTextureToTransition(targetWallAnim);

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
    private void OnEnable()
    {
        
    }

    void StartSpawning()
    {
        nextSpawneTime = Random.Range(MinIntervaleSpawn, MaxIntervaleSpawn);
        

    }
    private void GetRandomSide()
    {
        Debug.Log("fonction d'appel icone attention tram");
        indexSide = Random.Range(0, 2);
        currentSpawn = Spawners[indexSide];
        currentImage = FeedbackSpawners[indexSide];

        SoundManager.Instance.TramSoundComing(TramSoundSpawn);
        Debug.Log("son du tram joué");
        targetTrigger = triggerFeedBack[indexSide];

    }
    private void ActivateTrigger()
    {
        targetTrigger.SetActive(!targetTrigger.activeInHierarchy);
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

        Sequence feedBackObstacle2 = DOTween.Sequence();
        feedBackObstacle2.AppendCallback(ActivateTrigger);
        feedBackObstacle2.AppendInterval(0.3f);
        feedBackObstacle2.AppendCallback(ActivateTrigger);
        feedBackObstacle2.AppendInterval(0.3f);
        feedBackObstacle2.AppendCallback(ActivateTrigger);
        feedBackObstacle2.AppendInterval(0.3f);
        feedBackObstacle2.AppendCallback(ActivateTrigger);
        feedBackObstacle2.AppendInterval(0.3f);
        feedBackObstacle2.AppendCallback(ActivateTrigger);
        feedBackObstacle2.AppendInterval(0.3f);
        feedBackObstacle2.AppendCallback(ActivateTrigger);
        feedBackObstacle2.AppendInterval(0.3f);
        feedBackObstacle2.Play();
        yield return new WaitForSeconds(TimeShowFeedBack);

        GameObject obstacle =Instantiate(prefabTram, currentSpawn.position, currentSpawn.rotation);
        obstacle.GetComponentInChildren<MeshRenderer>().material = currentStyle;
        //RightSpawn
        if (indexSide == 0)
        {
            obstacle.GetComponent<TramObstacle>().SetDirection(-1);
            bumperLeft.SetActive(true);
            bumperRight.SetActive(false);
        }
        else
        {
            obstacle.GetComponent<TramObstacle>().SetDirection(1);
            bumperRight.SetActive(true);
            bumperLeft.SetActive(false);

        }
        isSpawning = false;
        timePast = 0;
        StartSpawning();
    }
}
