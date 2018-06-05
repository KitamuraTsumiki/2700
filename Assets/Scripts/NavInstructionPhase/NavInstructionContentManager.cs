using UnityEngine;
/// <summary>
/// This class manages choreography of the "navigation instruction" phase
/// </summary>
public class NavInstructionContentManager : ContentManager
{

	private void Start () {
		
	}

    public override void Pause()
    {

    }

    private void Update () {
        Debug.Log("you're in the navigation instruction phase");
    }
}
