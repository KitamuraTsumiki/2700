using UnityEngine;
/// <summary>
/// This manages the third block of the Accident (third) phase;
/// the truck stops before hit the player
/// </summary>
public class SecondTruckStops1 : ContentSubBlock {

	[SerializeField]
	private SecondTruckActions truckActions;

    protected override void Start()
    {
        base.Start();
    }

    private void Update () {
        ActivateDynamicObjectStatus();
        DeactivateDynamicObjectStatus();

        if (!isActive) { return; }
        EndStoryBlock();
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

    private void EndStoryBlock(){
		// end this block of the story
		if (!truckActions.truckSoundAfterHit.isPlaying){
			hasFinished = true;
		}
	}
}
