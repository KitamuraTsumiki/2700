using UnityEngine;
/// <summary>
/// This manages the second block of the Accident (third) phase;
/// another truck comes from the lane 2,
/// and check whether the player is attacked by the truck
/// </summary>
public class SecondTruckComing : ContentSubBlock {

    public GameObject truck;
    public Transform playerHead;

    [SerializeField]
	private Transform truckFrontGuide;
	private SecondTruckActions truckAction;
    
	private void Start(){
        base.Start();
	}

	private void Update () {
        if (!CheckDynamicObjectReference()) { return; }

        SwitchDynamicObjectStatus();
        if (!isActive) { return; }
        CheckTruckRecognition();
		CheckPlayerTruckContact();
	}

    private bool CheckDynamicObjectReference()
    {
        var truckAndPlayerAreAssigned = truck != null && playerHead != null;
        if (!truckAndPlayerAreAssigned) { return false; }

        if (truckAction != null) { return true; }

        truckAction = truck.GetComponent<SecondTruckActions>();
        return true;
        
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
        if (truckAction.isActive == isActive) { return; }
        truckAction.isActive = isActive;
    }

    private void CheckPlayerTruckContact(){
		var truckHasPassed = truckFrontGuide.position.z - playerHead.position.z > 0f;

		if(truckAction.isContacting) {
			// move on "SecondTruckHits"
			Debug.Log("SecondTruckComing has finished (main route)");
			hasFinished = true;
		} else if (!truckAction.isContacting && truckHasPassed){
			// move on "SecondTruckStops"
			Debug.Log("SecondTruckComing has finished (SecondTruckStops)");
			isInMainRoute = false;
			hasFinished = true;
		}
	}

	private void CheckTruckRecognition(){
		if(!PlayerSawTruck()) { return; }
		// move on instruction phase
		truckAction.isGoingToHit = false;
		isInMainRoute = false;
		hasFinished = true;
		
	}

	private bool PlayerSawTruck(){
		
		float viewDirThreshold = 0.5f;
		Vector3 truckDir = Vector3.Normalize(truck.transform.position
			- new Vector3(playerHead.position.x, truck.transform.position.y, playerHead.position.z));
		Vector3 viewAngle = playerHead.transform.forward.normalized;

		float viewAngleDotTruckDir = Vector3.Dot(viewAngle, truckDir);

		return viewAngleDotTruckDir > viewDirThreshold;
	}
}
