using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages the choreography of the introduction (first) scene.
/// </summary>

public class IntroductionContentManager : ContentManager {

	enum State { DescriptStart, Describing, DescriptEnd, Pause }

	[SerializeField]
	private AudioSource audioDescription;
	[SerializeField]
	private LineRenderer stopLine;
	[SerializeField]
	private IntroductionCraneActions craneActions;

	private float stopLineDisplayTime = 10f;
	private float audioStartTime;
	private State descriptState;
    private State lastState;

    public void SetDescriptEnd(){
		if(Input.GetKeyDown(KeyCode.Space)) {
			descriptState = State.DescriptEnd;
		}
	}

	protected override void Start () {
		// get the phase manager
		SetInitialState();
	}

    protected override void SetInitialState()
    {
        descriptState = startFromPaused ? State.Pause : State.DescriptStart;
        lastState = State.DescriptStart;
    }

    public override void EnterPause()
    {
        if (descriptState == State.Pause) { return; }
        lastState = descriptState;
        descriptState = State.Pause;
    }

    public override void ExitPause()
    {
        if (descriptState != State.Pause) { return; }
        descriptState = lastState;
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
        MoveOnNextPhase();

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
		//SetDescriptEnd();
		DiscriptEnd();
	}

    /// <summary>
    /// MoveOnNextPhase is called from PhaseManager class to skip this phase
    /// </summary>
    public void MoveOnNextPhase() {
    var phaseManager = GetComponentInParent<PhaseManager>();
        if (phaseManager == null) { return; }

        phaseManager.ActivateGuidingPhase();

        gameObject.SetActive(false);
    }
}
