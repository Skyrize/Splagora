using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlockWallAnimation : MonoBehaviour
{
    public float TimeWave;

    
    public float minWaveOffSetZ;
    public float maxWaveOffSetZ;

 
    public bool ChangeRotation;
    public float TimeRotation =0.2f;
    public float RadialRotation = 180;

    
    public float LatenceSwitchTexture;
    
    
    public bool ChangeScale;
    
    public float ModifScale;

    
    public bool SwitchTexture;
    public List<Texture> AllTextureStyle=new List<Texture>();
    public float SpeedSwitch=0.2f;

   
    
    public bool PingPongScale;
    public bool ChangeMesh;
    public List<Mesh> allMeshStyle = new List<Mesh>();


    

    public bool SwitchMaterial;
    public List<Material> allStyleMaterial = new List<Material>();



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

    //Call when trigger tag "TriggerAnim" hit them , play WaveAnimation
    public void AnimationBlock()
    {
        if (WaveAnim != null)
        {

            WaveAnim.Restart();
            WaveAnim.Play().OnComplete(EndAnimation);
            return;
        }

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
            AnimationBlock();
        }
    }
}
