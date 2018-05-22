using UnityEngine;
/// <summary>
/// This manages the first block of the Accident (third) phase;
/// check whether the player finds the second truck
/// </summary>
public class SecondTruckFound : ContentSubBlock {

	public float truckCheckPeriod = 3f;
	public Transform truck;
	private Transform playerHead;

	private float truckCheckDeadline;

	private void Start(){
		truckCheckDeadline = Time.time + truckCheckPeriod;
		playerHead = GetComponent<GuidingContentManager>().playerHead.transform;
	}

	private void Update () {
		CheckTruckRecognition();
	}

	private void CheckTruckRecognition(){
		bool willTruckHit = true;

		if(PlayerSawTruck()) {
			// move on instruction phase
			Debug.Log("SecondTruckFound has finished (instruction route)");
			willTruckHit = false;
			ActivateTruckAction(willTruckHit);
			isInMainRoute = false;
			hasFinished = true;
		}

		if (Time.time > truckCheckDeadline){ // if the player doesn't notice the second truck
			// move on "SecondTruckComing"
			Debug.Log("SecondTruckFound has finished (main route)");
			ActivateTruckAction(willTruckHit);
			hasFinished = true;
		}
	}

	private bool PlayerSawTruck(){
		float viewDirThreshold = 0.5f;
		Vector3 truckDir = Vector3.Normalize(truck.transform.position
			- new Vector3(playerHead.position.x, truck.transform.position.y, playerHead.position.z));
		Vector3 viewAngle = playerHead.transform.forward.normalized;

		float viewAngleDotTruckDir = Vector3.Dot(viewAngle, truckDir);

		return viewAngleDotTruckDir > viewDirThreshold;
	}

	private void ActivateTruckAction(bool _willHit){
		SecondTruckActions truckActions = truck.GetComponent<SecondTruckActions>();

		truckActions.isGoingToHit = _willHit;

		if(!truckActions.isActive) {
			truckActions.isActive = true;
		}
	}
}
