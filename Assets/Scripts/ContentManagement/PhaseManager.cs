using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseManager : MonoBehaviour {

	public static 	PhaseManager singleton;
	public int 		phaseId = 0;

	void Awake () {

		// keep only one contents manager in any scenes
		if(singleton == null) {
			DontDestroyOnLoad(gameObject);
			singleton = this;
		} else {
			Destroy(gameObject);
		}

	}

	void OnLevelWasLoaded(){
		// count phase index
		phaseId = SceneManager.GetActiveScene().buildIndex;
		Debug.Log("phaseId: " + phaseId);
	}
}
