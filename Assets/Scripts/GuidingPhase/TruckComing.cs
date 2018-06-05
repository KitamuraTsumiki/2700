using UnityEngine;
/// <summary>
/// This manages the first block of Guiding (second) part;
/// a truck is coming from lane 3
/// </summary>
public class TruckComing : ContentSubBlock {

	enum TruckComingState {displayTruckNotification, truckIsComing, checkPlayerPosition}

	public CanvasGroup truckNotification;
	public CanvasGroup playerPosNavigation;

	public PlayerPosCheckArea playerTargetZone;
    public Transform truckNotificationAnchor;

    private GameObject truck;
    private Transform playerHead;
	private TruckComingState truckComingState;
	private FirstTruckActions truckAction;
	private string truckComingAnimState = "Truck02_coming";

	public void InitUI(){
		truckNotification.alpha = 0f;
		playerPosNavigation.alpha = 0f;
	}

	protected override void Start () {
        base.Start();
		truckComingState = TruckComingState.displayTruckNotification;
        GuidingContentManager contentManager = GetComponent<GuidingContentManager>();
        truck = contentManager.firstTruck;
        truckAction = truck.GetComponent<FirstTruckActions>();
		playerHead = contentManager.playerHead.transform;
	}
	
	private void Update () {
        SwitchDynamicObjectStatus();
        if (!isActive) { return; }
		DisplayTruckNotification();
		TruckIsComing();
		CheckPlayerPosition();
	}

    public override void Pause()
    {
        base.Pause();
    }

    protected override void SwitchDynamicObjectStatus() {
        if (truckAction.isActive == isActive) { return; }
        truckAction.isActive = isActive;
    }

	private void ModifyTruckNotificationTransform(){
		truckNotification.transform.position = truckNotificationAnchor.position;
		truckNotification.transform.LookAt(playerHead);
		truckNotification.transform.Rotate(Vector3.up, 180f);
	}

	private void DisplayTruckNotification(){
		if(truckComingState != TruckComingState.displayTruckNotification) { return;	}

		// display the notification ("the truck is coming")
		truckNotification.alpha = Mathf.Min(truckNotification.alpha + Time.deltaTime, 1f);
        
		//modify position and rotation of the display
		ModifyTruckNotificationTransform();

		// move on the next state, when the player see the truck
		if(playerSawTruck()) {
			// start playing animation of the truck
			truckAction.isActive = true;
			truckComingState = TruckComingState.truckIsComing;
		}
	}

	private bool playerSawTruck(){
		float viewDirThreshold = 0.1f;
		Vector3 truckDir = Vector3.Normalize(truck.transform.position
			- new Vector3(playerHead.position.x, truck.transform.position.y, playerHead.position.z));
		Vector3 viewAngle = playerHead.transform.forward.normalized;

		float viewAngleDotTruckDir = Vector3.Dot(viewAngle, truckDir);

		return viewAngleDotTruckDir > viewDirThreshold;
	}

	private void TruckIsComing (){
		if(truckComingState != TruckComingState.truckIsComing) { return; }

        // turn truck notification off
        truckNotification.alpha = 0f;

		// when animation of the truck is finished, move on the next state
		if (truckAction.hasFinished){
			truckComingState = TruckComingState.checkPlayerPosition;
		}

	}

	private void DisplayPlayerPosNavigation (){
		// if the player is outside of the target zone,
		// display truck notification
		playerPosNavigation.alpha = Mathf.Min(playerPosNavigation.alpha + Time.deltaTime, 1f);
	} 

	private void CheckPlayerPosition(){
		if(truckComingState != TruckComingState.checkPlayerPosition) { return; }

		if(!playerTargetZone.isInside) {
			DisplayPlayerPosNavigation();

			// the following process is executed after displaying player position navigation
			if(playerPosNavigation.alpha < 1) { return; }
			// move on "Instruction of correct navigation position"
			Debug.Log("TruckComing has finished (to instruction phase)");
			isInMainRoute = false;
			hasFinished = true;
		} else {
			// move on the next block "TruckStops"
			Debug.Log("TruckComing has finished (main route)");
			hasFinished = true;
		}
	}
}
