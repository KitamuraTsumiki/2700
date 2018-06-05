using UnityEngine;
/// <summary>
/// This manages the third block of the Accident (third) phase;
/// The truck hits the player
/// </summary>
public class SecondTruckHits : ContentSubBlock {

	private GameObject playerHead;
	private CameraPosModification camPosMod;

	private void Start () {
        base.Start();
        camPosMod = (CameraPosModification)FindObjectOfType(typeof(CameraPosModification));

		playerHead = GetComponent<GuidingContentManager>().playerHead;

		// animate camera by physics simulation (without vive)
		var kinematicControl = playerHead.GetComponent<CharacterController>();
		if(kinematicControl != null) {
			kinematicControl.enabled = false;
		}
	}
	
	private void Update () {
        SwitchDynamicObjectStatus();
        if (!isActive) { return; }
         // for test with vive
        camPosMod.UpdateCamTransform();
		if(camPosMod.IsMotionEnd()) {
			FadeSceneOut();
		}
	}

    public override void Pause()
    {
        base.Pause();
    }

    protected override void SwitchDynamicObjectStatus()
    {
        
    }

    private void FadeSceneOut(){
		// make the field of view dark (by an image on UI canvas)

		// if the scene gets dark, end this block of the story
		hasFinished = true;
	}
}
