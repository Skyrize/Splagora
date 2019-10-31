using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlockWallAnimation : MonoBehaviour
{
    public SOTriggerAnim AnimSeting;

    private float TimeWave;


    private float minWaveOffSetZ;
    private float maxWaveOffSetZ;


    private bool ChangeRotation;
    private float TimeRotation =0.2f;
    private float RadialRotation = 180;


    private float LatenceSwitchTexture;


    private bool ChangeScale;

    private float ModifScale;


    private bool SwitchTexture;
    private List<Texture> AllTextureStyle=new List<Texture>();
    private float SpeedSwitch=0.2f;



    private bool PingPongScale;
    private bool ChangeMesh;
    private List<Mesh> allMeshStyle = new List<Mesh>();




    private bool SwitchMaterial;
    private List<Material> allStyleMaterial = new List<Material>();



    private int CountStyleMat;
    private int CurrentStyleIndex = 0;
    private int CountStyleMesh;
    private int CurrentStyleMeshIndex = 0;
    private Vector3 startPos;
    private Vector3 startScale;
    private Sequence WaveAnim = null;

    public void Start()
    {
        startPos = transform.position;
        startScale = transform.localScale;
        CountStyleMat = allStyleMaterial.Count;
        CountStyleMesh = allMeshStyle.Count;
        //AnimationBlock();
        
        
    }
    private void SetValueScriptableObject()
    {
        TimeWave = AnimSeting.TimeWave;

        minWaveOffSetZ = AnimSeting.minWaveOffSetZ;
        maxWaveOffSetZ = AnimSeting.maxWaveOffSetZ;

        ChangeRotation = AnimSeting.ChangeRotation;
        TimeRotation = AnimSeting.TimeRotation;
        RadialRotation = AnimSeting.RadialRotation;

        LatenceSwitchTexture = AnimSeting.LatenceSwitchTexture;

        ChangeScale = AnimSeting.ChangeScale;
        ModifScale = AnimSeting.ModifScale;

        SwitchTexture = AnimSeting.SwitchTexture;
        AllTextureStyle = AnimSeting.AllTextureStyle;
        SpeedSwitch = AnimSeting.SpeedSwitch;

        ChangeMesh = AnimSeting.ChangeMesh;
        allMeshStyle = AnimSeting.allMeshStyle;

        SwitchMaterial = AnimSeting.SwitchMaterial;
        allStyleMaterial = AnimSeting.allStyleMaterial;

        CountStyleMat = allStyleMaterial.Count;
        CountStyleMesh = allMeshStyle.Count;
    }

    //Call when trigger tag "TriggerAnim" hit them , play WaveAnimation
    public void AnimationBlock(SOTriggerAnim targetAnim)
    {
        /*
        if (WaveAnim != null)
        {

            WaveAnim.Restart();
            WaveAnim.Play().OnComplete(EndAnimation);
            return;
        }*/
        transform.position = startPos;
        transform.localScale = startScale;

        AnimSeting = targetAnim;
        SetValueScriptableObject();
        float offsetZ = Random.Range(minWaveOffSetZ, maxWaveOffSetZ);

        //Create Animation with tween ( interpolation )
        WaveAnim = DOTween.Sequence();

        WaveAnim.SetAutoKill(false);
        //Move forward
        WaveAnim.Append(gameObject.transform.DOMove(transform.position + new Vector3(0, 0, -offsetZ), ((TimeWave / 2)+Random.Range(0f, LatenceSwitchTexture))));
        //Rotation at the end of ForwardAnimation
        if (ChangeRotation)
        {
            if (RadialRotation == 180)
            {
                WaveAnim.Insert((TimeWave / 2) - TimeRotation
                , gameObject.transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 0, RadialRotation),
                TimeRotation, RotateMode.Fast));
            }
            else
            {
                WaveAnim.Insert((TimeWave / 2) - TimeRotation
                    , gameObject.transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 0, RadialRotation),
                    TimeRotation, RotateMode.FastBeyond360));
            }
        }
        //Used for Switch Mesh principaly, pingpong the scale between currenet and 0 , can switch mesh when is at Scale 0
        if (PingPongScale)
        {
            WaveAnim.Insert(TimeWave / 3, gameObject.transform.DOScale(new Vector3(0, 0, 0), TimeRotation / 2));

            if (ChangeMesh)
            {
                WaveAnim.InsertCallback(TimeWave / 3 + TimeRotation / 2, SwitchMesh);
            }

            WaveAnim.InsertCallback(TimeWave / 3 + TimeRotation / 2, SwitchMatWave);

            WaveAnim.Insert(TimeWave / 3 + TimeRotation / 2 , gameObject.transform.DOScale(startScale, TimeRotation / 2));


        }
        if(SwitchMaterial)
        {
            WaveAnim.AppendCallback(SwitchMatWave);
        }
        if (SwitchTexture)
        {
            WaveAnim.AppendCallback(SwitchTextureWave);

        }
        if (ChangeScale)
        {
            WaveAnim.Insert(TimeWave / 3, gameObject.transform.DOShakeScale(TimeWave/2, ModifScale, 10, 90));
            
        }
        //Return pos
        WaveAnim.Append(gameObject.transform.DOMove(transform.position , TimeWave / 2));
        
        //Snap position next END Anim

    }
    private void StartAnim()
    {
        WaveAnim.Play().OnComplete(EndAnimation);
    }

    private void SwitchTextureWave()
    {
        transform.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", AllTextureStyle[0]);
        transform.GetComponent<MeshRenderer>().material.SetTexture("_SecTex", AllTextureStyle[1]);
        //transform.GetComponent<MeshRenderer>().material.SetFloat("_Blend", 0);
        float blend = transform.GetComponent<MeshRenderer>().material.GetFloat("_Blend");
        if (blend < 0.5f)
        {
            transform.GetComponent<MeshRenderer>().material.DOFloat(1, "_Blend", 0.3f);
        }
        else
        {
            transform.GetComponent<MeshRenderer>().material.DOFloat(0, "_Blend", 0.3f);
        }

    }

    //Reset Position of Obj
    private void EndAnimation()
    {
        transform.position = startPos;
        transform.localScale = startScale;


    }
    //Switch Material instantlie
    private void SwitchMatWave()
    {
        CurrentStyleIndex++;
        if (CurrentStyleIndex >= CountStyleMat)
        {
            CurrentStyleIndex = 0;
        }
        transform.GetComponent<MeshRenderer>().material = allStyleMaterial[CurrentStyleIndex];
    }
    //Call when Scale = 0 to Switch Mesh smooth

    private void SwitchMesh()
    {
        CurrentStyleMeshIndex++;
        if (CurrentStyleMeshIndex >= CountStyleMesh)
        {
            CurrentStyleMeshIndex = 0;
        }
        transform.GetComponent<MeshFilter>().mesh = allMeshStyle[CurrentStyleMeshIndex];
    }

    //Trigger Call animation 
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.CompareTo("TriggerAnim")==0)
        {
            //AnimationBlock();
        }
    }
}
