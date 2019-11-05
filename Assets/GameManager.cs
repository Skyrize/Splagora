using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using Es.InkPainter;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject FacadeBrique, FacadeNeon, FacadeFin, FacadeBloc,TramSpawner, PowerUpsSpawner;
    public float Chrono;
    public float TimePast;
    private Texture texture;
    private RenderTexture rendTex;
    private bool isGaming;
    private Texture2D tex2D;
    private List<Color> allColor = new List<Color>();
    private Color MainColor;
    public Text ShowWiner;
    public int ScoreBleu,ScoreRouge;
    public GameObject P1, P2;
    public Material Style1, Style2;
    private int Turn,MancheP1,MancheP2;
    public GameObject CollisionWavePrefab;
    private GameObject waveInst;
    public GameObject SpotLight;
    public List<GameObject> FeedBackWinP1 = new List<GameObject>();
    public List<GameObject> FeedBackWinP2 = new List<GameObject>();

    public GameObject EndGamePanel;
    public Text TxtEndGame, M1P1, M1P2, M2P1, M2P2, M3P1, M3P2;
    public int IndexScene;
    public Light directionnal;



    // Start is called before the first frame update
    void Start()
    {
        MancheP1 = 0;
        MancheP2 = 0;
        Turn = 1;
        isGaming = true;
    }

    // Update is called once per frame
    void Update()
    {
        TimePast += Time.deltaTime;
        if(TimePast>= Chrono && isGaming)
        {
            isGaming = false;
            EndTurn();

        }
        else if(isGaming)
        {
            ShowWiner.text = Mathf.Round(Chrono - TimePast).ToString();
        }

        if(TimePast + 10f >= Chrono && isGaming)
        {
            TramSpawner.SetActive(false);
        }
        else
        {
            TramSpawner.SetActive(true);
        }


        
    }

    public void OnPlayerCollision()
    {
        Debug.Log("SPAWN");

        if (waveInst)
            return;
        waveInst = Instantiate(CollisionWavePrefab, P1.transform.position + (P2.transform.position - P1.transform.position) / 2, transform.rotation);
    }

    public void EndTurn()
    {
        ShowWiner.text = "CHARGEMENT";

        P1.GetComponent<InputComponent>().enabled = false;
        P2.GetComponent<InputComponent>().enabled = false;

        if (PowerUpsSpawner.GetComponent<PowerUpSpawnerComponent>().theOnlyOne)
            Destroy(PowerUpsSpawner.GetComponent<PowerUpSpawnerComponent>().theOnlyOne);
        PowerUpsSpawner.SetActive(false);
        
        CalculateScore();
        StartCoroutine(NextTurn());
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
                Vector4 oColorTotal = new Vector4();
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
            if (color.g > 0.8f) {

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
    


    IEnumerator NextTurn()
    {
        
        if (ScoreBleu > ScoreRouge)
        {
            if (Turn <= 2)
            {
                ShowWiner.text = "Equipe Rouge Gagne!";
                FeedBackWinP1[MancheP1].SetActive(true);
            }
            MancheP1++;
        }
        else
        {
            if (Turn <= 2)
            {
                ShowWiner.text = "Equipe Bleue Gagne!";
                FeedBackWinP2[MancheP2].SetActive(true);
            }
            MancheP2++;
        }

        
        FindObjectOfType<TriggerAnim>().Transition();

        yield return new WaitForSeconds(3.5f);

        
        switch (Turn)
        {
            case 1:
                Debug.Log("Fin tour 1 ");
                FacadeBrique.SetActive(false);
                SetTextureToTransition(FacadeNeon);
                M1P1.text =Mathf.Round( (((float)ScoreBleu / ((float)ScoreBleu + (float)ScoreRouge)) * 100)) + "%";
                M1P2.text = Mathf.Round((((float)ScoreRouge / ((float)ScoreBleu + (float)ScoreRouge)) * 100)) + "%";

                break;
            case 2:
                Debug.Log("Fin tour 2");

                FacadeNeon.SetActive(false);
                SetTextureToTransition(FacadeFin);
                SpotLight.SetActive(true);
                directionnal.intensity = 0.4f;

                M2P1.text = Mathf.Round(((float)ScoreBleu / ((float)ScoreBleu + (float)ScoreRouge) * 100) )+ "%";
                M2P2.text = Mathf.Round(((float)ScoreRouge / ((float)ScoreBleu + (float)ScoreRouge) * 100)) + "%";
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
            P1.GetComponent<InputComponent>().enabled = true;
            P2.GetComponent<InputComponent>().enabled = true;


            ScoreRouge = 0;
            ScoreBleu = 0;
            PowerUpsSpawner.SetActive(true);



            TimePast = 0;
            isGaming = true;

            ShowWiner.text = "";
        }

    }
    public void EndGame()
    {
        EndGamePanel.SetActive(true);
        if (MancheP1 > MancheP2)
        {
            TxtEndGame.text = "Equipe ROUGE Gagne la partie!";
        }
        else
        {
            TxtEndGame.text = "Equipe BLEUE Gagne la partie!";
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
