using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseManager : MonoBehaviour {

	public static PhaseManager singleton;
	public int phaseId = 0;

	private void Awake () {

		// keep only one phase manager in any scenes
		if(singleton == null) {
			DontDestroyOnLoad(gameObject);
			singleton = this;
		} else {
			Destroy(gameObject);
		}

	}

	private void OnLevelWasLoaded(){
		// count phase index
		phaseId = SceneManager.GetActiveScene().buildIndex;
		Debug.Log("phaseId: " + phaseId);
	}
}
