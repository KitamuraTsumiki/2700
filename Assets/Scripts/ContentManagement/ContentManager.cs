using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This is the parent of all "ContentsManager" class, 
/// which manages responsible scene of each of them.
/// </summary>
public class ContentManager : MonoBehaviour {

	public bool startFromPaused;
    [SerializeField]
    protected GameObject firstTruck;
    [SerializeField]
    protected GameObject secondTruck;
    [SerializeField]
    protected Transform cameraRig;
    [SerializeField]
    protected Transform initPlayerPos;

    protected virtual void Start () {
        
    }

    /// <summary>
    /// SetInitCamRigPos is called only beginning of instruction and replay phase.
    /// it is called by PhaseManager or child class of this class.
    /// </summary>
    protected void SetInitCamRigPos() {
        var simCamTransform = cameraRig.GetComponent<CameraTransform>();
        if (simCamTransform == null)
        {
            cameraRig.position = initPlayerPos.position;
            return;
        }

        // modification of camera height is for test without vive
        float cameraRigHeight = 1.8f;
        cameraRig.position = new Vector3(initPlayerPos.position.x, cameraRigHeight, initPlayerPos.position.z);
        Debug.Log("camera rig has camera transform");
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
