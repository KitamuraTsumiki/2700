using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages the choreography of the Guiding (second) scene.
/// </summary>

public class GuidingContentManager : ContentManager {

	enum GuidingState { TruckComing, NavigatePlayer, TruckStops }

	public TruckComing truckComing;
	public TruckStops truckStops;

	private GuidingState guidingState;


	protected override void Start () {
		// get the phase manager
		base.Start();
		guidingState = GuidingState.TruckComing;

		// Turn all UI canvases off
		truckComing.InitUI();

		// deactivate all story block managers
		truckComing.enabled = false;
		truckStops.enabled = false;
	}
	
	private void Update () {
		// call each step of the story
		TruckComing();
		TruckStops();

		// for testing scene transition
		base.SceneSwitch();
	}

	private void TruckComing(){
		if(guidingState != GuidingState.TruckComing) { return;}
		truckComing.enabled = true;

		if(truckComing.hasFinished) {
			// move on the next "TruckStop" (second) block in this phase
			truckComing.enabled = false;
			guidingState = GuidingState.TruckStops;
		} else {
			// move on "Instruction of correct position for navigation of trucks"
		}
	}
	private void TruckStops(){
		if(guidingState != GuidingState.TruckStops) { return;}
		truckStops.enabled = true;

		if(truckStops.hasFinished) {
			// move on the next phase "Accident"
			truckStops.enabled = false;
			SceneManager.LoadScene(nextPhase);
		}
	}
}
