using UnityEngine;

/// <summary>
/// This class manages the choreography of the accident (third) scene.
/// </summary>

public class AccidentContentManager : ContentManager {

	protected override void Start () {
		// get the phase manager
		base.Start();

	}
	
	private void Update () {
		// for testing scene transition
		base.SceneSwitch();
	}
}
