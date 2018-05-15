using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuidingContentManager : ContentManager {

	protected override void Start () {
		// get the phase manager
		base.Start();
		
	}
	
	// Update is called once per frame
	void Update () {
		// for testing scene transition
		base.SceneSwitch();
	}


}
