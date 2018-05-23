using UnityEngine;
using UnityEngine.XR;
/// <summary>
/// This class modifies the motion of the camera rig for vive
/// when the player is hit by a truck
/// </summary>
public class CameraPosModification : MonoBehaviour {

    public GameObject cameraPiv;
    public GameObject viveCamera;
    public bool isActivated = false;

	private float step = 0f;
	private Vector3 contactPoint;
	private Vector3 endPoint;

	public void SetPointsAndNormal(Vector3 _contactPoint){
		contactPoint = _contactPoint;
		InitEndPoint();
	}

	public void UpdateCamTransform(){

		MoveCameraRig();

	}

	public bool IsMotionEnd(){
		if(step < 1f) {
			return false;
		} else {
			return true;
		}
	}

	private void InitEndPoint(){
		float heightOfEye = 0.15f;
		float distAlongZAxis = 3.5f;
		endPoint = new Vector3(0f, heightOfEye, contactPoint.z + distAlongZAxis);
	}

	private void MoveCameraRig() {
		if(!isActivated) {return;}
		float speed = 2f;
		step = Mathf.Min(step + Time.deltaTime * speed, 1f);

		transform.position = Vector3.Lerp(contactPoint, endPoint, step);
    }

	private void ModifyDiffPosOfCam(){
		if(!isActivated) {return;}
		// modify position of the camera of vive
		Vector3 trackedLocalCamPos = InputTracking.GetLocalPosition(XRNode.CenterEye);

		cameraPiv.transform.position = transform.TransformPoint(-trackedLocalCamPos);
	}

	private void ModifyDiffRotOfCam(){
		if(!isActivated) {return;}
		// modify rotation of the camera of vive
		Quaternion trackedLocalCamRot = InputTracking.GetLocalRotation(XRNode.CenterEye);

		cameraPiv.transform.rotation = Quaternion.Inverse(trackedLocalCamRot);
	}

	private void Update(){
		ModifyDiffPosOfCam();

	}
}
