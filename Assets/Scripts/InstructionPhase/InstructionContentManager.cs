using UnityEngine;
/// <summary>
/// This class manages the "instruction" phase
/// </summary>
public class InstructionContentManager : ContentManager
{

	private void Start () {
		
	}

    public override void EnterPause()
    {

    }

    public override void ExitPause()
    {

    }

    private void Update () {
        Debug.Log("you're in the instruction phase");
	}
}
