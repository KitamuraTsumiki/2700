using UnityEngine;

/// <summary>
/// This class manages the choreography of the Guiding (second) scene.
/// </summary>

public class GuidingContentManager : ContentManager {

	enum GuidingState { TruckComing, NavigatePlayer, TruckStops }

	public TruckComing truckComing;
	public NavigatePlayer navigatePlayer;
	public TruckStops truckStops;

	private GuidingState guidingState;


	protected override void Start () {
		// get the phase manager
		base.Start();
		guidingState = GuidingState.TruckComing;

		// deactivate all story block managers
		truckComing.enabled = false;
		navigatePlayer.enabled = false;
		truckStops.enabled = false;
	}
	
	private void Update () {
		// call each step of the story
		TruckComing();
		NavigatePlayer();
		TruckStops();

		// for testing scene transition
		base.SceneSwitch();
	}

	private void TruckComing(){
		if(guidingState != GuidingState.TruckComing) { return;}
		truckComing.enabled = true;

		var moveOnNavigatePlayer = truckComing.hasFinished && !truckComing.playerTargetZone.isInside;
		if(moveOnNavigatePlayer) {
			// move on the next "TruckStop" (second) block in this phase
			truckComing.enabled = false;
			guidingState = GuidingState.NavigatePlayer;
		} else {
			// move on "Instruction of correct position for navigation of trucks"
		}
	}

	private void NavigatePlayer(){
		if(guidingState != GuidingState.NavigatePlayer) { return;}
		navigatePlayer.enabled = true;
	}

	private void TruckStops(){
		if(guidingState != GuidingState.TruckStops) { return;}
		truckStops.enabled = true;
	}
}
