using UnityEngine;

/// <summary>
/// This manages the third block of the Guiding (second) part;
/// The truck stops at correct position
/// </summary>
public class TruckStops : ContentSubBlock {

    [SerializeField]
	private Transform cranePosGuide;
    [SerializeField]
	private Transform truckGuide;
    private FirstTruckActions firstTruck;
	private GameObject secondTruck;
	private Transform playerHead;

	private void Start(){
        base.Start();
        GuidingContentManager contentManager = GetComponent<GuidingContentManager>();
        playerHead = contentManager.playerHead.transform;
        firstTruck = contentManager.firstTruck.GetComponent<FirstTruckActions>();
        secondTruck = contentManager.secondTruck;
    }

	private void Update () {
        SwitchDynamicObjectStatus();
        if (!isActive) { return; }
		RecognizeTruck();
	}

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
