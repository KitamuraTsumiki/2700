using UnityEngine;

/// <summary>
/// This manages the third block of the Guiding (second) part;
/// The truck stops at correct position
/// </summary>
public class TruckStops : ContentSubBlock {

    public FirstTruckActions firstTruck;
    public SecondTruckActions secondTruck;
    public Transform playerHead;

    [SerializeField]
	private Transform cranePosGuide;
    [SerializeField]
	private Transform truckGuide;
    
	protected override void Start(){
        base.Start();
    }

	private void Update () {
        if (!CheckDynamicObjectReference()) { return; }

        ActivateDynamicObjectStatus();
        DeactivateDynamicObjectStatus();

        if (!isActive) { return; }
		RecognizeTruck();
	}

    private bool CheckDynamicObjectReference()
    {
        var truckAndPlayerAreAssigned = firstTruck != null && playerHead != null && secondTruck != null;
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
        if (firstTruck.isActive == isActive) { return; }
        TruckActionControl.ActivateTruckAction(firstTruck);

    }

    protected override void DeactivateDynamicObjectStatus()
    {
        if (firstTruck.isActive == isActive) { return; }
        TruckActionControl.DeactivateTruckAction(firstTruck);
    }

    private void RecognizeTruck(){
		// check whether the player recognize that the truck stops
		// at the correct position in the lane 1
		// by alignment of position of the truck, the crane and the player
		float totalPosDiff = Mathf.Abs(playerHead.position.z - cranePosGuide.position.z)
			+ Mathf.Abs(truckGuide.position.z - cranePosGuide.position.z);

		float positionThreshold = 0.8f;
		var isAligned = totalPosDiff < positionThreshold;
		if(isAligned) {
			Debug.Log("TruckStops has finished");
            TruckActionControl.ActivateTruckObject(secondTruck);
			hasFinished = true;
		}
	}
}
