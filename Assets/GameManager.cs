using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using Es.InkPainter;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Es.InkPainter.Sample;

public class GameManager : MonoBehaviour
{
    public GameObject FacadeBrique, FacadeNeon, FacadeFin, FacadeBloc,TramSpawner, PowerUpsSpawner;
    public float Chrono;
    public float TimePast;
    private Texture texture;
    private RenderTexture rendTex;
    public bool isGaming;
    private Texture2D tex2D;
    private List<Color> allColor = new List<Color>();
    private Color MainColor;
    public Text ShowWiner,TxtWiner, TxtWinerWhite;
    public int ScoreBleu,ScoreRouge;
    public GameObject P1, P2;
    public Material Style1, Style2;
    [HideInInspector]
    public int Turn,MancheP1,MancheP2;
    public GameObject CollisionWavePrefab;
    private GameObject waveInst;
    public GameObject SpotLight;
    public List<GameObject> FeedBackWinP1 = new List<GameObject>();
    public List<GameObject> FeedBackWinP2 = new List<GameObject>();

    public GameObject EndGamePanel;
    public Text TxtEndGame, TxtEndGameWhite, M1P1, M1P2, M2P1, M2P2, M3P1, M3P2;
    public int IndexScene;
    public GameObject directionnal;

    public GameObject PlatformContener;
    public Material PlatformNormal, PlatformNeon , TramNeon,TramClassique;
    public List<GameObject> BlocChrono = new List<GameObject>();

    private SpawnerObstacle TramSpwan;
    TriggerAnim TAnim;

    [Header("Tram Color")]

    public GameObject BMX_Front, BMX_Back;
    public Material BMXClasicB, BMXNeonB, BMXClasicF, BMXNeonF;

    [Header("Bumper Color")]

    public GameObject B_SideL, B_SideR,B_Center, B_CenterSideL, B_CenterSideR;
    public Material BumperSide, BumperCenter, BumperCenterSide;
    public Material BumperSideNeon, BumperCenterNeon, BumperCenterSideNeon;

    [Header("Police Car")]

    public GameObject PoliceCar;
    [Header("ENDING")]

    public GameObject TriggerEnding;
    public bool preEnd;


    MousePainter painter;
    // Start is called before the first frame update
    void Start()
    {

        painter = GameObject.FindGameObjectWithTag("Painter").GetComponent<MousePainter>();
        preEnd = false;
        MancheP1 = 0;
        MancheP2 = 0;
        Turn = 1;
        isGaming = true;
        TramSpwan = FindObjectOfType<SpawnerObstacle>();
        TAnim = FindObjectOfType<TriggerAnim>();
        SoundManager.Instance.PlaySceneMusic1(Phase1Sound);
    }

    public AudioClip SoundtimerGong;
    // Update is called once per frame
    void Update()
    {
        TimePast += Time.deltaTime;
        if(TimePast >= Chrono && isGaming)
        {
            isGaming = false;
            EndTurn();

        }
        else if(isGaming)
        {
            ShowWiner.text = Mathf.Round(Chrono - TimePast).ToString();
        }

        if(TimePast + 5f >= Chrono && isGaming && preEnd==false)
        {
            preEnd = true;
            // TramSpawner.SetActive(false);
            TramSpawner.GetComponent<SpawnerObstacle>().CancelSpawn();
            EndChrono();
            
        }
        
        else
        {

            //TramSpawner.SetActive(true);
        }

        if((int)TimePast % 10 == 0 && Chrono - TimePast > 5 && Chrono==40)
        {
            LightBlocChrono((int)TimePast/ 10);
        }

        if (ShowWiner.text == "5")
        {
            SoundManager.Instance.SoundGong(SoundtimerGong);
            Debug.Log("Timer Gong launched");
        }
    }
    private void EndChrono()
    {
        Vector3 maxSize = new Vector3(2, 2, 2);
        Sequence pingpong = DOTween.Sequence();
        pingpong.Append(ShowWiner.transform.DOScale(Vector3.one, 0.5f));
        pingpong.Append(ShowWiner.transform.DOScale(maxSize, 0.5f));
        pingpong.Append(ShowWiner.transform.DOScale(Vector3.one, 0.5f));
        pingpong.Append(ShowWiner.transform.DOScale(maxSize, 0.5f));
        pingpong.Append(ShowWiner.transform.DOScale(Vector3.one, 0.5f));
        pingpong.Append(ShowWiner.transform.DOScale(maxSize, 0.5f));
        pingpong.Append(ShowWiner.transform.DOScale(Vector3.one, 0.5f));
        pingpong.Append(ShowWiner.transform.DOScale(maxSize, 0.5f));
        pingpong.Append(ShowWiner.transform.DOScale(Vector3.one, 0.5f));
        pingpong.Append(ShowWiner.transform.DOScale(maxSize, 0.5f));
        pingpong.Append(ShowWiner.transform.DOScale(Vector3.one, 0.5f));
    }
    private void LightBlocChrono(int index)
    {
        BlocChrono[index].transform.DOMoveZ(BlocChrono[index].transform.position.z - 0.1f, 1f, false);
        BlocChrono[index].transform.DOScale(BlocChrono[index].transform.localScale * 1f, 1f);

        BlocChrono[index].GetComponent<BoxCollider>().enabled = false;
        BlocChrono[index].GetComponent<BlockWallAnimation>().enabled = false;

    }
    

