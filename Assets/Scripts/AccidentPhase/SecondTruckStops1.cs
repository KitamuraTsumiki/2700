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
        SwitchDynamicObjectStatus();
        if (!isActive) { return; }
        EndStoryBlock();
	}

    public override void Pause()
    {
        base.Pause();
    }

    protected override void SwitchDynamicObjectStatus()
    {
        
    }

    private void EndStoryBlock(){
		// end this block of the story
		if (!truckActions.truckSoundAfterHit.isPlaying){
			hasFinished = true;
		}
	}
}
