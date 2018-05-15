using UnityEngine;
/// <summary>
/// This manages the first block of Guiding (second) part;
/// a truck is coming from lane 3
/// </summary>
public class TruckComing : MonoBehaviour {

	enum TruckComingState {displayTruckNotification, truckIsComing, checkPlayerPosition}

	public CanvasGroup truckNotification;

	public Transform playerHead;
	public GameObject truck;
	public bool hasFinished = false;
	public PlayerPosCheckArea playerTargetZone;

	private TruckComingState truckComingState;
	private Animator truckAnimation;
	private string truckComingAnimState = "Truck02_coming";

	private void Start () {
		truckComingState = TruckComingState.displayTruckNotification;
		truckNotification.alpha = 0f;
		truckAnimation = truck.GetComponent<Animator>();
	}
	
	private void Update () {
		DisplayTruckNotification();
		TruckIsComing();
		CheckPlayerPosition();
	}

	private void DisplayTruckNotification(){
		if(truckComingState != TruckComingState.displayTruckNotification) { return;	}

		// display the notification ("the truck is coming")
		truckNotification.alpha = Mathf.Min(truckNotification.alpha + Time.deltaTime, 1f);

		// move on the next state, when the player see the truck
		if(playerSawTruck()) {
			// start playing animation of the truck
			truckAnimation.Play(truckComingAnimState);
			truckComingState = TruckComingState.truckIsComing;
		}
	}

	private bool playerSawTruck(){
		float viewDirThreshold = 0.5f;
		Vector3 truckDir = truck.transform.position
			- new Vector3(playerHead.position.x, truck.transform.position.y, playerHead.position.z);

		float viewAngleDotTruckDir = Vector3.Dot(playerHead.rotation.eulerAngles, truckDir);

		Debug.Log("Dot product of view angle and truck direction" + viewAngleDotTruckDir);
		
		return viewAngleDotTruckDir < viewDirThreshold;
	}

	private void TruckIsComing (){
		if(truckComingState != TruckComingState.truckIsComing) { return; }

		// when animation of the truck is finished, move on the next state
		if (!truckAnimation.GetCurrentAnimatorStateInfo(0).IsName(truckComingAnimState)){
			truckComingState = TruckComingState.checkPlayerPosition;
		}

	}

	private void CheckPlayerPosition(){
		if(truckComingState != TruckComingState.checkPlayerPosition) { return; }

		if(!playerTargetZone.isInside) {
			// move on the next "TruckStop" (second) block in this phase
			hasFinished = true;
		}
	}
}
