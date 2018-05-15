using UnityEngine;

/// <summary>
/// this checks whether the player is in the area defined by a collider
/// </summary>
public class PlayerPosCheckArea : MonoBehaviour {

	public bool isInside = false;

	void OnTriggerEnter(Collider other){
		// check whether player's head enters the area
		if (other.CompareTag("MainCamera")){
			isInside = true;
		}
	}

	void OnTriggerExit(Collider other){
		// check whether player's head exits the area
		if (other.CompareTag("MainCamera")){
			isInside = true;
		}
	}
}
