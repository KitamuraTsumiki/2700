using UnityEngine;
using UnityEngine.SceneManagement;

// This is the parent of all "ContentsManager" class, 
// which manages responsible scene of each of them.
public class ContentManager : MonoBehaviour {

	public PhaseManager phaseManager;
	public string nextPhase;
	public bool sceneTransitionEnabled = false; // for test

	protected virtual void Start () {
		if(phaseManager == null) {
			phaseManager = FindObjectOfType<PhaseManager>();
		}
	}

	protected virtual void SceneSwitch(){
		var switchScene = Input.GetKeyDown(KeyCode.A) && sceneTransitionEnabled;
		if(switchScene) {
			Debug.Log("A is pressed");
			SceneManager.LoadScene(nextPhase);
		}
	}
}
