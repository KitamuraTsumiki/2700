using UnityEngine;
/// <summary>
/// This manages the third block of the Accident (third) phase;
/// the truck stops before hit the player
/// </summary>
public class SecondTruckStops2 : ContentSubBlock {

	public SecondTruckActions truckActions;

	private void Start(){
        base.Start();
	}

	private void Update () {
        if (!CheckDynamicObjectReference()) { return; }

        ActivateDynamicObjectStatus();
        DeactivateDynamicObjectStatus();

        if (!isActive) { return; }
        ActivateTruckActions();
	}

    private bool CheckDynamicObjectReference()
    {
        var truckAndPlayerAreAssigned = truckActions != null;
        return truckAndPlayerAreAssigned;
    }

    /// <summary>
    /// pausing function is called by "content manager" class
    /// in a function with the same name.
    /// it can be triggered by UI panels
    /// </summary>
    public override void Pause()
    {
        base.Pause();
    }

    protected override void ActivateDynamicObjectStatus()
    {
        if (truckActions.isActive == isActive) { return; }
        TruckActionControl.ActivateTruckAction(truckActions);
    }

    protected override void DeactivateDynamicObjectStatus()
    {
        if (truckActions.isActive == isActive) { return; }
        TruckActionControl.DeactivateTruckAction(truckActions);
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