    public void OnPlayerCollision()
    {
        Debug.Log("SPAWN");

        if (waveInst)
            return;
        waveInst = Instantiate(CollisionWavePrefab, P1.transform.position + (P2.transform.position - P1.transform.position) / 2, transform.rotation);
    }
    public void ResetPLayerCapacity()
    {
        painter.brush1.Scale = 0.13f;
        painter.brush2.Scale = 0.13f;

    }

    public void EndTurn()
    {
        ResetPLayerCapacity();
        TramSpawner.GetComponent<SpawnerObstacle>().StartSpawning();
        ShowWiner.text = "CHARGEMENT";

        P1.GetComponent<InputComponent>().Block();
        P2.GetComponent<InputComponent>().Block();

        if (PowerUpsSpawner.GetComponent<PowerUpSpawnerComponent>().theOnlyOne)
            Destroy(PowerUpsSpawner.GetComponent<PowerUpSpawnerComponent>().theOnlyOne);
        PowerUpsSpawner.SetActive(false);
        
        CalculateScore();
        StartCoroutine(NextTurn());

        foreach(GameObject blocChrono in BlocChrono)
        {
            blocChrono.GetComponent<BoxCollider>().enabled = true;
            blocChrono.GetComponent<BlockWallAnimation>().enabled = true;
        }
    }

    public void CalculateScore()
    {
        Material target = null;
        switch (Turn)
        {
            case 1:
                target = FacadeBrique.GetComponent<MeshRenderer>().material;
                break;
            case 2:
                target = FacadeNeon.GetComponent<MeshRenderer>().material;
                break;
            case 3:
                target = FacadeFin.GetComponent<MeshRenderer>().material;
                break;
            default:
                Debug.Log("Jamais t la ");
                break;
        }
        
        texture = target.GetTexture("_MainTex");
        rendTex  = target.GetTexture("_MainTex") as RenderTexture;

        int height = texture.height;
        int width = texture.width;

            tex2D = toTexture2D(rendTex, height, width);
       
            AnalyseColor(tex2D);
        
    }

    Texture2D toTexture2D(RenderTexture rTex , int height,int width)
    {
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex = ResizeTexture(tex, ImageFilterMode.Nearest, 0.25f);

        tex.Apply();
        return tex;
    }

    public enum ImageFilterMode : int
    {
        Nearest = 0,
        Biliner = 1,
        Average = 2
    }

