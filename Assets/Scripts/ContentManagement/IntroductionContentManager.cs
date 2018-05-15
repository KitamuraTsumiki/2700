using UnityEngine;
using UnityEngine.SceneManagement;

// This class manages the choreography of the introduction (first) scene.
public class IntroductionContentManager : ContentManager {

	enum State { DiscriptStart, Discripting, DiscriptEnd }

	public AudioSource audioDescription;
	public LineRenderer stopLine;

	private float stopLineDisplayTime = 10f;
	private float audioStartTime;
	private State discriptState = State.DiscriptStart;

	public void SetDiscriptEnd(){
		discriptState = State.DiscriptEnd;
	}

	protected override void Start () {
		// get the phase manager
		base.Start();

		// enable scene transition
		sceneTransitionEnabled = true;
	}

	private void WhenDrawGuideline(){
		var whenDisplayGuideline = stopLine != null 
			&& audioDescription.time > stopLineDisplayTime;
		if(whenDisplayGuideline) {
			stopLine.enabled = true;
		}
	}

	private void DiscriptStart(){
		if(discriptState != State.DiscriptStart) { return; }
		audioDescription.Play();
		audioStartTime = Time.time;
		discriptState = State.Discripting;
	}


	private void Discripting(){
		if(discriptState != State.Discripting) { return; }
		float silentTime = 2f;
		float introductionEndTime 
			= audioStartTime + audioDescription.clip.length + silentTime;
		discriptState = Time.time > introductionEndTime
			? State.Discripting : State.DiscriptEnd;
	}

	private void DiscriptEnd(){
		if(discriptState != State.DiscriptEnd) { return; }
		SceneManager.LoadScene(nextPhase);
	}

	private void Update () {
		DiscriptStart();
		WhenDrawGuideline();
		DiscriptEnd();
	}
}
