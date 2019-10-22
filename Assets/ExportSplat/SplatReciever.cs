﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatReciever : MonoBehaviour {

    // A mettre sur la façade et les surfaces qui doivent recevoir les splats.


	// need to add all the renderers before Start of Splat Manager
	void Awake () {
		Renderer thisRenderer = this.gameObject.GetComponent<Renderer> ();
		if (thisRenderer != null) {
			SplatManagerSystem.instance.AddRenderer (thisRenderer);
		}
	}

}
