using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class TimelineTest_Behavior : PlayableBehaviour
{
	// Called when the owning graph starts playing
	public override void OnGraphStart(Playable playable) {
        Debug.Log("sequence start");
    }

	// Called when the owning graph stops playing
	public override void OnGraphStop(Playable playable) {
        Debug.Log("sequence end");
    }

	// Called when the state of the playable is set to Play
	public override void OnBehaviourPlay(Playable playable, FrameData info) {
        Debug.Log("sequence is being played");

        // event calling test with instructionContentManager class
        // any better way to get the gameobject with the InstructionContentManager?
        GameObject obj = GameObject.FindWithTag("contentsManager");
        if (obj == null) { return; }
        InstructionContentManager contentManager = obj.GetComponent<InstructionContentManager>();
        if (contentManager == null) { return; }
        contentManager.timelineTest();
    }

	// Called when the state of the playable is set to Paused
	public override void OnBehaviourPause(Playable playable, FrameData info) {
        Debug.Log("sequence is paused");
    }

	// Called each frame while the state is set to Play
	public override void PrepareFrame(Playable playable, FrameData info) {
		
	}
}
