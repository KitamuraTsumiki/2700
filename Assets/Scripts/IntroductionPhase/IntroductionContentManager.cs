using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages the choreography of the introduction (first) scene.
/// </summary>

public class IntroductionContentManager : ContentManager {

	enum State { DescriptStart, Describing, DescriptEnd }

	[SerializeField]
	private AudioSource audioDescription;
	[SerializeField]
	private LineRenderer stopLine;
	[SerializeField]
	private IntroductionCraneActions craneActions;

	private float stopLineDisplayTime = 10f;
	private float audioStartTime;
	private State descriptState = State.DescriptStart;

	public void SetDescriptEnd(){
		if(Input.GetKeyDown(KeyCode.Space)) {
			descriptState = State.DescriptEnd;
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
		if(descriptState != State.DescriptStart) { return; }

		// start audio discription
		if(!audioDescription.isPlaying) {
			audioDescription.Play();
			audioStartTime = Time.time;
			descriptState = State.Describing;
		}

	}


	private void Discripting(){
		if(descriptState != State.Describing) { return; }

		ControlEnvironmentObjects();

		float silentTime = 2f;
		float introductionEndTime 
			= audioStartTime + audioDescription.clip.length + silentTime;
		descriptState = Time.time > introductionEndTime
			? State.DescriptEnd : State.Describing;
	}

	private void DiscriptEnd(){
		if(descriptState != State.DescriptEnd) { return; }
		SceneManager.LoadScene(nextPhase);
	}

	private void ControlEnvironmentObjects(){

		//activate animation of a crane
		if(!craneActions.isActive) {
			craneActions.isActive = true;
		}
	}

	private void Update () {
		Debug.Log("discription state: " + descriptState);
		DiscriptStart();
		Discripting();
		WhenDrawGuideline();
		SetDescriptEnd();
		DiscriptEnd();
	}
}
