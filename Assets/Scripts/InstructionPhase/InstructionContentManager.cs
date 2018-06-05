using UnityEngine;
/// <summary>
/// This class manages the "instruction" phase
/// </summary>
public class InstructionContentManager : ContentManager
{

	private void Start () {
		
	}

    public override void Pause()
    {

    }

    private void Update () {
        Debug.Log("you're in the instruction phase");
	}
}
