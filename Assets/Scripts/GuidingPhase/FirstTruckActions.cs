using UnityEngine;
/// <summary>
/// This class manages all actions of the first truck in the "guiding" phase
/// </summary>
public class FirstTruckActions : MonoBehaviour {

	// animation parameters
	[SerializeField]
	private Spline path;
	[SerializeField,Range(0f,1f)]
	private float currDist;
	[SerializeField, Range(0f, 1f)]
	private float currRot;
	private float distance;
	[SerializeField]
	private Transform tr;
	[SerializeField]
	private Transform startingPoint;

	private float truckSpeed = 0.1f;

	public bool isActive {
		get;
		set;
	}

	public bool hasFinished {
		get;
		private set;
	}

	private void Start () {
		tr.position = startingPoint.position;
		tr.rotation = startingPoint.rotation;
	}
	
	private void Update () {
		AnimateTruck();
		StopTruck();
	}

	private void AnimateTruck(){
		if(!isActive) { return; }

		float brakePosThresh = 0.9f;
		float minSpeed = 0.003f;
		if(currDist > brakePosThresh) {
			float brake = 0.985f;
			truckSpeed = Mathf.Max(truckSpeed * brake, minSpeed);
		}

		float step = Time.deltaTime * truckSpeed;

		currDist = Mathf.Min(currDist + step, 1f);
		tr.position = path.GetPositionOnSpline(currDist);
		tr.rotation = path.GetOrientationOnSpline(currDist);
	}

	private void StopTruck(){
		// in the future development, actual "navigation" of the truck will be added.
		if (currDist< 1f){return;}
		hasFinished = true;
	}
}
