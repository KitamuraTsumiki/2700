using UnityEngine;
/// <summary>
/// This manages the third block of the Accident (third) phase;
/// The truck hits the player
/// </summary>
public class SecondTruckHits : ContentSubBlock {

	public GameObject playerHead;
    [SerializeField]
	private CameraPosModification camPosMod;

	private void Start () {
        base.Start();

        
    }
	
	private void Update () {
        if (!CheckDynamicObjectReference()) { return; }

        SwitchDynamicObjectStatus();
        if (!isActive) { return; }
         // for test with vive
        camPosMod.UpdateCamTransform();
		if(camPosMod.IsMotionEnd()) {
			FadeSceneOut();
		}
	}

    private bool CheckDynamicObjectReference()
    {
        var dynamicObjectsAreAssigned = playerHead != null;
        if (dynamicObjectsAreAssigned)
        {
            // animate camera by physics simulation (without vive)
            var kinematicControl = playerHead.GetComponent<CharacterController>();
            if (kinematicControl != null)
            {
                kinematicControl.enabled = false;
            }

            return true;
        }
        return false;
    }

    /// <summary>
    /// pausing function is called by "content manager" class
    /// in a function with the same name.
    /// it can be triggered by UI panels
    /// </summary>
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