    private static Texture2D ResizeTexture(Texture2D pSource, ImageFilterMode pFilterMode, float pScale)
    {

        //*** Variables
        int i;

        //*** Get All the source pixels
        Color[] aSourceColor = pSource.GetPixels(0);
        Vector2 vSourceSize = new Vector2(pSource.width, pSource.height);

        //*** Calculate New Size
        float xWidth = Mathf.RoundToInt((float)pSource.width * pScale);
        float xHeight = Mathf.RoundToInt((float)pSource.height * pScale);

        //*** Make New
        Texture2D oNewTex = new Texture2D((int)xWidth, (int)xHeight, TextureFormat.RGBA32, false);

        //*** Make destination array
        int xLength = (int)xWidth * (int)xHeight;
        Color[] aColor = new Color[xLength];

        Vector2 vPixelSize = new Vector2(vSourceSize.x / xWidth, vSourceSize.y / xHeight);

        //*** Loop through destination pixels and process
        Vector2 vCenter = new Vector2();
        for (i = 0; i < xLength; i++)
        {

            //*** Figure out x&y
            float xX = (float)i % xWidth;
            float xY = Mathf.Floor((float)i / xWidth);

            //*** Calculate Center
            vCenter.x = (xX / xWidth) * vSourceSize.x;
            vCenter.y = (xY / xHeight) * vSourceSize.y;

            //*** Do Based on mode
            //*** Nearest neighbour (testing)
            if (pFilterMode == ImageFilterMode.Nearest)
            {

                //*** Nearest neighbour (testing)
                vCenter.x = Mathf.Round(vCenter.x);
                vCenter.y = Mathf.Round(vCenter.y);

                //*** Calculate source index
                int xSourceIndex = (int)((vCenter.y * vSourceSize.x) + vCenter.x);

                //*** Copy Pixel
                aColor[i] = aSourceColor[xSourceIndex];
            }

            //*** Bilinear
            else if (pFilterMode == ImageFilterMode.Biliner)
            {

                //*** Get Ratios
                float xRatioX = vCenter.x - Mathf.Floor(vCenter.x);
                float xRatioY = vCenter.y - Mathf.Floor(vCenter.y);

                //*** Get Pixel index's
                int xIndexTL = (int)((Mathf.Floor(vCenter.y) * vSourceSize.x) + Mathf.Floor(vCenter.x));
                int xIndexTR = (int)((Mathf.Floor(vCenter.y) * vSourceSize.x) + Mathf.Ceil(vCenter.x));
                int xIndexBL = (int)((Mathf.Ceil(vCenter.y) * vSourceSize.x) + Mathf.Floor(vCenter.x));
                int xIndexBR = (int)((Mathf.Ceil(vCenter.y) * vSourceSize.x) + Mathf.Ceil(vCenter.x));

                //*** Calculate Color
                aColor[i] = Color.Lerp(
                    Color.Lerp(aSourceColor[xIndexTL], aSourceColor[xIndexTR], xRatioX),
                    Color.Lerp(aSourceColor[xIndexBL], aSourceColor[xIndexBR], xRatioX),
                    xRatioY
                );
            }

            //*** Average
            else if (pFilterMode == ImageFilterMode.Average)
            {

                //*** Calculate grid around point
                int xXFrom = (int)Mathf.Max(Mathf.Floor(vCenter.x - (vPixelSize.x * 0.5f)), 0);
                int xXTo = (int)Mathf.Min(Mathf.Ceil(vCenter.x + (vPixelSize.x * 0.5f)), vSourceSize.x);
                int xYFrom = (int)Mathf.Max(Mathf.Floor(vCenter.y - (vPixelSize.y * 0.5f)), 0);
                int xYTo = (int)Mathf.Min(Mathf.Ceil(vCenter.y + (vPixelSize.y * 0.5f)), vSourceSize.y);

                //*** Loop and accumulate
                // Vector4 oColorTotal = new Vector4();
                Color oColorTemp = new Color();
                float xGridCount = 0;
                for (int iy = xYFrom; iy < xYTo; iy++)
                {
                    for (int ix = xXFrom; ix < xXTo; ix++)
                    {

                        //*** Get Color
                        oColorTemp += aSourceColor[(int)(((float)iy * vSourceSize.x) + ix)];

                        //*** Sum
                        xGridCount++;
                    }
                }

                //*** Average Color
                aColor[i] = oColorTemp / (float)xGridCount;
            }
        }

        //*** Set Pixels
        oNewTex.SetPixels(aColor);
        oNewTex.Apply();

        //*** Return
        return oNewTex;
    }

