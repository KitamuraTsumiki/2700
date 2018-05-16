using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages the choreography of the Guiding (second) scene.
/// </summary>

public class GuidingContentManager : ContentManager {

	enum State { FirstTruckComing, FirstTruckStops, SecondTruckFound, SecondTruckComing, SecondTruckHits, SecondTruckStops }

	// Contents of guiding (second) phase
	public TruckComing truckComing;
	public TruckStops truckStops;

	// Contents of accident (third) phase
	public SecondTruckFound secondTruckFound;
	public SecondTruckComing secondTruckComing;
	public SecondTruckHits secondTruckHits;
	public SecondTruckStops secondTruckStops;

	private State state;


	protected override void Start () {
		// get the phase manager
		base.Start();
		state = State.FirstTruckComing;

		// Turn all UI canvases off
		truckComing.InitUI();

		// deactivate all story block managers
		truckComing.enabled = false;
		truckStops.enabled = false;
		secondTruckFound.enabled = false;
		secondTruckComing.enabled = false;
		secondTruckHits.enabled = false;
		secondTruckStops.enabled = false;
	}
	
	private void Update () {
		// call each step of the story
		TruckComing();
		TruckStops();
		SecondTruckFound();
		SecondTruckComing();
		SecondTruckHits();
		SecondTruckStops();

		// for testing scene transition
		base.SceneSwitch();
	}

	private void TruckComing(){
		if(state != State.FirstTruckComing) { return;}
		truckComing.enabled = true;

		if(truckComing.hasFinished) {
			// move on the next "TruckStop" (second) block in this phase
			truckComing.enabled = false;
			state = State.FirstTruckStops;
		} else {
			// move on "Instruction of correct position for navigation of trucks"
		}
	}
	private void TruckStops(){
		if(state != State.FirstTruckStops) { return;}
		truckStops.enabled = true;

		if(truckStops.hasFinished) {
			// move on the next phase "Accident"
			truckStops.enabled = false;
			state = State.SecondTruckFound;
		}
	}

	protected void SecondTruckFound(){
		if(state != State.SecondTruckFound) { return;}
		secondTruckFound.enabled = true;

		// split the route
		if(!secondTruckFound.hasFinished) { return; }
		secondTruckFound.enabled = false;
		if(secondTruckFound.isInMainRoute) {
			state = State.SecondTruckComing;
		} else {
			state = State.SecondTruckStops;
		}
	}

	protected void SecondTruckComing(){
		if(state != State.SecondTruckComing) { return;}

		// split the route
		if (!secondTruckComing.hasFinished){ return; }
		secondTruckComing.enabled = false;
		if(secondTruckComing.isInMainRoute) {
			state = State.SecondTruckHits;
		} else {
			state = State.SecondTruckStops;
		}
	}

	protected void SecondTruckHits(){
		if(state != State.SecondTruckHits) { return;}
		if(secondTruckHits.hasFinished) {
			// move on "replay" phase
			secondTruckHits.enabled = false;
		}
	}

	protected void SecondTruckStops(){
		if(state != State.SecondTruckStops) { return;}
		if(secondTruckStops.hasFinished) {
			// move on "instruction" phase
			secondTruckStops.enabled = false;
		}
	}
}
