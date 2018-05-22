using UnityEngine;

/// <summary>
/// This manages the third block of the Guiding (second) part;
/// The truck stops at correct position
/// </summary>
public class TruckStops : ContentSubBlock {

	public Transform cranePosGuide;
	public Transform truckGuide;
	public GameObject secondTruck;
	private Transform playerHead;

	private void Start(){
		playerHead = GetComponent<GuidingContentManager>().playerHead.transform;
	}

	private void Update () {
		RecognizeTruck();
	}

	private void RecognizeTruck(){
		// check whether the player recognize that the truck stops
		// at the correct position in the lane 1
		// by alignment of position of the truck, the crane and the player
		float totalPosDiff = Mathf.Abs(playerHead.position.z - cranePosGuide.position.z)
			+ Mathf.Abs(truckGuide.position.z - cranePosGuide.position.z);

		float positionThreshold = 0.8f;
		var isAligned = totalPosDiff < positionThreshold;
		if(isAligned) {
			Debug.Log("TruckStops has finished");
			secondTruck.SetActive(true);
			hasFinished = true;
		}
	}
}
