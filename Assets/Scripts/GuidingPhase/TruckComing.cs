using UnityEngine;
/// <summary>
/// This manages the first block of Guiding (second) part;
/// a truck is coming from lane 3
/// </summary>
public class TruckComing : ContentSubBlock {

	enum TruckComingState {displayTruckNotification, truckIsComing, checkPlayerPosition}

    [SerializeField]
	private CanvasGroup truckNotification;
    [SerializeField]
	private CanvasGroup playerPosNavigation;

	public PlayerPosCheckArea playerTargetZone;
    public Transform truckNotificationAnchor;

    public GameObject firstTruck;
    public Transform playerHead;

	private TruckComingState truckComingState;
	private FirstTruckActions truckAction;

	public void InitUI(){
        CanvasGroup[] canvases = {truckNotification, playerPosNavigation };
        GuideCanvasControl.TurnGroupOff(canvases);
	}

	protected override void Start () {
        base.Start();
		truckComingState = TruckComingState.displayTruckNotification;
    }
	
	private void Update () {
        if (!CheckDynamicObjectReference()) { return; }

        ActivateDynamicObjectStatus();
        DeactivateDynamicObjectStatus();
        if (!isActive) { return; }

		DisplayTruckNotification();
		TruckIsComing();
		CheckPlayerPosition();
	}

    private bool CheckDynamicObjectReference() {
        var truckAndPlayerAreAssigned = firstTruck != null && playerHead != null;
        if (!truckAndPlayerAreAssigned){ return false; }

        if (truckAction != null) { return true; }

        truckAction = firstTruck.GetComponent<FirstTruckActions>();
        truckAction.SetAtStartPosition();
        return true;
        
    }

    /// <summary>
    /// pausing function is called by "content manager" class
    /// in a function with the same name.
    /// it can be triggered by UI panels
    /// </summary>
    public override void Pause() {
        base.Pause();
    }

    protected override void ActivateDynamicObjectStatus() {
        if (truckAction.isActive == isActive) { return; }
        TruckActionControl.ActivateTruckAction(truckAction);
        
    }

    protected override void DeactivateDynamicObjectStatus()
    {
        if (truckAction.isActive == isActive) { return; }
        TruckActionControl.DeactivateTruckAction(truckAction);
    }

    /// <summary>
    /// depending on the style of UIs,
    /// this function may be separated from this class
    /// and included in "GuideCanvasControl" class in the future development.
    /// </summary>
	private void ModifyTruckNotificationTransform(){
		truckNotification.transform.position = truckNotificationAnchor.position;
		truckNotification.transform.LookAt(playerHead);
		truckNotification.transform.Rotate(Vector3.up, 180f);
	}

	private void DisplayTruckNotification(){
		if(truckComingState != TruckComingState.displayTruckNotification) { return;	}

        // display the notification ("the truck is coming")
        GuideCanvasControl.FadeIn(truckNotification);
        
		//modify position and rotation of the display
		ModifyTruckNotificationTransform();

		// move on the next state, when the player see the truck
		if(playerSawTruck()) {
			// start playing animation of the truck
			truckComingState = TruckComingState.truckIsComing;
		}
	}

	private bool playerSawTruck(){
		float viewDirThreshold = 0.1f;
		Vector3 truckDir = Vector3.Normalize(firstTruck.transform.position
			- new Vector3(playerHead.position.x, firstTruck.transform.position.y, playerHead.position.z));
		Vector3 viewAngle = playerHead.transform.forward.normalized;

		float viewAngleDotTruckDir = Vector3.Dot(viewAngle, truckDir);

		return viewAngleDotTruckDir > viewDirThreshold;
	}

	private void TruckIsComing (){
		if(truckComingState != TruckComingState.truckIsComing) { return; }

        // turn truck notification off
        GuideCanvasControl.TurnOff(truckNotification);

		// when animation of the truck is finished, move on the next state
		if (truckAction.hasFinished){
			truckComingState = TruckComingState.checkPlayerPosition;
		}

	}

	private void DisplayPlayerPosNavigation (){
        // if the player is outside of the target zone,
        // display truck notification
        GuideCanvasControl.FadeIn(playerPosNavigation);
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
