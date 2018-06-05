using UnityEngine;
/// <summary>
/// This manages the third block of the Accident (third) phase;
/// the truck stops before hit the player
/// </summary>
public class SecondTruckStops2 : ContentSubBlock {

	private SecondTruckActions truckActions;

	private void Start(){
        base.Start();
        truckActions = GetComponent<GuidingContentManager>().secondTruck.GetComponent<SecondTruckActions>();
	}

	private void Update () {
        SwitchDynamicObjectStatus();
        if (!isActive) { return; }
        ActivateTruckActions();
	}

    public override void Pause()
    {
        base.Pause();
    }

    protected override void SwitchDynamicObjectStatus()
    {
        truckActions.isActive = isActive;
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
