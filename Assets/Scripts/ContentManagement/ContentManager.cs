using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This is the parent of all "ContentsManager" class, 
/// which manages responsible scene of each of them.
/// </summary>
public class ContentManager : MonoBehaviour {

	public string nextPhase;
	public bool sceneTransitionEnabled = false; // for test

	protected virtual void Start () {
		
	}

    public virtual void Pause() {

    }

	protected virtual void SceneSwitch(){
		var switchScene = Input.GetKeyDown(KeyCode.A) && sceneTransitionEnabled;
		if(switchScene) {
			Debug.Log("A is pressed");
			SceneManager.LoadScene(nextPhase);
		}
	}
}
