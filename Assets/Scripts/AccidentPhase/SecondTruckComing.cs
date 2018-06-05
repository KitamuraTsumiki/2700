using UnityEngine;
/// <summary>
/// This manages the second block of the Accident (third) phase;
/// another truck comes from the lane 2,
/// and check whether the player is attacked by the truck
/// </summary>
public class SecondTruckComing : ContentSubBlock {


	public Transform truckFrontGuide;
	private GameObject truck;

	[SerializeField]
	private SecondTruckActions truckAction;

	private Transform playerHead;

	private void Start(){
        base.Start();
        GuidingContentManager contentManager = GetComponent<GuidingContentManager>();
		playerHead = contentManager.playerHead.transform;
		truck = contentManager.secondTruck;
	}

	private void Update () {
        SwitchDynamicObjectStatus();
        if (!isActive) { return; }
        CheckTruckRecognition();
		CheckPlayerTruckContact();
	}

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
