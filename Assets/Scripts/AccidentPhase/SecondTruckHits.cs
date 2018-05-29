using UnityEngine;
/// <summary>
/// This manages the third block of the Accident (third) phase;
/// The truck hits the player
/// </summary>
public class SecondTruckHits : ContentSubBlock {

	private GameObject playerHead;
	private Rigidbody playerRbd;
	private CameraPosModification camPosMod;

	private void Start () {
		camPosMod = (CameraPosModification)FindObjectOfType(typeof(CameraPosModification));

		playerHead = GetComponent<GuidingContentManager>().playerHead;

		// animate camera by physics simulation (without vive)
		var kinematicControl = playerHead.GetComponent<CharacterController>();
		if(kinematicControl != null) {
			kinematicControl.enabled = false;
			playerRbd = GetComponent<GuidingContentManager>().playerRbdDummy.GetComponent<Rigidbody>();
			playerRbd.isKinematic = false;
			playerRbd.useGravity = true;
		}
	}
	
	private void Update () {
		//CheckPlayerVel(); // for test without vive

		// for test with vive
		camPosMod.UpdateCamTransform();
		if(camPosMod.IsMotionEnd()) {
			FadeSceneOut();
		}
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
