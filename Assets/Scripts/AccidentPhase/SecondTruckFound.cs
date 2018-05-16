using UnityEngine;
/// <summary>
/// This manages the first block of the Accident (third) phase;
/// check whether the player finds the second truck
/// </summary>
public class SecondTruckFound : MonoBehaviour {

	public bool hasFinished = false;
	public bool isInMainRoute = true;
	public float truckCheckPeriod = 3f;
	public Transform playerHead;
	public Transform truck;

	private float truckCheckDeadline;

	private void Start(){
		truckCheckDeadline = Time.time + truckCheckPeriod;
	}

	private void Update () {
		CheckTruckRecognition();
		ActivateTruckAction();
	}

	private void CheckTruckRecognition(){
		if(PlayerSawTruck()) {
			// move on instruction phase
			Debug.Log("SecondTruckFound has finished (instruction route)");
			isInMainRoute = false;
			hasFinished = true;
		}

		if (Time.time > truckCheckDeadline){
			// move on "SecondTruckComing"
			Debug.Log("SecondTruckFound has finished (main route)");
			hasFinished = true;
		}
	}

	private bool PlayerSawTruck(){
		float viewDirThreshold = 0.1f;
		Vector3 truckDir = Vector3.Normalize(truck.transform.position
			- new Vector3(playerHead.position.x, truck.transform.position.y, playerHead.position.z));
		Vector3 viewAngle = playerHead.transform.forward.normalized;

		float viewAngleDotTruckDir = Vector3.Dot(viewAngle, truckDir);

		return viewAngleDotTruckDir > viewDirThreshold;
	}

	private void ActivateTruckAction(){
		SecondTruckActions truckActions = truck.GetComponent<SecondTruckActions>();

		if(!truckActions.isActive) {
			truckActions.isActive = true;
		}
	}
}
