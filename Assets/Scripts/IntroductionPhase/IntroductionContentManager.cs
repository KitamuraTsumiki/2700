using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages the choreography of the introduction (first) scene.
/// </summary>

public class IntroductionContentManager : ContentManager {

	enum State { DiscriptStart, Discribing, DiscriptEnd }

	public AudioSource audioDescription;
	public LineRenderer stopLine;

	private float stopLineDisplayTime = 10f;
	private float audioStartTime;
	private State discriptState = State.DiscriptStart;

	public void SetDiscriptEnd(){
		if(Input.GetKeyDown(KeyCode.Space)) {
			discriptState = State.DiscriptEnd;
		}
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
		if(!audioDescription.isPlaying) {
			audioDescription.Play();
			audioStartTime = Time.time;
			discriptState = State.Discribing;
		}

	}


	private void Discripting(){
		if(discriptState != State.Discribing) { return; }
		float silentTime = 2f;
		float introductionEndTime 
			= audioStartTime + audioDescription.clip.length + silentTime;
		discriptState = Time.time > introductionEndTime
			? State.DiscriptEnd : State.Discribing;
	}

	private void DiscriptEnd(){
		if(discriptState != State.DiscriptEnd) { return; }
		SceneManager.LoadScene(nextPhase);
	}

	private void Update () {
		Debug.Log("discription state: " + discriptState);
		DiscriptStart();
		Discripting();
		WhenDrawGuideline();
		SetDiscriptEnd();
		DiscriptEnd();
	}
}
