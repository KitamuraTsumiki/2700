using UnityEngine;
/// <summary>
/// This manages the third block of the Accident (third) phase;
/// The truck hits the player
/// </summary>
public class SecondTruckHits : ContentSubBlock {

	public GameObject playerHead;

	private Rigidbody playerRbd;

	private void Start () {
		// animate camera by physics simulation (without vive)
		var kinematicControl = playerHead.GetComponent<CharacterController>();
		if(kinematicControl != null) {
			kinematicControl.enabled = false;
			playerRbd = playerHead.GetComponent<Rigidbody>();
			playerRbd.isKinematic = false;
			playerRbd.useGravity = true;
		}
	}
	
	private void Update () {
		CheckPlayerVel();
	}

	private void CheckPlayerVel(){
		float velMagnitudeTh = 0.15f;

		if(playerRbd.velocity.magnitude < velMagnitudeTh) {
			FadeSceneOut();
		}
	}

	private void FadeSceneOut(){
		// make the field of view dark (by an image on UI canvas)

		// if the scene gets dark, end this block of the story
		hasFinished = true;
	}
}
