using UnityEngine;
/// <summary>
/// This class manages all actions of the first truck in the "guiding" phase
/// </summary>
public class FirstTruckActions : TruckActions
{

	public bool hasFinished {
		get;
		private set;
	}

    private void Start()
    {
        SetAtStartPosition();
        RecordOriginalSpeed();
        gameObject.SetActive(false);
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
        SetTransform();
	}

	private void StopTruck(){
		// in the future development, actual "navigation" of the truck will be added.
		if (currDist< 1f){return;}
		hasFinished = true;
	}
}
