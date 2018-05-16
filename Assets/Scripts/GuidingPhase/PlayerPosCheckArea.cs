using UnityEngine;

/// <summary>
/// this checks whether the player is in the area defined by a collider
/// </summary>
public class PlayerPosCheckArea : MonoBehaviour {

	public bool isInside = false;

	private void OnTriggerStay(Collider other){
		if(other.CompareTag("MainCamera") && !isInside) {
			isInside = true;
		}
	}

	private void OnTriggerExit(Collider other){
		// check whether player's head exits the area
		if (other.CompareTag("MainCamera")){
			isInside = true;
		}
	}
}
