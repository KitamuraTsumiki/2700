using UnityEngine;

/// <summary>
/// This manages the third block of the Guiding (second) part;
/// The truck stops at correct position
/// </summary>
public class TruckStops : MonoBehaviour {

	public Transform cranePosGuide;
	public Transform playerHead;
	public Transform truckGuide;
	public float positionThreshold = 0.8f;
	public bool hasFinished = false;

	private void Update () {
		RecognizeTruck();
	}


	private void RecognizeTruck(){
		// check whether the player recognize that the truck stops
		// at the correct position in the lane 1
		// by alignment of position of the truck, the crane and the player
		float totalPosDiff = Mathf.Abs(playerHead.position.z - cranePosGuide.position.z)
			+ Mathf.Abs(truckGuide.position.z - cranePosGuide.position.z);

		Debug.Log("totalPosDiff: " + totalPosDiff);

		var isAligned = totalPosDiff < positionThreshold;
		if(isAligned) {
			Debug.Log("TruckStops has finished");
		}
	}
}