    private void AnalyseColor(Texture2D target)
    {
        Color[] colors = target.GetPixels(0, 0, target.width, target.height);
        //int height = target.height;
        //int width = target.width;
        foreach (Color color in colors) {
            if (color.b > 0.8f) {

                ScoreBleu++;
            }
            if (color.r > 0.8f) {
                ScoreRouge++;
            }

        }
        //for(int i=0; i<height; i++)
        //{
        //    for(int y = 0; y < width; y++)
        //    {
                //if (!allColor.Contains(target.GetPixel(i, y)))
                //{

                //    allColor.Add(target.GetPixel(i, y));
                //}
                //else
                //{
                    //if (target.GetPixel(i, y).g > 0.8f){
                        
                    //    ScoreBleu++;
                    //}
                    //if(target.GetPixel(i,y).r > 0.8f)
                    //{
                    //    ScoreRouge++;
                    //}
                    
                //}
               
            //}
        //}

    }
    //Set texture on animated wall
    public  void SetTextureToTransition(GameObject targetWall)
    {
        foreach(Transform bloc in FacadeBloc.transform)
        {
            bloc.GetComponent<MeshRenderer>().material = targetWall.GetComponent<MeshRenderer>().material;
        }
    }


    public AudioClip Phase1Sound;
    public AudioClip Phase2Sound;
    public AudioClip Phase3Sound;
    public AudioClip WaveSoundAnim;
    IEnumerator NextTurn()
    {
        
        if (ScoreBleu > ScoreRouge)
        {
            if (Turn <= 2)
            {
                TxtWiner.text = "EQUIPE BLEUE GAGNE";
                TxtWiner.color = Color.blue;
                TxtWinerWhite.text = "EQUIPE BLEUE GAGNE";
                FeedBackWinP2[MancheP2].SetActive(true);
                P2.GetComponentInChildren<Animator>().SetBool("Win", true);
                P1.GetComponentInChildren<Animator>().SetBool("Loose", true);

            }
            MancheP2++;
        }
        else
        {
            if (Turn <= 2)
            {
                TxtWiner.text = "EQUIPE ROUGE GAGNE";
                TxtWiner.color = Color.red;
                TxtWinerWhite.text = "EQUIPE ROUGE GAGNE";
                FeedBackWinP1[MancheP1].SetActive(true);
                P1.GetComponentInChildren<Animator>().SetBool("Win", true);
                P2.GetComponentInChildren<Animator>().SetBool("Loose", true);
            }
            MancheP1++;
        }

        if (Turn <= 2) 
            TAnim.Transition();
        if (Turn == 3)
        {
            TriggerEnding.SetActive(true);
        }
        SoundManager.Instance.WaveSoundEffect(WaveSoundAnim);

        yield return new WaitForSeconds(3.5f);

        foreach (GameObject blocChrono in BlocChrono)
        {
            blocChrono.GetComponent<BoxCollider>().enabled = false;
            blocChrono.GetComponent<BlockWallAnimation>().enabled = false;
        }
        switch (Turn)
        {
            case 1:
                Debug.Log("Fin tour 1 ");
                SoundManager.Instance.StopSceneMusic1(Phase1Sound);
                SoundManager.Instance.PlaySceneMusic2(Phase2Sound);                
                FacadeBrique.SetActive(false);
                SetTextureToTransition(FacadeNeon);
                M1P1.text =Mathf.Round( (((float)ScoreBleu / ((float)ScoreBleu + (float)ScoreRouge)) * 100)) + "%";
                M1P2.text = Mathf.Round((((float)ScoreRouge / ((float)ScoreBleu + (float)ScoreRouge)) * 100)) + "%";
                //Tram Render
                TramSpwan.currentStyle = TramNeon;

                //BMX Render
                BMX_Back.GetComponent<MeshRenderer>().material = BMXNeonB;
                BMX_Front.GetComponent<MeshRenderer>().material = BMXNeonF;

                //Bumper Render
                B_Center.GetComponent<MeshRenderer>().material = BumperCenterNeon;

                B_CenterSideL.GetComponent<MeshRenderer>().material = BumperCenterSideNeon;
                B_CenterSideR.GetComponent<MeshRenderer>().material = BumperCenterSideNeon;

                B_SideL.GetComponent<MeshRenderer>().material = BumperSideNeon;
                B_SideR.GetComponent<MeshRenderer>().material = BumperSideNeon;



                foreach (Transform render in PlatformContener.transform)
                {
                    render.GetComponent<MeshRenderer>().material = PlatformNeon;
                }

                break;
            case 2:
                Debug.Log("Fin tour 2");
                SoundManager.Instance.StopSceneMusic2(Phase2Sound);
                SoundManager.Instance.PlaySceneMusic3(Phase3Sound);               
                FacadeNeon.SetActive(false);
                SetTextureToTransition(FacadeFin);
                SpotLight.SetActive(true);
                directionnal.GetComponent<FadeOverTimeComponent>().Fade();

                M2P1.text = Mathf.Round(((float)ScoreBleu / ((float)ScoreBleu + (float)ScoreRouge) * 100) )+ "%";
                M2P2.text = Mathf.Round(((float)ScoreRouge / ((float)ScoreBleu + (float)ScoreRouge) * 100)) + "%";
                
                //Tram Render
                TramSpwan.currentStyle = TramClassique;

                //BMX Render
                BMX_Back.GetComponent<MeshRenderer>().material = BMXClasicB;
                BMX_Front.GetComponent<MeshRenderer>().material = BMXClasicF;
                
                //Bumper Render
                B_Center.GetComponent<MeshRenderer>().material = BumperCenter;

                B_CenterSideL.GetComponent<MeshRenderer>().material = BumperCenterSide;
                B_CenterSideR.GetComponent<MeshRenderer>().material = BumperCenterSide;

                B_SideL.GetComponent<MeshRenderer>().material = BumperSide;
                B_SideR.GetComponent<MeshRenderer>().material = BumperSide;

                TramSpawner.GetComponent<SpawnerObstacle>().prefabTram = PoliceCar;

                foreach (Transform render in PlatformContener.transform)
                {
                    render.GetComponent<MeshRenderer>().material = PlatformNormal;
                }

                break;
            case 3:
                EndGame();
                
                M3P1.text = Mathf.Round(((float)ScoreBleu / ((float)ScoreBleu + (float)ScoreRouge) * 100)) + "%";
                M3P2.text = Mathf.Round(((float)ScoreRouge / ((float)ScoreBleu + (float)ScoreRouge) * 100)) + "%";
                break;
            default: break;
        }
        Turn++;
        if (Turn > 3)
        {

        }
        else
        {
            P1.GetComponent<InputComponent>().Release();
            P2.GetComponent<InputComponent>().Release();

            P2.GetComponentInChildren<Animator>().SetBool("Win", false);
            P1.GetComponentInChildren<Animator>().SetBool("Win", false);
            P1.GetComponentInChildren<Animator>().SetBool("Loose", false);
            P2.GetComponentInChildren<Animator>().SetBool("Loose", false);
            ScoreRouge = 0;
            ScoreBleu = 0;
            PowerUpsSpawner.SetActive(true);



            TimePast = 0;
            isGaming = true;
            preEnd = false;
            ShowWiner.text = "";
            TxtWiner.text = "";
            TxtWinerWhite.text = "";
        }

    }
    public void EndGame()
    {
        EndGamePanel.SetActive(true);
        if (MancheP1 > MancheP2)
        {
            TxtEndGame.text = "VICTOIRE ROUGE";
            TxtEndGame.color = Color.red;
            TxtEndGameWhite.text = "VICTOIRE ROUGE";
            P1.GetComponentInChildren<Animator>().SetBool("Win", true);
            P2.GetComponentInChildren<Animator>().SetBool("Loose", true);
        }
        else
        {
            TxtEndGame.text = "VICTOIRE BLEUE";
            TxtEndGame.color = Color.blue;
            TxtEndGameWhite.text = "VICTOIRE BLEUE";

            P2.GetComponentInChildren<Animator>().SetBool("Win", true);
            P1.GetComponentInChildren<Animator>().SetBool("Loose", true);
        }
    }

    public void ButtonQuitter()
    {
        Application.Quit();
    }

    public void ButtonReplay()
    {
        SceneManager.LoadScene(IndexScene);
    }

}
