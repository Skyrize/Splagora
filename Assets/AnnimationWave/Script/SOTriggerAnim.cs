using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wall", menuName = "Wall Animation", order = 1)]
public class SOTriggerAnim : ScriptableObject
{

    public float TimeWave;


    public float minWaveOffSetZ;
    public float maxWaveOffSetZ;


    public bool ChangeRotation;
    public float TimeRotation = 0.2f;
    public float RadialRotation = 180;


    public float LatenceSwitchTexture;


    public bool ChangeScale;

    public float ModifScale;


    public bool SwitchTexture;
    public List<Texture> AllTextureStyle = new List<Texture>();
    public float SpeedSwitch = 0.2f;



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
}
