using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroductionContentManager : ContentManager {

	public 	AudioSource 	audioDescription;
	public 	LineRenderer 	stopLine;

	private float 			stopLineDisplayTime = 10f;
	private float 			audioStartTime;
	private bool 			whenStart;

	protected override void Start () {
		// get the phase manager
		base.Start();

		// set a flag to start audio introduction
		whenStart = audioDescription != null && !audioDescription.isPlaying;

		// enable scene transition
		sceneTransitionEnabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		// start description
		if(whenStart) {
			audioDescription.Play();
			audioStartTime = Time.time;
			whenStart = false;
		}

		// display stop line
		var whenDisplayGuideline = stopLine != null && audioDescription.time > stopLineDisplayTime;
		if(whenDisplayGuideline) {
			stopLine.enabled = true;
		}

		// end introduction
		float silentTime = 2f;
		float introductionEndTime = audioStartTime + audioDescription.clip.length + silentTime;
		if(Time.time > introductionEndTime) {
			SceneManager.LoadScene(nextPhase);

		} else {
			// skip introduction
			base.SceneSwitch();
		}

	}
}
