using UnityEngine;
using System.Collections;

public class SplatMakerExample : MonoBehaviour {

    Vector4 channelMask1 = new Vector4(0, 1, 1, 0);
    Vector4 channelMask = new Vector4(0, 1, 0, 1);


    static bool start_game = true;

    int splatsX = 1;
	int splatsY = 1;

	public float splatScale = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// Get how many splats are in the splat atlas
		splatsX = SplatManagerSystem.instance.splatsX;
		splatsY = SplatManagerSystem.instance.splatsY;
        
			channelMask1 = new Vector4(1,0,0,0);
			channelMask = new Vector4(0,0,1,1);


        // A uniformiser (y a une bool différente qui remplit la même fonction dans les scripts suivants "Menu.cs", "SplatMakerExample.cs" et celui-ci) Faire que ce soit la même qui gère les trois trucs.
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Joystick2Button0) || Input.GetKeyDown(KeyCode.Space)) {
            if (start_game == false)
            {
                start_game = !start_game;
            }
        }
		

        if (start_game) {
            Vector3 ray = GameObject.FindGameObjectWithTag("Player1").transform.position;               // C'est ce qui gère le spawn des splats (tâches de peintures)
            Vector3 ray2 = GameObject.FindGameObjectWithTag("Player2").transform.position;
            RaycastHit hit;

            if (Physics.Raycast(GameObject.FindGameObjectWithTag("Player1").transform.position, transform.TransformDirection(Vector3.forward), out hit, 10000))
            {

                Vector3 leftVec = Vector3.Cross(hit.normal, Vector3.up);
                float randScale = Random.Range(0.5f, 1.5f);

                GameObject newSplatObject = new GameObject();
                newSplatObject.transform.position = hit.point;
                if (leftVec.magnitude > 0.001f)
                {
                    newSplatObject.transform.rotation = Quaternion.LookRotation(leftVec, hit.normal);
                }
                newSplatObject.transform.RotateAround(hit.point, hit.normal, Random.Range(-180, 180));
                newSplatObject.transform.localScale = new Vector3(randScale, randScale * 0.5f, randScale) * splatScale;

                Splat newSplat;
                newSplat.splatMatrix = newSplatObject.transform.worldToLocalMatrix;
                newSplat.channelMask = channelMask1;

                float splatscaleX = 1.0f / splatsX;
                float splatscaleY = 1.0f / splatsY;
                float splatsBiasX = Mathf.Floor(Random.Range(0, splatsX * 0.99f)) / splatsX;
                float splatsBiasY = Mathf.Floor(Random.Range(0, splatsY * 0.99f)) / splatsY;

                newSplat.scaleBias = new Vector4(splatscaleX, splatscaleY, splatsBiasX, splatsBiasY);

                SplatManagerSystem.instance.AddSplat(newSplat);
                GameObject.Destroy(newSplatObject);
            }


            if (Physics.Raycast(GameObject.FindGameObjectWithTag("Player2").transform.position, transform.TransformDirection(Vector3.forward), out hit, 10000))
            {

                Vector3 leftVec = Vector3.Cross(hit.normal, Vector3.up);
                float randScale = Random.Range(0.5f, 1.5f);

                GameObject newSplatObject = new GameObject();
                newSplatObject.transform.position = hit.point;
                if (leftVec.magnitude > 0.001f)
                {
                    newSplatObject.transform.rotation = Quaternion.LookRotation(leftVec, hit.normal);
                }
                newSplatObject.transform.RotateAround(hit.point, hit.normal, Random.Range(-180, 180));
                newSplatObject.transform.localScale = new Vector3(randScale, randScale * 0.5f, randScale) * splatScale;

                Splat newSplat;
                newSplat.splatMatrix = newSplatObject.transform.worldToLocalMatrix;
                newSplat.channelMask = channelMask;

                float splatscaleX = 1.0f / splatsX;
                float splatscaleY = 1.0f / splatsY;
                float splatsBiasX = Mathf.Floor(Random.Range(0, splatsX * 0.99f)) / splatsX;
                float splatsBiasY = Mathf.Floor(Random.Range(0, splatsY * 0.99f)) / splatsY;

                newSplat.scaleBias = new Vector4(splatscaleX, splatscaleY, splatsBiasX, splatsBiasY);

                SplatManagerSystem.instance.AddSplat(newSplat);
                GameObject.Destroy(newSplatObject);
            }
        }

    }
}
