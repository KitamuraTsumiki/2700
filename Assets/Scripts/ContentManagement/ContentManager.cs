using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This is the parent of all "ContentsManager" class, 
/// which manages responsible scene of each of them.
/// </summary>
public class ContentManager : MonoBehaviour {

	public bool startFromPaused;

    protected virtual void Start () {
		
	}

    protected virtual void SetInitialState()
    {
        
    }

    public virtual void EnterPause()
    {

    }

    public virtual void ExitPause()
    {

    }
}
