﻿using UnityEngine;
/// <summary>
/// This manages the third block of the Accident (third) phase;
/// the truck stops before hit the player
/// </summary>
public class SecondTruckStops2 : ContentSubBlock {

	private SecondTruckActions truckActions;

	private void Start(){
		truckActions = (SecondTruckActions)GameObject.FindObjectOfType(typeof(SecondTruckActions));
	}

	private void Update () {
		ActivateTruckActions();
	}

	private void ActivateTruckActions(){
		truckActions.ActivateActionsAfterHit();

		// when the audio clip on the "truckActions" finish,
		// end this block of the story
		if (!truckActions.truckSoundAfterHit.isPlaying){
			hasFinished = true;
		}
	}
}
