using UnityEngine;
/// <summary>
/// This manages the second block of the Accident (third) phase;
/// another truck comes from the lane 2,
/// and check whether the player is attacked by the truck
/// </summary>
public class SecondTruckComing : ContentSubBlock {

	public SecondTruckActions truckAction;
	public Transform truckFrontGuide;
	public Transform playerHead;

	private void Update () {
		var truckHasPassed = truckFrontGuide.position.z - playerHead.position.z > 0f;

		if(!truckHasPassed) { return; }
		CheckPlayerTruckContact();
	}



	private void CheckPlayerTruckContact(){
		if(truckAction.hitting) {
			// move on "SecondTruckHits"
			Debug.Log("SecondTruckComing has finished (main route)");
			hasFinished = true;
		} else {
			// move on "SecondTruckStops"
			Debug.Log("SecondTruckComing has finished (SecondTruckStops)");
			isInMainRoute = false;
			hasFinished = true;
		}
	}
}
