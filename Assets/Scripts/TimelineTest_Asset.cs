using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class TimelineTest_Asset : PlayableAsset
{
	// Factory method that generates a playable based on this asset
	public override Playable CreatePlayable(PlayableGraph graph, GameObject go) {
        TimelineTest_Behavior timelineTest = new TimelineTest_Behavior();
        return ScriptPlayable<TimelineTest_Behavior>.Create(graph, timelineTest);
		//return Playable.Create(graph);
	}
}
