using UnityEngine;
/// <summary>
/// This manages the third block of the Accident (third) phase;
/// the truck stops before hit the player
/// </summary>
public class SecondTruckStops1 : ContentSubBlock {

	public SecondTruckActions truckActions;

	private void Update () {
		EndStoryBlock();
	}

	private void EndStoryBlock(){
		// end this block of the story
		if (!truckActions.truckSoundAfterHit.isPlaying){
			hasFinished = true;
		}
	}
}
