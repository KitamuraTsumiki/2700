using UnityEngine;

/// <summary>
/// This manages the third block of the Guiding (second) part;
/// The truck stops at correct position
/// </summary>
public class TruckStops : ContentSubBlock {

    public FirstTruckActions firstTruck;
    public GameObject secondTruck;
    public Transform playerHead;

    [SerializeField]
	private Transform cranePosGuide;
    [SerializeField]
	private Transform truckGuide;
    
	private void Start(){
        base.Start();
    }

	private void Update () {
        if (!CheckDynamicObjectReference()) { return; }

        SwitchDynamicObjectStatus();
        if (!isActive) { return; }
		RecognizeTruck();
	}

    private bool CheckDynamicObjectReference()
    {
        var truckAndPlayerAreAssigned = firstTruck != null && playerHead != null && secondTruck != null;
        if (truckAndPlayerAreAssigned)
        {
            return true;
        }
        return false;
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

    protected override void SwitchDynamicObjectStatus()
    {
        if (firstTruck.isActive == isActive) { return; }
        firstTruck.isActive = isActive;
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
			secondTruck.SetActive(true);
			hasFinished = true;
		}
	}
}
