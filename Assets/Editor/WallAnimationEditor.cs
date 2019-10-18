using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BlockWallAnimation))]
[CanEditMultipleObjects]
public class WallAnimationEditor : Editor
{
    SerializedProperty TimeWave;
    SerializedProperty Latence;


    SerializedProperty AmplitudeWaveMin;
    SerializedProperty AmplitudeWaveMax;

    SerializedProperty ChangeRotation;
    SerializedProperty TimeRotation;
    SerializedProperty DegreeRotation;
    public RotationOption RotOp;

    SerializedProperty ShakeScale;
    SerializedProperty forceShake;

    SerializedProperty SwitchMaterial;
    SerializedProperty allMaterial;

    SerializedProperty SwitchTexture;
    SerializedProperty allTexture;
    SerializedProperty speedSwitchTexture;









    float minAmp;
    float maxAmp;
    bool rot;
    bool shakeScale;
    bool switchMat;
    bool switchTex;

    public enum RotationOption
    {
        Rot180 = 0,
        Rot360 = 1
    }

    void OnEnable()
    {

        // Setup the SerializedProperties.
        TimeWave = serializedObject.FindProperty("TimeWave");
        Latence = serializedObject.FindProperty("LatenceSwitchTexture");

        AmplitudeWaveMin = serializedObject.FindProperty("minWaveOffSetZ");
        AmplitudeWaveMax = serializedObject.FindProperty("maxWaveOffSetZ");

        ChangeRotation = serializedObject.FindProperty("ChangeRotation");
        TimeRotation = serializedObject.FindProperty("TimeRotation");
        DegreeRotation = serializedObject.FindProperty("RadialRotation");

        ShakeScale = serializedObject.FindProperty("ChangeScale");
        forceShake = serializedObject.FindProperty("ModifScale");

        SwitchMaterial = serializedObject.FindProperty("SwitchMaterial");
        allMaterial = serializedObject.FindProperty("allStyleMaterial");

        SwitchTexture = serializedObject.FindProperty("SwitchTexture");
        allTexture = serializedObject.FindProperty("AllTextureStyle");
        speedSwitchTexture = serializedObject.FindProperty("SpeedSwitch");
    }

    public override void OnInspectorGUI()
    {
        
        //base.OnInspectorGUI();
        
        serializedObject.Update();

        var titleStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 15 ,clipping=TextClipping.Overflow};
        titleStyle.normal.textColor = Color.blue;
        RectOffset bdr;
        bdr = titleStyle.border;
        //GUI.Label(new Rect(bdr.horizontal, bdr.vertical, 100, 30), "AA");

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        {
            //Create Title
            EditorGUILayout.LabelField("Tool for wave on wall animation", titleStyle);
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            //Simple float assign
            EditorGUILayout.Slider(TimeWave, 0.4f, 2, new GUIContent("Time during wave:"));

            Rect customField = EditorGUILayout.GetControlRect(false, 200f);
            //GUILayout.Label("AA");
            GUI.Label(customField, "AA", EditorStyles.textField);

            EditorGUILayout.Space();
            EditorGUILayout.Slider(Latence, 0, 1, new GUIContent("Latence to ComeBack"));
        }
        EditorGUILayout.EndVertical();


        EditorGUILayout.Space();

        //Get Value Amplitude
        minAmp = AmplitudeWaveMin.floatValue;
        maxAmp = AmplitudeWaveMax.floatValue;

        //Create Style center text
        //Create Title
        EditorGUILayout.LabelField("Movement Amplitude of Wave", titleStyle);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //Show Value Amplitude
        minAmp = EditorGUILayout.FloatField("Minimum Amplitude Wave",minAmp);
        maxAmp = EditorGUILayout.FloatField("Maximum Amplitude Wave", maxAmp);

        //Show MinMaxSlider
        EditorGUILayout.MinMaxSlider(ref minAmp,ref maxAmp,1,5);

        //Set Value changed
        AmplitudeWaveMin.floatValue = minAmp;
        AmplitudeWaveMax.floatValue = maxAmp;

        EditorGUILayout.LabelField("Effect Add on Climax Wave:", titleStyle);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        var BareHori = new GUIStyle(GUI.skin.horizontalSlider) { };
       
        EditorGUILayout.LabelField("", BareHori);
        //Section Rotation
        rot = ChangeRotation.boolValue;
        rot=EditorGUILayout.ToggleLeft("Rotation Animation ",rot);
        ChangeRotation.boolValue=rot;


        if (rot )
        {
            EditorGUILayout.Slider(TimeRotation, 0.1f, 1, new GUIContent("Rotation time:"));
            switch (DegreeRotation.floatValue)
            {
                case 180:
                    RotOp = RotationOption.Rot180;
                    break;
                case 360:
                    RotOp = RotationOption.Rot360;
                    break;
                default:
                    break;
            }
            RotOp = (RotationOption)EditorGUILayout.EnumPopup("Angle Rotation:", RotOp);
            switch (RotOp)
            {
                case RotationOption.Rot180:
                    DegreeRotation.floatValue = 180;
                    break;
                case RotationOption.Rot360:
                    DegreeRotation.floatValue = 360;
                    break;
                default:
                    break;
            }
        }
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("", BareHori);
        //Section Material
        switchMat = SwitchMaterial.boolValue;
        switchMat = EditorGUILayout.ToggleLeft("Switch Material ", switchMat);
        SwitchMaterial.boolValue = switchMat;

        if (switchMat)
        {
            
            EditorGUILayout.PropertyField(allMaterial,true);
        }

        // SectionTexture
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("", BareHori);
        switchTex = SwitchTexture.boolValue;
        switchTex = EditorGUILayout.ToggleLeft("Switch Texture ", switchTex);
        SwitchTexture.boolValue = switchTex;

        if (switchTex)
        {

            EditorGUILayout.PropertyField(allTexture, true);
            EditorGUILayout.Slider(speedSwitchTexture, 0.1f, 0.6f, new GUIContent("Time transition texture"));

        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("", BareHori);
        //Section Shaking

        shakeScale = ShakeScale.boolValue;
        shakeScale = EditorGUILayout.ToggleLeft("Shake Scale Animation ", shakeScale);
        ShakeScale.boolValue = shakeScale;

        if (shakeScale)
        {
            EditorGUILayout.Slider(forceShake, 30, 200, new GUIContent("Force of Shaking:"));
            
        }


        BlockWallAnimation Block = (BlockWallAnimation)target;
        EditorGUILayout.LabelField("", BareHori);
        if (GUILayout.Button("Play Wave Animation"))
        {
            //Block.CreateAnimationBlock();
        }

        serializedObject.ApplyModifiedProperties();

        


    }
}
