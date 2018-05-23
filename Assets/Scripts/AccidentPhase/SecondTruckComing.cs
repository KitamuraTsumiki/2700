using UnityEngine;
/// <summary>
/// This manages the second block of the Accident (third) phase;
/// another truck comes from the lane 2,
/// and check whether the player is attacked by the truck
/// </summary>
public class SecondTruckComing : ContentSubBlock {


	public Transform truckFrontGuide;
	private SecondTruckActions truckAction;
	private Transform playerHead;

	private void Start(){
		playerHead = GetComponent<GuidingContentManager>().playerHead.transform;
		truckAction = (SecondTruckActions)GameObject.FindObjectOfType(typeof(SecondTruckActions));
	}

	private void Update () {
		CheckPlayerTruckContact();
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
}
